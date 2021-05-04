using System;
using System.Threading;
using System.Threading.Tasks;
using BookStoreMongoDb.Server.Infrastructure.Repositories;
using BookStoreMongoDb.Server.Models.Entities;
using BookStoreMongoDb.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookStoreMongoDb.Server.Application.Features.Authors.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, IResult<bool>>
    {
        private readonly IMongoRepository<Author> _authorRepository;

        private readonly ILogger<DeleteAuthorCommandHandler> _logger;

        public DeleteAuthorCommandHandler(IMongoRepository<Author> authorRepository, ILogger<DeleteAuthorCommandHandler> logger)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IResult<bool>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting author ...");

            try
            {
                Author author = await _authorRepository.FindByIdAsync(request.Id);

                if (author == null) throw new Exception("Error : Author Id does not exist.");

                await _authorRepository.DeleteByIdAsync(author.Id.ToString());

                return await Result<bool>.SuccessAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Deleting author", ex);
                throw;
            }
        }
    }
}
