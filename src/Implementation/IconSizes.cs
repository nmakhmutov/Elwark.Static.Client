using System.IO;
using System.Net.Http;
using Elwark.Storage.Client.Abstraction;

namespace Elwark.Storage.Client.Implementation
{
    internal class IconSizes : IIconSizes
    {
        private readonly Image _size16X16;
        private readonly Image _size32X32;
        private readonly Image _size36X36;
        private readonly Image _size48X48;
        private readonly Image _size57X57;
        private readonly Image _size60X60;
        private readonly Image _size70X70;
        private readonly Image _size72X72;
        private readonly Image _size76X76;
        private readonly Image _size96X96;
        private readonly Image _size114X114;
        private readonly Image _size120X120;
        private readonly Image _size144X144;
        private readonly Image _size150X150;
        private readonly Image _size152X152;
        private readonly Image _size180X180;
        private readonly Image _size192X192;
        private readonly Image _size310X310;
        private readonly Image _size500X500;
        private readonly Image _favicon;

        public IconSizes(HttpClient client, string path)
        {
            _size16X16 = new Image(client, Path.Combine(path, "16x16.png"));
            _size32X32 = new Image(client, Path.Combine(path, "32x32.png"));
            _size36X36 = new Image(client, Path.Combine(path, "36x36.png"));
            _size48X48 = new Image(client, Path.Combine(path, "48x48.png"));
            _size57X57 = new Image(client, Path.Combine(path, "57x57.png"));
            _size60X60 = new Image(client, Path.Combine(path, "60x60.png"));
            _size70X70 = new Image(client, Path.Combine(path, "70x70.png"));
            _size72X72 = new Image(client, Path.Combine(path, "72x72.png"));
            _size76X76 = new Image(client, Path.Combine(path, "76x76.png"));
            _size96X96 = new Image(client, Path.Combine(path, "96x96.png"));
            _size114X114 = new Image(client, Path.Combine(path, "114x114.png"));
            _size120X120 = new Image(client, Path.Combine(path, "120x120.png"));
            _size144X144 = new Image(client, Path.Combine(path, "144x144.png"));
            _size150X150 = new Image(client, Path.Combine(path, "150x150.png"));
            _size152X152 = new Image(client, Path.Combine(path, "152x152.png"));
            _size180X180 = new Image(client, Path.Combine(path, "180x180.png"));
            _size192X192 = new Image(client, Path.Combine(path, "192x192.png"));
            _size310X310 = new Image(client, Path.Combine(path, "310x310.png"));
            _size500X500 = new Image(client, Path.Combine(path, "500x500.png"));
            _favicon = new Image(client, Path.Combine(path, "favicon.ico"));
        }

        public IImage Size16x16 =>
            _size16X16;

        public IImage Size32x32 =>
            _size32X32;

        public IImage Size36x36 =>
            _size36X36;

        public IImage Size48x48 =>
            _size48X48;

        public IImage Size57x57 =>
            _size57X57;

        public IImage Size60x60 =>
            _size60X60;

        public IImage Size70x70 =>
            _size70X70;

        public IImage Size72x72 =>
            _size72X72;

        public IImage Size76x76 =>
            _size76X76;

        public IImage Size96x96 =>
            _size96X96;

        public IImage Size114x114 =>
            _size114X114;

        public IImage Size120x120 =>
            _size120X120;

        public IImage Size144x144 =>
            _size144X144;

        public IImage Size150x150 =>
            _size150X150;

        public IImage Size152x152 =>
            _size152X152;

        public IImage Size180x180 =>
            _size180X180;

        public IImage Size192x192 =>
            _size192X192;

        public IImage Size310x310 =>
            _size310X310;

        public IImage Size500x500 =>
            _size500X500;

        public IImage Favicon =>
            _favicon;
    }
}