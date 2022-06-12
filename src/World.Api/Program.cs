using System.Globalization;
using CorrelationId;
using CorrelationId.DependencyInjection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using World.Api.Endpoints;
using World.Api.Infrastructure;
using World.Api.Services.Country;

const string appName = "Worlds.Api";
const string mainCors = "MainCORS";
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCorrelationId(options =>
    {
        options.UpdateTraceIdentifier = true;
        options.AddToLoggingScope = true;
    })
    .WithTraceIdentifierProvider();

builder.Services
    .AddCors(options =>
        options.AddPolicy(mainCors, policyBuilder => policyBuilder
            .WithOrigins(builder.Configuration.GetRequiredSection("Cors").Get<string[]>()!)
            .WithMethods(HttpMethods.Get)
            .AllowAnyHeader()
        ));

builder.Services
    .AddRequestLocalization(options =>
    {
        var cultures = new List<CultureInfo>
        {
            new("en"),
            new("ru"),
            new("fr"),
            new("de"),
            new("it")
        };

        options.DefaultRequestCulture = new RequestCulture("en");

        options.SupportedCultures = cultures;
        options.SupportedUICultures = cultures;
        options.RequestCultureProviders = new List<IRequestCultureProvider>
        {
            new QueryStringRequestCultureProvider { QueryStringKey = "language", UIQueryStringKey = "language" },
            new AcceptLanguageHeaderRequestCultureProvider()
        };
    });

var postgresql = builder.Configuration["Postgresql:ConnectionString"]!;

builder.Services
    .AddSingleton(_ => new CountryService(postgresql))
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

    await new WorldDbContextSeed(dbContext).SeedAsync();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
        ForwardLimit = 10
    })
    .UseCors(mainCors)
    .UseCorrelationId()
    .UseRequestLocalization();

app.MapCountryEndpoints()
    .MapTimeZoneEndpoints();

app.Run();
