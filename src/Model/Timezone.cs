using Newtonsoft.Json;

namespace Elwark.Storage.Client.Model
{
    public sealed class Timezone
    {
        [JsonProperty("zone")]
        public string Zone { get; set; }

        [JsonProperty("country")]
        public CountryCode Country { get; set; }

        [JsonProperty("gmt")]
        public Gmt Gmt { get; set; }
    }

    public sealed class CountryCode
    {
        [JsonProperty("alpha2")]
        public string Alpha2 { get; set; }

        [JsonProperty("alpha3")]
        public string Alpha3 { get; set; }
    }

    public sealed class Gmt
    {
        [JsonProperty("offset")]
        public string Offset { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

}