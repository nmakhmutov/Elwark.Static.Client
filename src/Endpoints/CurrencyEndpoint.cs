using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Storage.Client.Abstraction;
using Elwark.Storage.Client.Model;

namespace Elwark.Storage.Client.Endpoints
{
    internal class CurrencyEndpoint : ICurrencyEndpoint
    {
        private const string Section = "currencies";
        private readonly HttpClient _client;

        public CurrencyEndpoint(HttpClient client) =>
            _client = client;

        public async Task<IReadOnlyCollection<Currency>> GetAllAsync(CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync(Section, cancellationToken);

            return await response.Convert(Array.Empty<Currency>);
        }

        public async Task<Currency?> GetByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/code/{code}", cancellationToken);

            return await response.Convert<Currency?>(() => null);
        }

        public async Task<Currency?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/name/{name}", cancellationToken);

            return await response.Convert<Currency?>(() => null);
        }
    }
}