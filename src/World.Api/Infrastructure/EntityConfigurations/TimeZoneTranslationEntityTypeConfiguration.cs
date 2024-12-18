using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using World.Api.Models;

namespace World.Api.Infrastructure.EntityConfigurations;

internal sealed class TimeZoneTranslationEntityTypeConfiguration : IEntityTypeConfiguration<TimeZoneTranslation>
{
    public void Configure(EntityTypeBuilder<TimeZoneTranslation> builder)
    {
        builder.ToTable("time_zone_translations");

        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => new
        {
            x.TimeZoneId,
            x.Language
        });

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseSerialColumn();

        builder.Property(x => x.TimeZoneId)
            .HasColumnName("time_zone_id");

        builder.Property(x => x.Language)
            .HasColumnName("language")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.StandardName)
            .HasColumnName("standard_name")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.DisplayName)
            .HasColumnName("display_name")
            .HasMaxLength(128)
            .IsRequired();
    }
}
