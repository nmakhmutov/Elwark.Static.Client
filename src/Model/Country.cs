using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Elwark.Storage.Client.Model
{
    public sealed class Country
    {
        [JsonProperty("alpha2Code")]
        public string Alpha2Code { get; set; }

        [JsonProperty("alpha3Code")]
        public string Alpha3Code { get; set; }

        [JsonProperty("numericCode")]
        public string NumericCode { get; set; }

        [JsonProperty("area")]
        public double Area { get; set; }

        [JsonProperty("borders")]
        public string[] Borders { get; set; }

        [JsonProperty("callingCodes")]
        public long[] CallingCodes { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("currencies")]
        public string[] Currencies { get; set; }

        [JsonProperty("demonym")]
        public string Demonym { get; set; }

        [JsonProperty("flag")]
        public Uri Flag { get; set; }

        [JsonProperty("independent")]
        public bool Independent { get; set; }

        [JsonProperty("landlocked")]
        public bool Landlocked { get; set; }

        [JsonProperty("languages")]
        public string[] Languages { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("name")]
        public CountryName Name { get; set; }

        [JsonProperty("region")]
        public Region Region { get; set; }

        [JsonProperty("timezones")]
        public string[] Timezones { get; set; }

        [JsonProperty("regionalBlocs")]
        public RegionalBloc[] RegionalBlocs { get; set; }

        [JsonProperty("subregion")]
        public string SubRegion { get; set; }

        [JsonProperty("topLevelDomain")]
        public string[] TopLevelDomain { get; set; }

        [JsonProperty("translations")]
        public Translation[] Translations { get; set; }
    }
    
    public sealed class CountryName
    {
        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("official")]
        public string Official { get; set; }

        [JsonProperty("native")]
        public Translation[] Native { get; set; }
    }
    
    public sealed class Translation
    {
        [JsonProperty("language")]
        public string Language { get; set; }
        
        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("official")]
        public string Official { get; set; }

        public override string ToString() =>
            $"({Language}) {Common}";
    }

    public sealed class RegionalBloc
    {
        [JsonProperty("acronym")]
        public string Acronym { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    
    public enum Region { Africa, Americas, Antarctic, Asia, Europe, Oceania }
}