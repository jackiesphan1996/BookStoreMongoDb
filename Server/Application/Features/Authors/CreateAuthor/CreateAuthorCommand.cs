using System;
using BookStoreMongoDb.Shared;
using MediatR;

namespace BookStoreMongoDb.Server.Application.Features.Authors.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<IResult<bool>>
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }
    }
}
