namespace StreamHub.Persistence.Entities;

/// <summary>
///     Represents an episode of a series.
/// </summary>
public class Episode
{
    /// <summary>
    ///     Gets or sets the unique identifier for the media entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the title of the episode.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    ///     Gets or sets the description of the episode.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Gets or sets the duration of the episode in minutes.
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    ///     Gets or sets the episode number.
    /// </summary>
    public int EpisodeNumber { get; set; }

    /// <summary>
    ///     Gets or sets the release date of the episode.
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    ///     Gets or sets the foreign key for the season.
    /// </summary>
    public int SeasonId { get; set; }

    /// <summary>
    ///     Gets or sets the navigation property to the season that this episode belongs to.
    /// </summary>
    public required Season Season { get; set; }
}