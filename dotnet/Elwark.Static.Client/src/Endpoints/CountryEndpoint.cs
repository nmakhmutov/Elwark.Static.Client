using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Static.Client.Abstraction;
using Elwark.Static.Client.Model;
using Newtonsoft.Json;

namespace Elwark.Static.Client.Endpoints
{
    internal class CountryEndpoint : ICountryEndpoint
    {
        private const string Section = "country";

        private readonly HttpClient _client;

        public CountryEndpoint(HttpClient client) =>
            _client = client;

        public async Task<IReadOnlyCollection<Country>> GetAllAsync(CancellationToken cancellationToken)
        {
            var response = await _client.GetStringAsync(Section);
            return JsonConvert.DeserializeObject<Country[]>(response);
        }

        public async Task<Country> GetByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/code/{code}", cancellationToken);

            return await response.Convert<Country>();
        }

        public async Task<Country> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/name/{name}", cancellationToken);

            return await response.Convert<Country>();
        }

        public async Task<Country> GetByCapitalAsync(string capital, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/capital/{capital}", cancellationToken);

            return await response.Convert<Country>();
        }

        public async Task<IReadOnlyCollection<Country>> GetByRegionAsync(string region, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/region/{region}", cancellationToken);

            return await response.Convert<Country[]>();
        }
        
        public async Task<IReadOnlyCollection<Country>> GetByCurrencyAsync(string currency, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/currency/{currency}", cancellationToken);

            return await response.Convert<Country[]>();
        }
    }
}