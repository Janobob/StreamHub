namespace StreamHub.Persistence.Entities;

/// <summary>
///     Represents a movie entity, inheriting from the base media class.
/// </summary>
public class Movie : Media
{
    /// <summary>
    ///     Gets or sets the duration of the movie in minutes.
    /// </summary>
    public int Duration { get; set; }
}