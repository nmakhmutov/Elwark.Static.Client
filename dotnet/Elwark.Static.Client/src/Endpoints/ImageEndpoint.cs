using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Static.Client.Abstraction;
using Elwark.Static.Client.Implementation;
using Elwark.Static.Client.Model;

namespace Elwark.Static.Client.Endpoints
{
    internal class ImageEndpoint : IImageEndpoint
    {
        private const string Section = "image";
        private readonly HttpClient _client;

        public ImageEndpoint(HttpClient client)
        {
            _client = client;
        }

        public IImage GetRandom(ImageOrientation orientation) =>
            new Image(_client, $"{Section}/random?orientation={orientation.ToString().ToLower()}");

        public IImage GetRandom(ImageResolution resolution, ImageOrientation orientation) =>
            new Image(_client, $"{Section}/random/{resolution.ToString().ToLower()}?orientation={orientation.ToString().ToLower()}");

        public IImage GetRandom(uint width, uint height) =>
            new Image(_client, $"{Section}/random/{width}/{height}");

        public async Task<IReadOnlyCollection<Uri>> GetAdminImagesAsync(CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/admin", cancellationToken);

            return await response.Convert<Uri[]>();
        }
    }
}