using System.Net.Http;
using Elwark.Storage.Client.Abstraction;
using Elwark.Storage.Client.Implementation;

namespace Elwark.Storage.Client.Endpoints
{
    internal class StaticEndpoint : IStaticEndpoint
    {
        private const string Section = "static";
        private readonly Icons _icons;

        public StaticEndpoint(HttpClient client)
        {
            _icons = new Icons(client, Section);
        }

        public IIcons Icons => _icons;
    }
}