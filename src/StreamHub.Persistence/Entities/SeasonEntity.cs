namespace StreamHub.Persistence.Entities;

/// <summary>
///     Represents a season of a series.
/// </summary>
public class SeasonEntity
{
    /// <summary>
    ///     Gets or sets the unique identifier for the media library.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the number of the season.
    ///     0 is used for specials.
    /// </summary>
    public int SeasonNumber { get; set; }

    /// <summary>
    ///     Gets or sets the title of the season.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    ///     Gets or sets the description of the season.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Gets or sets the release date of the season.
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    ///     Gets or sets the end date of the season.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    ///     Get or sets the foreign key for the series.
    /// </summary>
    public int SeriesId { get; set; }

    /// <summary>
    ///     Gets or sets the navigation property to the series that this season belongs to.
    /// </summary>
    public required SeriesEntity SeriesEntity { get; set; }

    /// <summary>
    ///     Gets or sets the list of episodes in the season.
    /// </summary>
    public List<EpisodeEntity> Episodes { get; set; } = [];
}