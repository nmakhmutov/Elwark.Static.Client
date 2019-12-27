using Newtonsoft.Json;

namespace Elwark.Storage.Client.Model
{
    public sealed class Currency
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("decimalDigits")]
        public byte DecimalDigits { get; set; }

        [JsonProperty("name")]
        public CurrencyName Name { get; set; }

        [JsonProperty("symbol")]
        public Symbol Symbol { get; set; }
    }

    public sealed class CurrencyName
    {
        [JsonProperty("singular")]
        public string Singular { get; set; }

        [JsonProperty("plural")]
        public string Plural { get; set; }
    }

    public sealed class Symbol
    {
        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("native")]
        public string Native { get; set; }
    }
}