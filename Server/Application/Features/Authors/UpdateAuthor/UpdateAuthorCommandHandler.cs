using BookStoreMongoDb.Server.Infrastructure.Repositories;
using BookStoreMongoDb.Server.Models.Entities;
using BookStoreMongoDb.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreMongoDb.Server.Application.Features.Authors.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, IResult<bool>>
    {
        private readonly IMongoRepository<Author> _authorRepository;

        private readonly ILogger<UpdateAuthorCommandHandler> _logger;

        public UpdateAuthorCommandHandler(IMongoRepository<Author> authorRepository, ILogger<UpdateAuthorCommandHandler> logger)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IResult<bool>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating author ...");

            try
            {
                Author author = await _authorRepository.FindByIdAsync(request.Id);

                if (author == null) throw new Exception("Error : Author Id does not exist.");

                author.Name = request.Name;
                author.ShortBio = request.ShortBio;
                author.BirthDate = request.BirthDate;
                author.LastModifiedOn = DateTime.UtcNow;

                await _authorRepository.ReplaceOneAsync(author);

                return await Result<bool>.SuccessAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when updating author", ex);
                throw;
            }
        }
    }
}
