namespace StreamHub.Persistence.Entities;

/// <summary>
///     Represents a series entity, inheriting from the base media class.
/// </summary>
public class Series : Movie
{
    /// <summary>
    ///     Gets or sets the list of seasons for the series.
    /// </summary>
    public List<Season> Seasons { get; set; } = [];
}