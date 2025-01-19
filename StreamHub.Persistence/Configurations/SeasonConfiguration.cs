using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Configurations;

/// <summary>
///     Configures the entity framework for the <see cref="Season" /> entity.
/// </summary>
public class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Season> builder)
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

        builder.HasOne(s => s.Series)
            .WithMany(s => s.Seasons)
            .HasForeignKey(s => s.SeriesId);
    }
}