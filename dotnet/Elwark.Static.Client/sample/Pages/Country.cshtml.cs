using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elwark.Static.Client.Abstraction;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elwark.Static.Client.Sample.Pages
{
    public class Country : PageModel
    {
        private readonly IElwarkStaticClient _staticClient;

        public Country(IElwarkStaticClient staticClient)
        {
            _staticClient = staticClient;
        }

        public IReadOnlyCollection<Model.Country> Countries { get; set; }
        public Model.Country Capital { get; set; }
        public Model.Country CountryCode { get; set; }
        public IReadOnlyCollection<Model.Country> Currency { get; set; }
        public Model.Country CountryName { get; set; }
        public IReadOnlyCollection<Model.Country> Regions { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Countries = await _staticClient.Country.GetAllAsync(cancellationToken);
            Capital = await _staticClient.Country.GetByCapitalAsync("London", cancellationToken);
            CountryCode = await _staticClient.Country.GetByCodeAsync("USA", cancellationToken);
            Currency = await _staticClient.Country.GetByCurrencyAsync("eur", cancellationToken);
            CountryName = await _staticClient.Country.GetByNameAsync("France", cancellationToken);
            Regions = await _staticClient.Country.GetByRegionAsync("Europe", cancellationToken);
        }
    }
}