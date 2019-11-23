using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Elwark.Static.Client.Abstraction;

namespace Elwark.Static.Client.Implementation
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

        public Task<Stream> GetStreamAsync() =>
            _client.GetStreamAsync(Path);

        public Task<byte[]> GetBytesAsync() =>
            _client.GetByteArrayAsync(Path);
    }
}