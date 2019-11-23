using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Static.Client.Abstraction;
using Elwark.Static.Client.Model;

namespace Elwark.Static.Client.Endpoints
{
    internal  class TimezoneEndpoint : ITimezoneEndpoint
    {
        private const string Section = "timezone";
        private readonly HttpClient _client;

        public TimezoneEndpoint(HttpClient client)
        {
            _client = client;
        }

        public async Task<IReadOnlyCollection<Timezone>> GetAllAsync(CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync(Section, cancellationToken);

            return await response.Convert<Timezone[]>();
        }

        public async Task<Timezone> GetByTimezoneNameAsync(string name, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/zone/{name}", cancellationToken);

            return await response.Convert<Timezone>();
        }

        public async Task<IReadOnlyCollection<Timezone>> GetByCountryCodeAsync(string code, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/country/{code}", cancellationToken);

            return await response.Convert<Timezone[]>();
        }
    }
}