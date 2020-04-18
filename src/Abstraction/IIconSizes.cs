namespace Elwark.Storage.Client.Abstraction
{
    public interface IIconSizes
    {
        IImage Size16x16 { get; }

        IImage Size32x32 { get; }

        IImage Size36x36 { get; }

        IImage Size48x48 { get; }

        IImage Size57x57 { get; }

        IImage Size60x60 { get; }

        IImage Size70x70 { get; }

        IImage Size72x72 { get; }

        IImage Size76x76 { get; }

        IImage Size96x96 { get; }

        IImage Size114x114 { get; }

        IImage Size120x120 { get; }

        IImage Size144x144 { get; }

        IImage Size150x150 { get; }

        IImage Size152x152 { get; }

        IImage Size180x180 { get; }

        IImage Size192x192 { get; }

        IImage Size310x310 { get; }

        IImage Size500x500 { get; }

        IImage Favicon { get; }
    }
}