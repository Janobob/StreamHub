using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Enums;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="Media" /> entity.
/// </summary>
public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Description)
            .HasMaxLength(2000);

        builder.Property(m => m.Path)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(m => m.ReleaseDate)
            .HasColumnType("date") // Date only, no time
            .IsRequired();

        builder.HasOne(m => m.MediaLibrary)
            .WithMany(ml => ml.MediaItems)
            .HasForeignKey(m => m.MediaLibraryId);

        // Discriminator column for TPH (Table-Per-Hierarchy)
        builder.HasDiscriminator<MediaType>("MediaType")
            .HasValue<Movie>(MediaType.Movie)
            .HasValue<Series>(MediaType.Series);
    }
}