using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using World.Api.Infrastructure.EntityConfigurations;
using World.Api.Models;

namespace World.Api.Infrastructure;

public sealed class WorldDbContext : DbContext
{
    public WorldDbContext(DbContextOptions<WorldDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; } = default!;

    public DbSet<CountryTranslation> CountryTranslations { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CountryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CountryTranslationEntityTypeConfiguration());
    }
}

public sealed class WorldDbContextFactory : IDesignTimeDbContextFactory<WorldDbContext>
{
    public WorldDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WorldDbContext>()
            .UseNpgsql("Host=_;Database=_;Username=_;Password=_");

        return new WorldDbContext(optionsBuilder.Options);
    }
}
