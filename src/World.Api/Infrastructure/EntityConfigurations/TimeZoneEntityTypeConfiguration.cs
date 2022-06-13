using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeZone = World.Api.Models.TimeZone;

namespace World.Api.Infrastructure.EntityConfigurations;

internal sealed class TimeZoneEntityTypeConfiguration : IEntityTypeConfiguration<TimeZone>
{
    public void Configure(EntityTypeBuilder<TimeZone> builder)
    {
        builder.ToTable("time_zones");

        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => x.Name);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseSerialColumn();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.UtcOffset)
            .HasColumnName("utc_offset")
            .IsRequired();

        builder.HasMany(x => x.Translations)
            .WithOne()
            .HasForeignKey(x => x.TimeZoneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
