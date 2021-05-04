using BookStoreMongoDb.Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;

namespace BookStoreMongoDb.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder
                .CreateDefault(args)
                .AddClientServices();

            builder.RootComponents.Add<App>("#app");

            await builder.Build().RunAsync();
        }
    }
}
