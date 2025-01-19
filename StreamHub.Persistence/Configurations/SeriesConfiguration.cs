using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Enums;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="Series" /> entity.
/// </summary>
public class SeriesConfiguration : IEntityTypeConfiguration<Series>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Series> builder)
    {
        builder.HasBaseType<Media>(); // Inherits from Media

        builder.Property(s => s.Status)
            .HasConversion<string>() // Store enum as string
            .IsRequired()
            .HasDefaultValue(MediaStatus.Continuing);

        builder.HasMany(s => s.Seasons)
            .WithOne(s => s.Series)
            .HasForeignKey(s => s.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}