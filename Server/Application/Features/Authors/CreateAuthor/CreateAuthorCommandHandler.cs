using System;
using System.Threading;
using System.Threading.Tasks;
using BookStoreMongoDb.Server.Infrastructure.Repositories;
using BookStoreMongoDb.Server.Models.Entities;
using BookStoreMongoDb.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookStoreMongoDb.Server.Application.Features.Authors.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, IResult<bool>>
    {
        private readonly IMongoRepository<Author> _authorRepository;

        private readonly ILogger<CreateAuthorCommandHandler> _logger;

        public CreateAuthorCommandHandler(IMongoRepository<Author> authorRepository, ILogger<CreateAuthorCommandHandler> logger)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IResult<bool>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating author ...");

            try
            {
                var author = new Author
                {
                    Name = request.Name,
                    BirthDate = request.BirthDate,
                    ShortBio = request.ShortBio,
                    CreatedOn = DateTime.UtcNow
                };

                await _authorRepository.InsertOneAsync(author);

                return await Result<bool>.SuccessAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when creating author", ex);
                throw;
            }
        }
    }
}
