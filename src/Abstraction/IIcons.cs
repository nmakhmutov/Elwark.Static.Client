namespace Elwark.Storage.Client.Abstraction
{
    public interface IIcons
    {
        IUserIcon User { get; }

        IElwarkIcons Elwark { get; }
    }
}