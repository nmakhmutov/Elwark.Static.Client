using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Elwark.Static.Client
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task<T> Convert<T>(this HttpResponseMessage message)
        {
            switch (message.StatusCode)
            {
                case HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());

                case HttpStatusCode.NotFound:
                    return default;

                default:
                    message.EnsureSuccessStatusCode();
                    return default;
            }
        }
    }
}