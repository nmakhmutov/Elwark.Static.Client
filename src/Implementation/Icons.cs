using System.IO;
using System.Net.Http;
using Elwark.Storage.Client.Abstraction;

namespace Elwark.Storage.Client.Implementation
{
    internal class Icons : IIcons
    {
        private const string Section = "icons";
        private readonly ElwarkIcons _elwarkIcons;
        private readonly UserIcon _userIcon;

        public Icons(HttpClient client, string path)
        {
            var route = Path.Combine(path, Section);
            _userIcon = new UserIcon(client, route);
            _elwarkIcons = new ElwarkIcons(client, route);
        }

        public IUserIcon User => _userIcon;

        public IElwarkIcons Elwark => _elwarkIcons;
    }
}