using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Static.Client.Model;

namespace Elwark.Static.Client.Abstraction
{
    public interface IImageEndpoint
    {
        IImage GetRandom(ImageOrientation orientation = ImageOrientation.Landscape);

        IImage GetRandom(ImageResolution resolution, ImageOrientation orientation = ImageOrientation.Landscape);

        IImage GetRandom(uint width, uint height);

        Task<IReadOnlyCollection<Uri>> GetAdminImagesAsync(CancellationToken cancellationToken = default);
    }
}