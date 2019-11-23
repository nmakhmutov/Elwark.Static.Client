using Elwark.Static.Client.Abstraction;

namespace Elwark.Static.Client
{
    public interface IElwarkStaticClient
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