using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BookStoreMongoDb.Client.Extensions;
using BookStoreMongoDb.Client.Models;
using BookStoreMongoDb.Shared;

namespace BookStoreMongoDb.Client.Services.Author
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _httpClient;

        public AuthorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginatedResult<GetAllAuthorsViewModel>> GetAllAsync(int page, int pageSize, string search)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(Routes.AuthorEndPoint.GetAll(page, pageSize, search));

            return await response.ToPagingResult<GetAllAuthorsViewModel>();
        }

        public async Task<IResult> CreateAsync(AuthorViewModel request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(Routes.AuthorEndPoint.Create, request);

            return await response.ToResult();
        }
    }
}
