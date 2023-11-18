using System.Runtime.CompilerServices;
using Npgsql;

namespace World.Api.Services.TimeZone;

internal sealed class TimeZoneService
{
    private readonly string _connection;

    public TimeZoneService(string connection) =>
        _connection = connection;

    public async IAsyncEnumerable<TimeZoneOverview> GetAsync(string language,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        var sql = language == "en"
            ? """
              SELECT tz.name, tzt.display_name
              FROM time_zones tz
                       LEFT JOIN time_zone_translations tzt ON tz.id = tzt.time_zone_id AND tzt.language = $1
              ORDER BY tz.id
              """
            : """
              SELECT tz.name, COALESCE(tzt.display_name, tzd.display_name)
              FROM time_zones tz
                       LEFT JOIN time_zone_translations tzt ON tz.id = tzt.time_zone_id AND tzt.language = $1
                       LEFT JOIN time_zone_translations tzd ON tz.id = tzd.time_zone_id AND tzd.language = 'en'
              ORDER BY tz.id
              """;

        await using var source = NpgsqlDataSource.Create(_connection);
        await using var command = source.CreateCommand(sql);
        command.Parameters.AddWithValue(language);

        await using var reader = await command.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            ct.ThrowIfCancellationRequested();
            yield return new TimeZoneOverview(reader.GetString(0), reader.GetString(1));
        }
    }

    public async Task<TimeZoneDetails?> GetAsync(string id, string language, CancellationToken ct = default)
    {
        var sql = language == "en"
            ? """
              SELECT tz.name,
                     tz.utc_offset,
                     tzt.standard_name,
                     tzt.display_name
              FROM time_zones tz
                       LEFT JOIN time_zone_translations tzt ON tz.id = tzt.time_zone_id AND tzt.language = $1
              WHERE tz.name = $2
              LIMIT 1
              """
            : """
              SELECT tz.name,
                     tz.utc_offset,
                     COALESCE(tzt.standard_name, tzd.standard_name) AS standard_name,
                     COALESCE(tzt.display_name, tzd.display_name) AS display_name
              FROM time_zones tz
                       LEFT JOIN time_zone_translations tzt ON tz.id = tzt.time_zone_id AND tzt.language = $1
                       LEFT JOIN time_zone_translations tzd ON tz.id = tzd.time_zone_id AND tzd.language = 'en'
              WHERE tz.name = $2
              LIMIT 1
              """;

        await using var source = NpgsqlDataSource.Create(_connection);
        await using var command = source.CreateCommand(sql);
        command.Parameters.AddWithValue(language);
        command.Parameters.AddWithValue(id);

        await using var reader = await command.ExecuteReaderAsync(ct);
        if (await reader.ReadAsync(ct))
            return new TimeZoneDetails(
                reader.GetString(0),
                reader.GetTimeSpan(1),
                reader.GetString(2),
                reader.GetString(3)
            );

        return null;
    }
}
