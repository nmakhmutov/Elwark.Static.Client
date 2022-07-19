// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace World.Api.Models;

public sealed class Country
{
    // ReSharper disable once UnusedMember.Local
    private Country(Uri flag)
    {
        Flag = flag;
        StartOfWeek = DayOfWeek.Monday;
        Alpha2 = Alpha3 = Region = Subregion = string.Empty;
        Translations = new HashSet<CountryTranslation>();
    }

    public Country(int id, string alpha2, string alpha3, Uri flag, string region, string? subregion,
        DayOfWeek startOfWeek)
    {
        Id = id;
        Alpha2 = alpha2.ToUpperInvariant();
        Alpha3 = alpha3.ToUpperInvariant();
        Flag = flag;
        Region = region;
        Subregion = subregion;
        StartOfWeek = startOfWeek;
        Translations = new HashSet<CountryTranslation>();
    }

    public int Id { get; private set; }

    public string Alpha2 { get; private set; }

    public string Alpha3 { get; private set; }

    public string Region { get; private set; }

    public string? Subregion { get; private set; }

    public DayOfWeek StartOfWeek { get; private set; }

    public Uri Flag { get; private set; }

    public HashSet<CountryTranslation> Translations { get; private set; }

    public void AddTranslation(string language, string common, string official) =>
        Translations.Add(new CountryTranslation(Id, language.ToLowerInvariant(), common, official));

    public override string ToString() =>
        $"({Alpha2}) {Translations.First().Common}";
}
