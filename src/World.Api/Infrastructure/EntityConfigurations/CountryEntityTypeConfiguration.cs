using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using World.Api.Models;

namespace World.Api.Infrastructure.EntityConfigurations;

internal sealed class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");

        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => x.Alpha2);
        builder.HasAlternateKey(x => x.Alpha3);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Alpha2)
            .HasColumnName("alpha2")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.Alpha3)
            .HasColumnName("alpha3")
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.Flag)
            .HasColumnName("flag")
            .HasMaxLength(1024)
            .IsRequired();

        builder.HasMany(x => x.Translations)
            .WithOne()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
