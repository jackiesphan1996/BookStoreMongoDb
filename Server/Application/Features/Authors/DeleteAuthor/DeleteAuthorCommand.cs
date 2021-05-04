using BookStoreMongoDb.Shared;
using MediatR;

namespace BookStoreMongoDb.Server.Application.Features.Authors.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<IResult<bool>>
    {
        public string Id { get; set; }
    }
}
