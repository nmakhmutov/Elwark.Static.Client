// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace World.Api.Models;

public sealed class CountryTranslation
{
    public CountryTranslation() =>
        Language = Official = Common = string.Empty;

    public CountryTranslation(int countryId, string language, string common, string official)
    {
        Id = Guid.Empty;
        CountryId = countryId;
        Language = language.ToUpperInvariant();
        Official = official;
        Common = common;
    }

    public Guid Id { get; private set; }

    public int CountryId { get; private set; }

    public string Language { get; private set; }

    public string Common { get; private set; }

    public string Official { get; private set; }

    public override string ToString() =>
        $"({Language}) {Common}";
}
