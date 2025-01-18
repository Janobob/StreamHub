using Microsoft.EntityFrameworkCore;
using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Contexts;

/// <summary>
///     Database context for the StreamHub application.
/// </summary>
/// <param name="options">The options for configuring the DbContext.</param>
public class StreamHubDbContext(DbContextOptions<StreamHubDbContext> options) : DbContext(options)
{
    /// <summary>
    ///     Gets or sets the collection of media libraries in the database.
    /// </summary>
    public DbSet<MediaLibrary> MediaLibraries { get; set; }

    /// <summary>
    ///     Gets or sets the collection of media entities in the database.
    ///     This is the base type for Movies, Series, and Anime.
    /// </summary>
    public DbSet<Media> Media { get; set; }

    /// <summary>
    ///     Gets or sets the collection of movie entities in the database.
    /// </summary>
    public DbSet<Movie> Movies { get; set; }

    /// <summary>
    ///     Gets or sets the collection of series entities in the database.
    /// </summary>
    public DbSet<Series> Series { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StreamHubDbContext).Assembly);
    }
}