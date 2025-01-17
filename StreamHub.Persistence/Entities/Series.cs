using StreamHub.Persistence.Enums;

namespace StreamHub.Persistence.Entities;

/// <summary>
///     Represents a series entity, inheriting from the base media class.
/// </summary>
public class Series : Movie
{
    /// <summary>
    ///     Gets or sets the status of the series (e.g., continuing, ended).
    /// </summary>
    public MediaStatus Status { get; set; }
}