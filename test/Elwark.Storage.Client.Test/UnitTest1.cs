using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Elwark.Storage.Client.Test
{
    public class StaticEndpointTest
    {
        private readonly Uri _host = new Uri("http://localhost:3000");
        
        [Fact]
        public void Check_static_icon_url()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(collection => collection.AddElwarkStorageClient(_host))
                .Configure(applicationBuilder => { });

            var server = new TestServer(builder);

            var client = server.Services.GetService<IElwarkStorageClient>();

            var image = client.Static.Icons.Elwark.Primary.Size16x16;
            Assert.Equal(new Uri(_host, "static/icons/elwark/primary/16x16.png"), image.Path);
        }

        [Fact(Skip = ".local")]
        public async Task Download_bytes_static_icon()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(collection => collection.AddElwarkStorageClient(_host))
                .Configure(applicationBuilder => { });

            var server = new TestServer(builder);

            var client = server.Services.GetService<IElwarkStorageClient>();

            var image = client.Static.Icons.Elwark.Primary.Size16x16;
            var result = await image.GetBytesAsync();
            
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        } 
    }
}