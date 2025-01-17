using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="MediaLibrary" /> entity.
/// </summary>
public class MediaLibraryConfiguration : IEntityTypeConfiguration<MediaLibrary>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<MediaLibrary> builder)
    {
        builder.HasKey(ml => ml.Id);

        builder.Property(ml => ml.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ml => ml.Description)
            .HasMaxLength(1000);

        builder.Property(ml => ml.Path)
            .IsRequired();

        builder.HasMany(ml => ml.MediaItems)
            .WithOne(m => m.MediaLibrary)
            .HasForeignKey(m => m.MediaLibraryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}