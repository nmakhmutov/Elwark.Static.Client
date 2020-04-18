using Newtonsoft.Json;

namespace Elwark.Storage.Client.Model
{
    public sealed class Currency
    {
        public Currency(string code, byte decimalDigits, CurrencyName name, Symbol symbol)
        {
            Code = code;
            DecimalDigits = decimalDigits;
            Name = name;
            Symbol = symbol;
        }

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
        public CurrencyName(string singular, string plural)
        {
            Singular = singular;
            Plural = plural;
        }

        [JsonProperty("singular")]
        public string Singular { get; set; }

        [JsonProperty("plural")]
        public string Plural { get; set; }
    }

    public sealed class Symbol
    {
        public Symbol(string common, string native)
        {
            Common = common;
            Native = native;
        }

        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("native")]
        public string Native { get; set; }
    }
}