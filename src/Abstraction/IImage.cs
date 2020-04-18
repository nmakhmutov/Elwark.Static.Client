using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Elwark.Storage.Client.Abstraction
{
    public interface IImage
    {
        Uri Path { get; }

        Task<HttpResponseMessage> GetAsync(CancellationToken cancellationToken = default);

        Task<Stream> GetStreamAsync();

        Task<byte[]> GetBytesAsync();
    }
}