using System;
using Newtonsoft.Json;

namespace Elwark.Storage.Client.Model
{
    public sealed class Country
    {
        public Country(string alpha2Code, string alpha3Code, string numericCode, double area, string[] borders,
            long[] callingCodes, string capital, string[] currencies, string demonym, Uri flag, bool independent,
            bool landlocked, string[] languages, double latitude, double longitude, CountryName name, Region region,
            string[] timezones, RegionalBloc[] regionalBlocs, string subRegion, string[] topLevelDomain,
            Translation[] translations)
        {
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            NumericCode = numericCode;
            Area = area;
            Borders = borders;
            CallingCodes = callingCodes;
            Capital = capital;
            Currencies = currencies;
            Demonym = demonym;
            Flag = flag;
            Independent = independent;
            Landlocked = landlocked;
            Languages = languages;
            Latitude = latitude;
            Longitude = longitude;
            Name = name;
            Region = region;
            Timezones = timezones;
            RegionalBlocs = regionalBlocs;
            SubRegion = subRegion;
            TopLevelDomain = topLevelDomain;
            Translations = translations;
        }

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

        public override string ToString() =>
            $"{Alpha2Code}: {Name}";
    }

    public sealed class CountryName
    {
        public CountryName(string common, string official, Translation[] native)
        {
            Common = common;
            Official = official;
            Native = native;
        }

        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("official")]
        public string Official { get; set; }

        [JsonProperty("native")]
        public Translation[] Native { get; set; }

        public override string ToString() =>
            Common;
    }

    public sealed class Translation
    {
        public Translation(string language, string common, string official)
        {
            Language = language;
            Common = common;
            Official = official;
        }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("official")]
        public string Official { get; set; }

        public override string ToString() =>
            $"{Language}: {Common}";
    }

    public sealed class RegionalBloc
    {
        public RegionalBloc(string acronym, string name)
        {
            Acronym = acronym;
            Name = name;
        }

        [JsonProperty("acronym")]
        public string Acronym { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public override string ToString() =>
            $"{Acronym}: {Name}";
    }

    public enum Region
    {
        Africa,
        Americas,
        Antarctic,
        Asia,
        Europe,
        Oceania
    }
}