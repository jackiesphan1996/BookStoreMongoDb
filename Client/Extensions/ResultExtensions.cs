using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BookStoreMongoDb.Shared;

namespace BookStoreMongoDb.Client.Extensions
{
    public static class ResultExtensions
    {
        public static async Task<PaginatedResult<T>> ToPagingResult<T>(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<PaginatedResult<T>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return responseObject;
        }

        public static async Task<IResult> ToResult(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<Result>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return responseObject;
        }
    }
}
