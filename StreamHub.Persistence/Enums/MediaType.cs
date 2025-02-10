namespace StreamHub.Persistence.Enums;

// TODO: move to StreamHub.Common.Enums
/// <summary>
///     Represents the type of media entity.
/// </summary>
public enum MediaType
{
    /// <summary>
    ///     Indicates that the media is of any type.
    /// </summary>
    All,

    /// <summary>
    ///     Indicates that the media is a movie.
    /// </summary>
    Movie,

    /// <summary>
    ///     Indicates that the media is a series.
    /// </summary>
    Series,

    /// <summary>
    ///     Indicates that the media is an anime.
    /// </summary>
    Anime
}