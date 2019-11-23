using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Elwark.Static.Client.Abstraction
{
    public interface IBlacklistEndpoint
    {
        Task<IReadOnlyCollection<string>> GetPasswordsAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<string>> GetEmailDomainsAsync(CancellationToken cancellationToken = default);

        Task<bool> IsForbiddenPasswordAsync(string password, CancellationToken cancellationToken = default);

        Task<bool> IsForbiddenEmailDomainAsync(string domain, CancellationToken cancellationToken = default);
    }
}