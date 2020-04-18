using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Storage.Client.Abstraction;

namespace Elwark.Storage.Client.Implementation
{
    internal class Image : IImage
    {
        private readonly HttpClient _client;

        public Image(HttpClient client, string path)
        {
            _client = client;
            Path = new Uri(_client.BaseAddress, path);
        }

        public Uri Path { get; }

        public Task<HttpResponseMessage> GetAsync(CancellationToken cancellationToken) =>
            _client.GetAsync(Path, cancellationToken);

        public Task<Stream> GetStreamAsync() =>
            _client.GetStreamAsync(Path);

        public Task<byte[]> GetBytesAsync() =>
            _client.GetByteArrayAsync(Path);
    }
}