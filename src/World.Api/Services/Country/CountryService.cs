using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Caching.Hybrid;
using Npgsql;
using World.Api.Models;

namespace World.Api.Services.Country;

internal sealed class CountryService
{
    private readonly HybridCache _cache;
    private readonly string _connection;

    public CountryService(string connection, HybridCache cache)
    {
        _connection = connection;
        _cache = cache;
    }

    public async IAsyncEnumerable<CountryOverview> GetAsync(
        Language language,
        [EnumeratorCancellation] CancellationToken ct = default
    )
    {
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

        var result = await GetLanguageOrDefault(language, ct);
        var countries = await _cache.GetOrCreateAsync(
            CacheKey.Countries(result),
            async token =>
            {
                await using var source = NpgsqlDataSource.Create(_connection);
                await using var command = source.CreateCommand(sql);
                command.Parameters.AddWithValue(result.ToString());

                await using var reader = await command.ExecuteReaderAsync(token);

                var list = new List<CountryOverview>();
                while (await reader.ReadAsync(token))
                {
                    list.Add(new CountryOverview(
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3)
                    ));
                }

                return list;
            },
            cancellationToken: ct
        );

        foreach (var country in countries)
            yield return country;
    }

    public async Task<CountryDetails?> GetAsync(string code, Language language, CancellationToken ct = default)
    {
        if (code.Length is < 2 or > 3)
            return null;

        var result = await GetLanguageOrDefault(language, ct);

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
        command.Parameters.AddWithValue(result.ToString());
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

    private async Task<Language> GetLanguageOrDefault(Language language, CancellationToken ct)
    {
        var languages = await _cache.GetOrCreateAsync(
            CacheKey.CountryLanguages(),
            async token =>
            {
                await using var source = NpgsqlDataSource.Create(_connection);
                await using var command = source.CreateCommand("SELECT DISTINCT language FROM country_translations");

                await using var reader = await command.ExecuteReaderAsync(token);

                var list = new List<Language>();
                while (await reader.ReadAsync(token))
                    list.Add(new Language(reader.GetString(0)));

                return list.ToHashSet();
            },
            cancellationToken: ct
        );

        return languages.Contains(language) ? language : Language.English;
    }
}
