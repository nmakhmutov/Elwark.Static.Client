using Elwark.Storage.Client.Abstraction;

namespace Elwark.Storage.Client
{
    public interface IElwarkStorageClient
    {
        ICountryEndpoint Country { get; }

        IBlacklistEndpoint Blacklist { get; }

        ICurrencyEndpoint Currency { get; }

        ILanguageEndpoint Language { get; }

        ITimezoneEndpoint Timezone { get; }

        IStaticEndpoint Static { get; }

        IImageEndpoint Images { get; }
    }
}