using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="Anime" /> entity.
/// </summary>
public class AnimeConfiguration : IEntityTypeConfiguration<Anime>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Anime> builder)
    {
        builder.HasBaseType<Series>(); // Inherits from Series
    }
}