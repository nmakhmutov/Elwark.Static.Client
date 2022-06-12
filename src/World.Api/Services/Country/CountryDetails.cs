namespace World.Api.Services.Country;

internal sealed record CountryDetails(
    string Numeric,
    string Alpha2,
    string Alpha3,
    string Common,
    string Official,
    string Flag
);
