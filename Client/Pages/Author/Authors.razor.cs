using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BookStoreMongoDb.Client.Models;
using BookStoreMongoDb.Client.Services.Author;
using BookStoreMongoDb.Shared;
using Microsoft.AspNetCore.Components;

namespace BookStoreMongoDb.Client.Pages.Author
{
    public partial class Authors
    {
        private List<GetAllAuthorsViewModel> _authors = new List<GetAllAuthorsViewModel>();

        private AuthorViewModel _author = new AuthorViewModel();
        private int Page { get; set; } = 1;
        private int PageSize { get; set; } = 10;
        private int TotalCount { get; set; }
        private string SearchText { get; set; } = "";
        private bool IsLoading { get; set; }
        private bool AuthorDialogVisible { get; set; }
        private string AuthorDialogTitle { get; set; }
        private Form<AuthorViewModel> AuthorDialogForm { get; set; }

        [Inject] 
        private IAuthorService AuthorService { get; set; }
        
        [Inject]
        private MessageService MessageService { get; set; }

        [Inject]
        private ConfirmService ConfirmService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetAllAsync();
        }

        private async Task GetAllAsync()
        {
            IsLoading = true;
            var response = await AuthorService.GetAllAsync(Page, PageSize, SearchText);
            if (response.Succeeded)
            {
                _authors = response.Data;
                TotalCount = response.TotalCount;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    await MessageService.Error(message);
                }
            }

            IsLoading = false;
        }

        private async Task HandlePageIndexChanged(PaginationEventArgs args)
        {
            Page = args.Page;
            PageSize = args.PageSize;

            await GetAllAsync();
        }

        private async Task HandlePageSizeChange(PaginationEventArgs args)
        {
            Page = args.Page;
            PageSize = args.PageSize;

            await GetAllAsync();
        }

        private void CreateAuthor()
        {
            _author = new AuthorViewModel
            {
                BirthDate = DateTime.Now
            };
            AuthorDialogVisible = true;
            AuthorDialogTitle = "Create Author";
        }

        private void HandleAuthorDialogCancel()
        {
            AuthorDialogVisible = false;
        }

        private async Task HandleAuthorDialogOk()
        {
            if (this.AuthorDialogForm.Validate())
            {
                await SaveAsync(_author);
            }
        }

        private void Edit(string id)
        {
            
        }

        private async Task ShowDeleteConfirm(GetAllAuthorsViewModel author)
        {
            var content = $"Are you sure to delete Author  '{author.Name}' ?";
            var title = "Delete confirmation";
            var confirmResult = await ConfirmService.Show(content, title, ConfirmButtons.YesNo);
            if (confirmResult == ConfirmResult.Yes)
            {
                await Delete(author);
            }
        }

        private async Task SaveAsync(AuthorViewModel author)
        {
            if (string.IsNullOrEmpty(author.Id))
            {
                await CreateAsync(author);
            }
            else
            {
                await UpdateAsync(author);
            }
        }

        private async Task CreateAsync(AuthorViewModel author)
        {
            var request = new AuthorViewModel { Name = author.Name, ShortBio = author.ShortBio, BirthDate = author.BirthDate };

            var response = await AuthorService.CreateAsync(request);

            if (response.Succeeded)
            {
                await GetAllAsync();
                AuthorDialogVisible = false;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    await MessageService.Error(message);
                }
            }
        }

        private async Task UpdateAsync(AuthorViewModel author)
        {
            
        }

        private async Task Delete(GetAllAuthorsViewModel author)
        {
            
        }
    }
}
