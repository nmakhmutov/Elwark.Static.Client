// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace World.Api.Models;

public sealed class TimeZone
{
    public TimeZone(string name, TimeSpan utcOffset)
    {
        Id = 0;
        Name = name;
        UtcOffset = utcOffset;
        Translations = [];
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public TimeSpan UtcOffset { get; private set; }

    public HashSet<TimeZoneTranslation> Translations { get; private set; }

    public void AddTranslation(string language, string standardName, string displayName) =>
        Translations.Add(new TimeZoneTranslation(Id, language, standardName, displayName));
}
