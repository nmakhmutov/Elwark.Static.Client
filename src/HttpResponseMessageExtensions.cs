using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Elwark.Storage.Client
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task<T> Convert<T>(this HttpResponseMessage message, Func<T> empty) =>
            message.StatusCode switch
            {
                HttpStatusCode.OK => JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync()),
                HttpStatusCode.NotFound => empty(),
                _ => EnsureSuccessStatusCode(message, empty)
            };

        private static T EnsureSuccessStatusCode<T>(HttpResponseMessage message, Func<T> empty)
        {
            message.EnsureSuccessStatusCode();
            return empty();
        }
    }
}