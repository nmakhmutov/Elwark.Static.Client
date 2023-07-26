using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Npgsql;

namespace World.Api.Services.Country;

internal sealed class CountryService
{
    private readonly string _connection;

    public CountryService(string connection) =>
        _connection = connection;

    public async IAsyncEnumerable<CountryOverview> GetAsync(CultureInfo culture,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        var language = culture.TwoLetterISOLanguageName.ToUpperInvariant();

        var sql = language == "EN"
            ? """
                SELECT c.alpha2, ctr.common
                FROM countries c
                         LEFT JOIN country_translations ctr ON c.id = ctr.country_id AND ctr.language = $1
                ORDER BY ctr.common
                """
            : """
                SELECT c.alpha2, COALESCE(ctr.common, ctd.common) AS name
                FROM countries c
                         LEFT JOIN country_translations ctr ON c.id = ctr.country_id AND ctr.language = $1
                         LEFT JOIN country_translations ctd ON c.id = ctd.country_id AND ctd.language = 'EN'
                ORDER BY name
                """;

        await using var source = NpgsqlDataSource.Create(_connection);
        await using var command = source.CreateCommand(sql);
        command.Parameters.AddWithValue(language);

        await using var reader = await command.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            ct.ThrowIfCancellationRequested();
            yield return new CountryOverview(reader.GetString(0), reader.GetString(1));
        }
    }

    public async Task<CountryDetails?> GetAsync(string code, CultureInfo culture, CancellationToken ct = default)
    {
        if (code.Length is < 2 or > 3)
            return null;

        var language = culture.TwoLetterISOLanguageName.ToUpperInvariant();

        var sb = new StringBuilder(language == "EN"
            ? """
                SELECT c.id,
                       c.alpha2,
                       c.alpha3,
                       c.continent,
                       c.region,
                       ctr.common,
                       ctr.official,
                       c.flag,
                       c.languages,
                       c.currencies
                FROM countries c
                         LEFT JOIN country_translations ctr ON c.id = ctr.country_id AND ctr.language = $1
                """
            : """
                SELECT c.id,
                       c.alpha2,
                       c.alpha3,
                       c.continent,
                       c.region,
                       COALESCE(ctr.common, ctd.common)     AS common,
                       COALESCE(ctr.official, ctd.official) AS official,
                       c.flag,
                       c.languages,
                       c.currencies
                FROM countries c
                         LEFT JOIN country_translations ctr ON c.id = ctr.country_id AND ctr.language = $1
                         LEFT JOIN country_translations ctd ON c.id = ctd.country_id AND ctd.language = 'EN'
                """);

        sb.Append(code.Length == 2 ? " WHERE c.alpha2 = $2 LIMIT 1;" : " WHERE c.alpha3 = $2 LIMIT 1;");

        await using var source = NpgsqlDataSource.Create(_connection);
        await using var command = source.CreateCommand(sb.ToString());
        command.Parameters.AddWithValue(language);
        command.Parameters.AddWithValue(code);

        await using var reader = await command.ExecuteReaderAsync(ct);
        if (await reader.ReadAsync(ct))
            return new CountryDetails(
                reader.GetFieldValue<int>(0).ToString().PadLeft(3, '0'),
                reader.GetFieldValue<string>(1),
                reader.GetFieldValue<string>(2),
                reader.GetFieldValue<string>(3),
                reader.GetFieldValue<string>(4),
                reader.GetFieldValue<string>(5),
                reader.GetFieldValue<string>(6),
                reader.GetFieldValue<string>(7),
                reader.GetFieldValue<string[]>(8),
                reader.GetFieldValue<string[]>(9)
            );

        return null;
    }
}
