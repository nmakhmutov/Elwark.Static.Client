// ReSharper disable UnusedMember.Local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace World.Api.Models;

public sealed class Country
{
#pragma warning disable CS8618
    private Country()
    {
    }
#pragma warning restore CS8618

    public static Country Create(int id, string alpha2, string alpha3, Uri flag, string region, string? subregion,
        DayOfWeek startOfWeek)
    {
        if (!flag.IsAbsoluteUri)
            throw new ArgumentException("Flag must be absolute url", nameof(flag));

        return new Country(
            id,
            alpha2.ToUpperInvariant(),
            alpha3.ToUpperInvariant(),
            flag.ToString(),
            region,
            subregion,
            startOfWeek
        );
    }

    private Country(int id, string alpha2, string alpha3, string flag, string region, string? subregion,
        DayOfWeek startOfWeek)
    {
        Id = id;
        Alpha2 = alpha2;
        Alpha3 = alpha3;
        Flag = flag;
        Region = region;
        Subregion = subregion;
        StartOfWeek = startOfWeek;
        Languages = Array.Empty<string>();
        Currencies = Array.Empty<string>();
        Translations = new HashSet<CountryTranslation>();
    }

    public int Id { get; private set; }

    public string Alpha2 { get; private set; }

    public string Alpha3 { get; private set; }

    public string Region { get; private set; }

    public string? Subregion { get; private set; }

    public DayOfWeek StartOfWeek { get; private set; }

    public string Flag { get; private set; }

    public string[] Languages { get; private set; }

    public string[] Currencies { get; private set; }

    public HashSet<CountryTranslation> Translations { get; private set; }

    public void AddTranslation(string language, string common, string official) =>
        Translations.Add(new CountryTranslation(Id, language, common, official));

    public void AddLanguage(string language)
    {
        if (Languages.Contains(language) || language.Length != 2)
            return;

        Languages = Languages.Append(language.ToUpperInvariant()).ToArray();
    }

    public void AddCurrency(string currency)
    {
        if (Currencies.Contains(currency) || currency.Length != 3)
            return;

        Currencies = Currencies.Append(currency.ToUpperInvariant()).ToArray();
    }

    public override string ToString() =>
        $"({Alpha2}) {Translations.FirstOrDefault()?.Common}";
}
