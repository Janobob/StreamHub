using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="MovieEntity" /> entity.
/// </summary>
public class MovieConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.HasBaseType<MediaEntity>(); // Inherits from Media

        builder.Property(m => m.Duration)
            .IsRequired();
    }
}