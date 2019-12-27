using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Storage.Client.Model;

namespace Elwark.Storage.Client.Abstraction
{
    public interface ITimezoneEndpoint
    {
        Task<IReadOnlyCollection<Timezone>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Timezone> GetByTimezoneNameAsync(string name, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<Timezone>> GetByCountryCodeAsync(string code,
            CancellationToken cancellationToken = default);
    }
}