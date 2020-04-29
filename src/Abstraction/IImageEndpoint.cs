using Elwark.Storage.Client.Model;

namespace Elwark.Storage.Client.Abstraction
{
    public interface IImageEndpoint
    {
        IImage GetRandom(ImageOrientation orientation = ImageOrientation.Landscape);

        IImage GetRandom(ImageResolution resolution, ImageOrientation orientation = ImageOrientation.Landscape);

        IImage GetRandom(uint width, uint height);
    }
}