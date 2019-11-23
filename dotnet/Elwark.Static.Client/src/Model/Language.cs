using Newtonsoft.Json;

namespace Elwark.Static.Client.Model
{
    public sealed class Language
    {
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
        [JsonProperty("alpha3")]
        public string Alpha3 { get; set; }

        [JsonProperty("alpha2b")]
        public string Alpha2B { get; set; }

        [JsonProperty("alpha2t")]
        public string Alpha2T { get; set; }

        [JsonProperty("alpha1")]
        public string Alpha1 { get; set; }
    }

    public sealed class LanguageName
    {
        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("native")]
        public string Native { get; set; }
    }

    public enum Scope { Individual, Macrolanguage, Special };

    public enum TypeEnum { Ancient, Constructed, Extinct, Historical, Living, Special };
}