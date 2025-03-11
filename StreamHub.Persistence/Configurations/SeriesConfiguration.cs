using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Common.Enums;
using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="SeriesEntity" /> entity.
/// </summary>
public class SeriesConfiguration : IEntityTypeConfiguration<SeriesEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SeriesEntity> builder)
    {
        builder.HasBaseType<MediaEntity>(); // Inherits from Media

        builder.Property(s => s.Status)
            .HasConversion<string>() // Store enum as string
            .IsRequired()
            .HasDefaultValue(MediaStatus.Continuing);

        builder.HasMany(s => s.Seasons)
            .WithOne(s => s.SeriesEntity)
            .HasForeignKey(s => s.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}