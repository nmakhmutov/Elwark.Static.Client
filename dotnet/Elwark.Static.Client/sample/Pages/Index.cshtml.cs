using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elwark.Static.Client.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elwark.Static.Client.Sample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IElwarkStaticClient _staticClient;

        public IndexModel(IElwarkStaticClient staticClient)
        {
            _staticClient = staticClient;
        }

        public IReadOnlyCollection<string> Passwords { get; set; } = ArraySegment<string>.Empty;
        public IReadOnlyCollection<string> Emails { get; set; } = ArraySegment<string>.Empty;

        public IStaticEndpoint Static =>
            _staticClient.Static;

        public IImageEndpoint Image =>
            _staticClient.Images;

        public async Task OnGetAsync()
        {
            Passwords = await _staticClient.Blacklist.GetPasswordsAsync();
            Emails = await _staticClient.Blacklist.GetEmailDomainsAsync();
        }
    }
}