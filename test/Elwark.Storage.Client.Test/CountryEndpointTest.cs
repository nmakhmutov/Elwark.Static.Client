using System.Threading.Tasks;
using Elwark.Storage.Client.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Elwark.Storage.Client.Test
{
    public class CountryEndpointTest : BaseTest
    {
        [Fact]
        public async Task Check_all_countries_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            var result = await client.Country.GetAllAsync();

            Assert.NotNull(result);

            foreach (var country in result)
            {
                Assert.NotNull(country.Name);
                Assert.NotNull(country.Currencies);
                Assert.NotNull(country.Borders);
                Assert.NotNull(country.Flag);
                Assert.NotNull(country.Languages);
                Assert.NotNull(country.Timezones);
                Assert.NotNull(country.Translations);
                Assert.NotNull(country.CallingCodes);
                Assert.NotNull(country.RegionalBlocs);
                Assert.NotNull(country.TopLevelDomain);
            }
        }

        [Fact]
        public async Task Check_by_2alpha_country_code_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            const string code = "us";
            var result = await client.Country.GetByCodeAsync(code);

            Assert.NotNull(result);
            Assert.Equal("US", result.Alpha2Code);
            Assert.Equal("USA", result.Alpha3Code);
            Assert.Collection(result.Currencies, s => Assert.Equal("USD", s));
        }

        [Fact]
        public async Task Check_by_3alpha_country_code_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            const string code = "prt";
            var result = await client.Country.GetByCodeAsync(code);

            Assert.NotNull(result);
            Assert.Equal("PT", result.Alpha2Code);
            Assert.Equal("PRT", result.Alpha3Code);
            Assert.Collection(result.Currencies, s => Assert.Equal("EUR", s));
        }

        [Fact]
        public async Task Check_by_capital_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            const string capital = "London";
            var result = await client.Country.GetByCapitalAsync(capital);

            Assert.NotNull(result);
            Assert.Equal(capital, result.Capital);
        }

        [Fact]
        public async Task Check_by_codes_countries_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            var codes = new[] {"ru", "us", "gb", "ad"};
            var result = await client.Country.GetByCodesAsync(codes);

            Assert.NotNull(result);
            Assert.True(codes.Length == result.Count);

            foreach (var country in result)
            {
                Assert.NotNull(country.Name);
                Assert.NotNull(country.Currencies);
                Assert.NotNull(country.Borders);
                Assert.NotNull(country.Flag);
                Assert.NotNull(country.Languages);
                Assert.NotNull(country.Timezones);
                Assert.NotNull(country.Translations);
                Assert.NotNull(country.CallingCodes);
                Assert.NotNull(country.RegionalBlocs);
                Assert.NotNull(country.TopLevelDomain);
            }
        }

        [Fact]
        public async Task Check_by_country_name_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            var result = await client.Country.GetByNameAsync("Spain");
            Assert.NotNull(result);
            Assert.Equal("ES", result.Alpha2Code);
            Assert.Equal("ESP", result.Alpha3Code);
        }

        [Fact]
        public async Task Check_by_currency_code_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            var result = await client.Country.GetByCurrencyAsync("EUR");
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Check_by_non_existent_capital_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();
            const string capital = "non_exist_capital_for_test_client";

            var result = await client.Country.GetByCapitalAsync(capital);
            Assert.Null(result);
        }

        [Fact]
        public async Task Check_by_non_existent_country_name_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            var result = await client.Country.GetByNameAsync("non_existent_country_name");
            Assert.Null(result);
        }

        [Fact]
        public async Task Check_by_non_existent_currency_code_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            var result = await client.Country.GetByCurrencyAsync("non_existent_currency_code");
            Assert.Empty(result);
        }

        [Fact]
        public async Task Check_by_non_exists_codes_countries_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            var codes = new[] {"qq", "aa", "dd", "random"};
            var result = await client.Country.GetByCodesAsync(codes);

            Assert.Empty(result);
        }

        [Fact]
        public async Task Check_by_region_code_success()
        {
            var client = Server.Services.GetService<IElwarkStorageClient>();

            var result = await client.Country.GetByRegionAsync(Region.Africa);
            Assert.NotEmpty(result);
        }
    }
}