using System.Threading.Tasks;
using BookStoreMongoDb.Client.Models;
using BookStoreMongoDb.Shared;

namespace BookStoreMongoDb.Client.Services.Author
{
    public interface IAuthorService : IService
    {
        Task<PaginatedResult<GetAllAuthorsViewModel>> GetAllAsync(int page, int pageSize, string search);
        Task<IResult> CreateAsync(AuthorViewModel request);
    }
}
