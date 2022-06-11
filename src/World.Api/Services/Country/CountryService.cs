using System.Runtime.CompilerServices;
using Npgsql;

namespace World.Api.Services.Country;

internal sealed class CountryService
{
    private readonly string _connection;

    public CountryService(string connection) =>
        _connection = connection;

    public async IAsyncEnumerable<CountryOverview> GetAsync(string language,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        const string sql = @"
SELECT c.alpha2, COALESCE(ctr.common, ctd.common) AS name
FROM countries c
         LEFT JOIN country_translations ctr ON c.id = ctr.country_id AND ctr.language = $1
         LEFT JOIN country_translations ctd ON c.id = ctd.country_id AND ctd.language = 'en'
ORDER BY name;
";

        await using var connection = new NpgsqlConnection(_connection);
        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters = { new NpgsqlParameter { Value = language } }
        };

        await connection.OpenAsync(ct);

        await using var reader = await command.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            ct.ThrowIfCancellationRequested();
            yield return new CountryOverview(reader.GetString(0), reader.GetString(1));
        }
    }

    public async Task<CountryOverview?> GetAsync(string code, string language, CancellationToken ct = default)
    {
        const string sql = @"
SELECT c.alpha2, COALESCE(ctr.common, ctd.common) AS name
FROM countries c
         LEFT JOIN country_translations ctr ON c.id = ctr.country_id AND ctr.language = $1
         LEFT JOIN country_translations ctd ON c.id = ctd.country_id AND ctd.language = 'en'
WHERE c.alpha2 = $1
LIMIT 1;
";

        await using var connection = new NpgsqlConnection(_connection);
        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter { Value = language },
                new NpgsqlParameter { Value = code }
            }
        };

        await connection.OpenAsync(ct);

        await using var reader = await command.ExecuteReaderAsync(ct);
        if (await reader.ReadAsync(ct))
            return new CountryOverview(reader.GetString(0), reader.GetString(1));

        return null;
    }
}
