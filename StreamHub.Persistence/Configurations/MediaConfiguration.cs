using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Enums;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="MediaEntity" /> entity.
/// </summary>
public class MediaConfiguration : IEntityTypeConfiguration<MediaEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<MediaEntity> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.TheTvdbId)
            .IsRequired();

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Description)
            .HasMaxLength(2000);

        builder.Property(m => m.Studio)
            .HasMaxLength(200);

        builder.Property(m => m.Path)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(m => m.ReleaseDate)
            .HasColumnType("date") // Date only, no time
            .IsRequired();

        builder.HasOne(m => m.MediaLibraryEntity)
            .WithMany(ml => ml.MediaItems)
            .HasForeignKey(m => m.MediaLibraryId);

        // Discriminator column for TPH (Table-Per-Hierarchy)
        builder.HasDiscriminator<MediaType>("MediaType")
            .HasValue<MovieEntity>(MediaType.Movie)
            .HasValue<SeriesEntity>(MediaType.Series);

        builder.Property(s => s.Status)
            .HasConversion<string>() // Store enum as string
            .IsRequired()
            .HasDefaultValue(MediaStatus.Released);
    }
}