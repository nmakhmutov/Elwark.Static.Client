using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Elwark.Storage.Client.Test
{
    public class StaticEndpointTest : BaseTest
    {
        [Fact]
        public void Check_static_icon_url()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            var image = client.Static.Icons.Elwark.Primary.Size16x16;

            Assert.EndsWith("/static/icons/elwark/primary/16x16.png", image.Path.ToString());
        }

        [Fact]
        public async Task Download_bytes_static_icon()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            var image = client.Static.Icons.Elwark.Primary.Size16x16;
            var result = await image.GetBytesAsync();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Download_random_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();
            var result = await client.Images.GetRandom().GetBytesAsync();
            
            Assert.NotEmpty(result);
        }
    }
}