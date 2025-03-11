using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="MediaLibraryEntity" /> entity.
/// </summary>
public class MediaLibraryConfiguration : IEntityTypeConfiguration<MediaLibraryEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<MediaLibraryEntity> builder)
    {
        builder.HasKey(ml => ml.Id);

        builder.Property(ml => ml.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ml => ml.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(ml => ml.Path)
            .IsRequired();

        builder.HasMany(ml => ml.MediaItems)
            .WithOne(m => m.MediaLibraryEntity)
            .HasForeignKey(m => m.MediaLibraryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}