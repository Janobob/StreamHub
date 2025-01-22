using StreamHub.Persistence.Enums;

namespace StreamHub.Domain.MetaData.Models;

/// <summary>
///     Represents the result of a media metadata search.
/// </summary>
public class MediaMetaDataSearchResult
{
    /// <summary>
    ///     Gets or sets the unique identifier of the media metadata search result.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the media metadata search result.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Gets or sets the description of the media metadata search result.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    ///     Gets or sets the type of media associated with the search result.
    /// </summary>
    public required MediaType MediaType { get; set; }
}