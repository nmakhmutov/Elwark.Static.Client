using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using World.Api.Models;

namespace World.Api.Infrastructure.EntityConfigurations;

internal sealed class CountryTranslationEntityTypeConfiguration : IEntityTypeConfiguration<CountryTranslation>
{
    public void Configure(EntityTypeBuilder<CountryTranslation> builder)
    {
        builder.ToTable("country_translations");

        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => new { x.CountryId, x.Language });

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseSerialColumn();

        builder.Property(x => x.CountryId)
            .HasColumnName("country_id")
            .IsRequired();

        builder.Property(x => x.Language)
            .HasColumnName("language")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.Common)
            .HasColumnName("common")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Official)
            .HasColumnName("official")
            .HasMaxLength(256)
            .IsRequired();

        builder.HasOne<Country>()
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
