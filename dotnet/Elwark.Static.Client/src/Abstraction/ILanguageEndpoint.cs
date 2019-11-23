using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Static.Client.Model;

namespace Elwark.Static.Client.Abstraction
{
    public interface ILanguageEndpoint
    {
        Task<IReadOnlyCollection<Language>> GetPrimariesAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<Language>> GetFullAsync(CancellationToken cancellationToken = default);

        Task<Language> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}