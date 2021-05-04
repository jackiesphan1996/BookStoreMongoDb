using BookStoreMongoDb.Shared;
using MediatR;

namespace BookStoreMongoDb.Server.Application.Features.Authors.GetAllAuthors
{

    public class GetAllAuthorsQuery : IRequest<PaginatedResult<GetAllAuthorsViewModel>>
    {
        public string Search { get; set; } = "";
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

}
