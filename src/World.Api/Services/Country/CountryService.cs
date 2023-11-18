using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Npgsql;

namespace World.Api.Services.Country;

internal sealed class CountryService
{
    private static readonly ConcurrentBag<string> Languages = new();
    private readonly string _connection;

    public CountryService(string connection) =>
        _connection = connection;

    public async IAsyncEnumerable<CountryOverview> GetAsync(CultureInfo culture,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        var language = await GetLanguageOrDefault(culture.TwoLetterISOLanguageName)
            .ConfigureAwait(false);

        const string sql =
            """
            SELECT c.alpha2,
                   c.alpha3,
                   c.region,
                   ctr.common
            FROM countries c
                     LEFT JOIN country_translations ctr ON c.id = ctr.country_id AND ctr.language = $1
            ORDER BY ctr.common
            """;

        await using var source = NpgsqlDataSource.Create(_connection);
        await using var command = source.CreateCommand(sql);
        command.Parameters.AddWithValue(language);

        await using var reader = await command.ExecuteReaderAsync(ct)
            .ConfigureAwait(false);

        while (await reader.ReadAsync(ct).ConfigureAwait(false))
        {
            ct.ThrowIfCancellationRequested();
            yield return new CountryOverview(
                reader.GetString(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3)
            );
        }
    }

    public async Task<CountryDetails?> GetAsync(string code, CultureInfo culture, CancellationToken ct = default)
    {
        if (code.Length is < 2 or > 3)
            return null;

        var language = await GetLanguageOrDefault(culture.TwoLetterISOLanguageName)
            .ConfigureAwait(false);

        var sb = new StringBuilder(
            """
            SELECT c.id,
                   c.alpha2,
                   c.alpha3,
                   c.region,
                   ctr.common,
                   ctr.official,
                   c.flag,
                   c.languages,
                   c.currencies
            FROM countries c
                     LEFT JOIN country_translations ctr ON c.id = ctr.country_id AND ctr.language = $1
            """
        );

        sb.Append(code.Length == 2 ? " WHERE c.alpha2 = $2 LIMIT 1;" : " WHERE c.alpha3 = $2 LIMIT 1;");

        await using var source = NpgsqlDataSource.Create(_connection);
        await using var command = source.CreateCommand(sb.ToString());
        command.Parameters.AddWithValue(language);
        command.Parameters.AddWithValue(code);

        await using var reader = await command.ExecuteReaderAsync(ct)
            .ConfigureAwait(false);

        if (await reader.ReadAsync(ct).ConfigureAwait(false))
            return new CountryDetails(
                reader.GetFieldValue<int>(0).ToString().PadLeft(3, '0'),
                reader.GetFieldValue<string>(1),
                reader.GetFieldValue<string>(2),
                reader.GetFieldValue<string>(3),
                reader.GetFieldValue<string>(4),
                reader.GetFieldValue<string>(5),
                reader.GetFieldValue<string>(6),
                reader.GetFieldValue<string[]>(7),
                reader.GetFieldValue<string[]>(8)
            );

        return null;
    }

    private async ValueTask<string> GetLanguageOrDefault(string language)
    {
        if (Languages.IsEmpty)
        {
            await using var source = NpgsqlDataSource.Create(_connection);
            await using var command = source.CreateCommand("SELECT DISTINCT language FROM country_translations");

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
                Languages.Add(reader.GetString(0));
        }

        return Languages.Contains(language, StringComparer.OrdinalIgnoreCase) ? language.ToUpperInvariant() : "EN";
    }
}
