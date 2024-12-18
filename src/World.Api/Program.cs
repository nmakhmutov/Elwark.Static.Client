using System.Globalization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;
using Serilog;
using World.Api.Endpoints;
using World.Api.Infrastructure;
using World.Api.Services.Country;
using World.Api.Services.TimeZone;

const string appName = "Worlds.Api";
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors()
    .AddOutputCache(options => options.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(1))
    .AddRequestLocalization(options =>
    {
        var cultures = new List<CultureInfo>
        {
            new("en"),
            new("ru"),
            new("es"),
            new("fr"),
            new("de")
        };

        options.DefaultRequestCulture = new RequestCulture("en");

        options.SupportedCultures = cultures;
        options.SupportedUICultures = cultures;
        options.RequestCultureProviders = new List<IRequestCultureProvider>
        {
            new QueryStringRequestCultureProvider
            {
                QueryStringKey = "language",
                UIQueryStringKey = "language"
            },
            new AcceptLanguageHeaderRequestCultureProvider()
        };
    });

builder.Services
    .AddStackExchangeRedisCache(options => options.Configuration = builder.Configuration.GetConnectionString("Redis"))
    .AddHybridCache(options => options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromHours(1),
        LocalCacheExpiration = TimeSpan.FromMinutes(30)
    });

var postgresql = builder.Configuration.GetConnectionString("Postgresql")!;

builder.Services
    .AddSingleton(provider => new CountryService(postgresql, provider.GetRequiredService<HybridCache>()))
    .AddSingleton(provider => new TimeZoneService(postgresql, provider.GetRequiredService<HybridCache>()))
    .AddDbContext<WorldDbContext>(options => options.UseNpgsql(postgresql));

builder.Host
    .UseSerilog((context, configuration) => configuration
        .Enrich.WithProperty("ApplicationName", appName)
        .ReadFrom.Configuration(context.Configuration)
    );

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WorldDbContext>();
    await dbContext.Database.MigrateAsync();

    var options = scope.ServiceProvider.GetRequiredService<IOptions<RequestLocalizationOptions>>();
    await new WorldDbContextSeed(dbContext, options.Value.SupportedCultures!)
        .SeedAsync();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
        ForwardLimit = 10
    })
    .UseCors(policy => policy
        .WithOrigins(builder.Configuration.GetRequiredSection("Cors").Get<string[]>()!)
        .WithMethods(HttpMethods.Get)
        .AllowAnyHeader()
    )
    .UseRequestLocalization()
    .UseOutputCache();

app.MapCountryEndpoints()
    .MapTimeZoneEndpoints();

await app.RunAsync();
