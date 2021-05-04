using BookStoreMongoDb.Server.Application.Features.Authors.CreateAuthor;
using BookStoreMongoDb.Server.Infrastructure.Repositories;
using BookStoreMongoDb.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreMongoDb.Server.Application.Features.Authors.GetAllAuthors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, PaginatedResult<GetAllAuthorsViewModel>>
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly ILogger<CreateAuthorCommandHandler> _logger;

        public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository, ILogger<CreateAuthorCommandHandler> logger)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResult<GetAllAuthorsViewModel>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all authors");
            try
            {
                var allAuthors = _authorRepository.GetAllWithPaging(request.Search, request.Page, request.PageSize, out var totalRecord);

                var data = allAuthors.Select(x => new GetAllAuthorsViewModel
                    {
                        Id = x.Id.ToString(),
                        Name = x.Name,
                        BirthDate = x.BirthDate,
                        ShortBio = x.ShortBio,
                        CreatedOn = x.CreatedOn,
                        LastModifiedOn = x.LastModifiedOn
                    }).ToList();


                return new PaginatedResult<GetAllAuthorsViewModel>
                {
                    TotalCount = totalRecord,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when get all authors", ex);
                throw;
            }
        }

    }

    public static class AAA
    {
        public static Expression<Func<T, bool>> ToExpression<T>(this Predicate<T> p)
        {
            ParameterExpression p0 = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, bool>>(Expression.Call(p.Method, p0),
                new ParameterExpression[] { p0 });
        }
    }
}
