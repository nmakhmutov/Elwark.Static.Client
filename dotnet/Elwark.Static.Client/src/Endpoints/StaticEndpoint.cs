using System.Net.Http;
using Elwark.Static.Client.Abstraction;
using Elwark.Static.Client.Implementation;

namespace Elwark.Static.Client.Endpoints
{
    internal class StaticEndpoint : IStaticEndpoint
    {
        private const string Section = "static/images";
        private readonly Icons _icons;

        public StaticEndpoint(HttpClient client)
        {
            _icons = new Icons(client, Section);
        }

        public IIcons Icons => _icons;
    }
}