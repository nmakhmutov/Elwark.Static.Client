using Newtonsoft.Json;

namespace Elwark.Storage.Client.Model
{
    public sealed class Language
    {
        public Language(CurrencyName name, Iso639 iso639, TypeEnum type, Scope scope)
        {
            Name = name;
            Iso639 = iso639;
            Type = type;
            Scope = scope;
        }

        [JsonProperty("name")]
        public CurrencyName Name { get; set; }

        [JsonProperty("iso639")]
        public Iso639 Iso639 { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("scope")]
        public Scope Scope { get; set; }
    }

    public sealed class Iso639
    {
        public Iso639(string alpha3, string? alpha2B, string? alpha2T, string? alpha1)
        {
            Alpha3 = alpha3;
            Alpha2B = alpha2B;
            Alpha2T = alpha2T;
            Alpha1 = alpha1;
        }

        [JsonProperty("alpha3")]
        public string Alpha3 { get; set; }

        [JsonProperty("alpha2b")]
        public string? Alpha2B { get; set; }

        [JsonProperty("alpha2t")]
        public string? Alpha2T { get; set; }

        [JsonProperty("alpha1")]
        public string? Alpha1 { get; set; }
    }

    public sealed class LanguageName
    {
        public LanguageName(string common, string native)
        {
            Common = common;
            Native = native;
        }

        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("native")]
        public string Native { get; set; }
    }

    public enum Scope
    {
        Individual,
        Macrolanguage,
        Special
    }

    public enum TypeEnum
    {
        Ancient,
        Constructed,
        Extinct,
        Historical,
        Living,
        Special
    }
}