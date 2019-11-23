using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Static.Client.Model;

namespace Elwark.Static.Client.Abstraction
{
    public interface ICurrencyEndpoint
    {
        Task<IReadOnlyCollection<Currency>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Currency> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

        Task<Currency> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}