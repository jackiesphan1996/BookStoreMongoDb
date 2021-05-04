using BookStoreMongoDb.Server.Configurations;
using BookStoreMongoDb.Server.Models.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace BookStoreMongoDb.Server.Infrastructure.Repositories
{
    public interface IAuthorRepository : IMongoRepository<Author>
    {
        IEnumerable<Author> GetAllWithPaging(string search, int page, int pageSize, out long totalRecord);
    }
    public class AuthorRepository : MongoRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(IMongoDbSettings settings) : base(settings)
        {
        }

        public IEnumerable<Author> GetAllWithPaging(string search, int page, int pageSize, out long totalRecord)
        {
            search ??= "";

            totalRecord = string.IsNullOrEmpty(search) ? Collection.EstimatedDocumentCount() : Collection.Find(x => x.Name.Contains(search)).CountDocuments();

            if (totalRecord > 0)
            {
                return Collection.Find(x => x.Name.Contains(search)).SortByDescending(x => x.CreatedOn).Skip((page - 1) * pageSize).Limit(pageSize).ToList();
            }

            return new List<Author>();
        }
    }
}
