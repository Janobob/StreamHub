using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="SeasonEntity" /> entity.
/// </summary>
public class SeasonConfiguration : IEntityTypeConfiguration<SeasonEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SeasonEntity> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SeasonNumber)
            .IsRequired();

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Description)
            .HasMaxLength(2000);

        builder.Property(s => s.ReleaseDate)
            .HasColumnType("date") // Date only, no time
            .IsRequired();

        builder.Property(s => s.EndDate)
            .HasColumnType("date") // Date only, no time
            .IsRequired();

        builder.HasOne(s => s.SeriesEntity)
            .WithMany(s => s.Seasons)
            .HasForeignKey(s => s.SeriesId);

        builder.HasMany(s => s.Episodes)
            .WithOne(s => s.SeasonEntity)
            .HasForeignKey(s => s.SeasonId);
    }
}