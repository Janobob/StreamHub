using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="EpisodeEntity" /> entity.
/// </summary>
public class EpisodeConfiguration : IEntityTypeConfiguration<EpisodeEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<EpisodeEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(2000);

        builder.Property(e => e.Duration)
            .IsRequired();

        builder.Property(e => e.EpisodeNumber)
            .IsRequired();

        builder.Property(e => e.ReleaseDate)
            .HasColumnType("date") // Store only the date
            .IsRequired();

        builder.HasOne(e => e.SeasonEntity)
            .WithMany(s => s.Episodes)
            .HasForeignKey(e => e.SeasonId);
    }
}