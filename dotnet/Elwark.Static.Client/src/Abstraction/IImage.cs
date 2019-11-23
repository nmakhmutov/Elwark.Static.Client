using System;
using System.IO;
using System.Threading.Tasks;

namespace Elwark.Static.Client.Abstraction
{
    public interface IImage
    {
        Uri Path { get; }

        Task<Stream> GetStreamAsync();
        
        Task<byte[]> GetBytesAsync();
    }
}