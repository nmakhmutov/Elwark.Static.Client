// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace World.Api.Models;

public sealed class TimeZoneTranslation
{
    public TimeZoneTranslation(int timeZoneId, string language, string standardName, string displayName)
    {
        Id = 0;
        TimeZoneId = timeZoneId;
        Language = language;
        StandardName = standardName;
        DisplayName = displayName;
    }

    public int Id { get; private set; }

    public int TimeZoneId { get; private set; }

    public string Language { get; private set; }

    public string StandardName { get; private set; }

    public string DisplayName { get; private set; }
}
