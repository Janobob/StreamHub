namespace StreamHub.Persistence.Entities;

/// <summary>
///     Represents a series entity, inheriting from the base media class.
/// </summary>
public class SeriesEntity : MovieEntity
{
    /// <summary>
    ///     Gets or sets the list of seasons for the series.
    /// </summary>
    public List<SeasonEntity> Seasons { get; set; } = [];
}