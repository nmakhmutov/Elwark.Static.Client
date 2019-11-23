using System.IO;
using System.Net.Http;
using Elwark.Static.Client.Abstraction;

namespace Elwark.Static.Client.Implementation
{
    internal class ElwarkIcons : IElwarkIcons
    {
        private const string Section = "elwark";
        private readonly IconSizes _primary;
        private readonly IconSizes _black;
        private readonly IconSizes _white;

        public ElwarkIcons(HttpClient client, string path)
        {
            _primary = new IconSizes(client, Path.Combine(path, Section, "primary"));
            _black = new IconSizes(client, Path.Combine(path, Section, "black"));
            _white = new IconSizes(client, Path.Combine(path, Section, "white"));
        }

        public IIconSizes Primary => _primary;
        
        public IIconSizes Black => _black;
        
        public IIconSizes White => _white;
    }
}