using System;
using System.Net.Http;
using Elwark.Storage.Client.Abstraction;
using Elwark.Storage.Client.Endpoints;

namespace Elwark.Storage.Client
{
    internal class ElwarkStorageClient : IElwarkStorageClient
    {
        private readonly CountryEndpoint _countryEndpoint;
        private readonly BlacklistEndpoint _blacklistEndpoint;
        private readonly CurrencyEndpoint _currencyEndpoint;
        private readonly LanguageEndpoint _languageEndpoint;
        private readonly TimezoneEndpoint _timezoneEndpoint;
        private readonly StaticEndpoint _staticEndpoint;
        private readonly ImageEndpoint _imageEndpoint;

        public ElwarkStorageClient(HttpClient client)
        {
            if (client == null) 
                throw new ArgumentNullException(nameof(client));
            
            _countryEndpoint = new CountryEndpoint(client);
            _blacklistEndpoint = new BlacklistEndpoint(client);
            _currencyEndpoint = new CurrencyEndpoint(client);
            _languageEndpoint = new LanguageEndpoint(client);
            _timezoneEndpoint = new TimezoneEndpoint(client);
            _staticEndpoint = new StaticEndpoint(client);
            _imageEndpoint = new ImageEndpoint(client);
            
        }

        public ICountryEndpoint Country =>
            _countryEndpoint;

        public IBlacklistEndpoint Blacklist =>
            _blacklistEndpoint;

        public ICurrencyEndpoint Currency =>
            _currencyEndpoint;

        public ILanguageEndpoint Language =>
            _languageEndpoint;

        public ITimezoneEndpoint Timezone =>
            _timezoneEndpoint;

        public IStaticEndpoint Static =>
            _staticEndpoint;

        public IImageEndpoint Images =>
            _imageEndpoint;
    }
}