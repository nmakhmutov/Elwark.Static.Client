using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Storage.Client.Abstraction;
using Elwark.Storage.Client.Model;

namespace Elwark.Storage.Client.Endpoints
{
    internal class TimezoneEndpoint : ITimezoneEndpoint
    {
        private const string Section = "timezones";
        private readonly HttpClient _client;

        public TimezoneEndpoint(HttpClient client) =>
            _client = client;

        public async Task<IReadOnlyCollection<Timezone>> GetAllAsync(CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync(Section, cancellationToken);

            return await response.Convert(Array.Empty<Timezone>);
        }

        public async Task<Timezone?> GetByTimezoneNameAsync(string name, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/zone/{WebUtility.UrlEncode(name)}", cancellationToken);

            return await response.Convert<Timezone?>(() => null);
        }

        public async Task<IReadOnlyCollection<Timezone>> GetByCountryCodeAsync(string code,
            CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/country/{code}", cancellationToken);

            return await response.Convert(Array.Empty<Timezone>);
        }
    }
}