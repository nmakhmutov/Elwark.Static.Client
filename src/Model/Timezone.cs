using Newtonsoft.Json;

namespace Elwark.Storage.Client.Model
{
    public sealed class Timezone
    {
        public Timezone(string zone, CountryCode country, Gmt gmt)
        {
            Zone = zone;
            Country = country;
            Gmt = gmt;
        }

        [JsonProperty("zone")]
        public string Zone { get; set; }

        [JsonProperty("country")]
        public CountryCode Country { get; set; }

        [JsonProperty("gmt")]
        public Gmt Gmt { get; set; }
    }

    public sealed class CountryCode
    {
        public CountryCode(string alpha2, string alpha3)
        {
            Alpha2 = alpha2;
            Alpha3 = alpha3;
        }

        [JsonProperty("alpha2")]
        public string Alpha2 { get; set; }

        [JsonProperty("alpha3")]
        public string Alpha3 { get; set; }
    }

    public sealed class Gmt
    {
        public Gmt(string offset, string name)
        {
            Offset = offset;
            Name = name;
        }

        [JsonProperty("offset")]
        public string Offset { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}