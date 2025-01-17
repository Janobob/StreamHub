using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="Movie" /> entity.
/// </summary>
public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasBaseType<Media>(); // Inherits from Media
    }
}