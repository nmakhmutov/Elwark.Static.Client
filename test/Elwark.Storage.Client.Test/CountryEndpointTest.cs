using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Elwark.Storage.Client.Test
{
    public class CountryEndpointTest
    {
        private readonly Uri _host = new Uri("http://localhost:3000");

        [Fact(Skip = ".local")]
        public async Task Check_all_countries()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(collection => collection.AddElwarkStorageClient(_host))
                .Configure(applicationBuilder => { });

            var server = new TestServer(builder);

            var client = server.Services.GetService<IElwarkStorageClient>();

            var result = await client.Country.GetAllAsync();
            Assert.NotNull(result);
        }
        
        [Fact(Skip = ".local")]
        public async Task Check_by_codes_countries()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(collection => collection.AddElwarkStorageClient(_host))
                .Configure(applicationBuilder => { });

            var server = new TestServer(builder);

            var client = server.Services.GetService<IElwarkStorageClient>();

            var result = await client.Country.GetByCodesAsync(new []{"ru", "us", "gb", "ad"});
            Assert.NotNull(result);
        }
    }
}