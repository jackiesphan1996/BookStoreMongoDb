using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMongoDb.Server.Infrastructure.Repositories;
using BookStoreMongoDb.Server.Models.Entities;
using Microsoft.Extensions.Logging;

namespace BookStoreMongoDb.Server
{
    public interface IDatabaseSeeder
    {
        void Initialize();
    }

    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly IMongoRepository<Author> _authorRepository;

        public DatabaseSeeder(ILogger<DatabaseSeeder> logger, IMongoRepository<Author> authorRepository)
        {
            _logger = logger;
            _authorRepository = authorRepository;
        }

        public void Initialize()
        {
            _logger.LogInformation("Start seeding data....");

            AddAuthors();
        }

        private void AddAuthors()
        {
            Task.Run(async () =>
            {
                if (!_authorRepository.AsQueryable().Any())
                {
                    var createAuthors = new List<Author>();
                    for (int i = 0; i < 100000; i++)
                    {
                        createAuthors.Add(new Author
                        {
                            Name = $"Author {i}",
                            CreatedOn = DateTime.Now.AddMinutes(i % 2),
                            ShortBio = "Short bio",
                            BirthDate = DateTime.Now.AddYears(-20)
                        });
                    }

                    await _authorRepository.InsertManyAsync(createAuthors);
                }
            });
        }
    }
}
