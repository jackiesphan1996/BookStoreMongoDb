using System;
using System.ComponentModel.DataAnnotations;
using BookStoreMongoDb.Shared;
using MediatR;

namespace BookStoreMongoDb.Server.Application.Features.Authors.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<IResult<bool>>
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }
    }
}
