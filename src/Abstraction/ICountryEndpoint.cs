using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Storage.Client.Model;

namespace Elwark.Storage.Client.Abstraction
{
    public interface ICountryEndpoint
    {
        Task<IReadOnlyCollection<Country>> GetAllAsync(CancellationToken cancellationToken = default);
        
        Task<Country> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        
        Task<Country> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        
        Task<Country> GetByCapitalAsync(string capital, CancellationToken cancellationToken = default);
        
        Task<IReadOnlyCollection<Country>> GetByRegionAsync(string region, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<Country>> GetByCurrencyAsync(string currency,
            CancellationToken cancellationToken = default);
    }
}