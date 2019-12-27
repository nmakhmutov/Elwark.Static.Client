using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Storage.Client.Abstraction;

namespace Elwark.Storage.Client.Endpoints
{
    internal class BlacklistEndpoint : IBlacklistEndpoint
    {
        private const string Section = "blacklist";
        private readonly HttpClient _client;

        public BlacklistEndpoint(HttpClient client)
        {
            _client = client;
        }

        public async Task<IReadOnlyCollection<string>> GetPasswordsAsync(CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/password", cancellationToken);

            return await response.Convert<string[]>();
        }

        public async Task<IReadOnlyCollection<string>> GetEmailDomainsAsync(CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/email", cancellationToken);

            return await response.Convert<string[]>();
        }

        public async Task<bool> IsForbiddenPasswordAsync(string password, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/password/{password}", cancellationToken);

            return await response.Convert<bool>();
        }

        public async Task<bool> IsForbiddenEmailDomainAsync(string domain, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"{Section}/email/{domain}", cancellationToken);

            return await response.Convert<bool>();
        }
    }
}