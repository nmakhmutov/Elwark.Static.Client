using System.IO;
using System.Net.Http;
using Elwark.Static.Client.Abstraction;

namespace Elwark.Static.Client.Implementation
{
    internal class IconSizes : IIconSizes
    {
        private readonly Image _square16;
        private readonly Image _square32;
        private readonly Image _square36;
        private readonly Image _square48;
        private readonly Image _square57;
        private readonly Image _square60;
        private readonly Image _square70;
        private readonly Image _square72;
        private readonly Image _square76;
        private readonly Image _square96;
        private readonly Image _square114;
        private readonly Image _square120;
        private readonly Image _square144;
        private readonly Image _square150;
        private readonly Image _square152;
        private readonly Image _square180;
        private readonly Image _square192;
        private readonly Image _square310;
        private readonly Image _square500;
        private readonly Image _favicon;

        public IconSizes(HttpClient client, string path)
        {
            _square16 = new Image(client, Path.Combine(path, "16x16.png"));
            _square32 = new Image(client, Path.Combine(path, "32x32.png"));
            _square36 = new Image(client, Path.Combine(path, "36x36.png"));
            _square48 = new Image(client, Path.Combine(path, "48x48.png"));
            _square57 = new Image(client, Path.Combine(path, "57x57.png"));
            _square60 = new Image(client, Path.Combine(path, "60x60.png"));
            _square70 = new Image(client, Path.Combine(path, "70x70.png"));
            _square72 = new Image(client, Path.Combine(path, "72x72.png"));
            _square76 = new Image(client, Path.Combine(path, "76x76.png"));
            _square96 = new Image(client, Path.Combine(path, "96x96.png"));
            _square114 = new Image(client, Path.Combine(path, "114x114.png"));
            _square120 = new Image(client, Path.Combine(path, "120x120.png"));
            _square144 = new Image(client, Path.Combine(path, "144x144.png"));
            _square150 = new Image(client, Path.Combine(path, "150x150.png"));
            _square152 = new Image(client, Path.Combine(path, "152x152.png"));
            _square180 = new Image(client, Path.Combine(path, "180x180.png"));
            _square192 = new Image(client, Path.Combine(path, "192x192.png"));
            _square310 = new Image(client, Path.Combine(path, "310x310.png"));
            _square500 = new Image(client, Path.Combine(path, "500x500.png"));
            _favicon = new Image(client, Path.Combine(path, "favicon.ico"));
        }

        public IImage Square16 =>
            _square16;

        public IImage Square32 =>
            _square32;

        public IImage Square36 =>
            _square36;

        public IImage Square48 =>
            _square48;

        public IImage Square57 =>
            _square57;

        public IImage Square60 =>
            _square60;

        public IImage Square70 =>
            _square70;

        public IImage Square72 =>
            _square72;

        public IImage Square76 =>
            _square76;

        public IImage Square96 =>
            _square96;

        public IImage Square114 =>
            _square114;

        public IImage Square120 =>
            _square120;

        public IImage Square144 =>
            _square144;

        public IImage Square150 =>
            _square150;

        public IImage Square152 =>
            _square152;

        public IImage Square180 =>
            _square180;

        public IImage Square192 =>
            _square192;

        public IImage Square310 =>
            _square310;

        public IImage Square500 =>
            _square500;

        public IImage Favicon =>
            _favicon;
    }
}