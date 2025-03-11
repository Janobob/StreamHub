using StreamHub.Common.Enums;

namespace StreamHub.Domain.MetaData.Models;

/// <summary>
///     Represents the result of a media metadata search.
/// </summary>
public class MetaDataSearchResult
{
    /// <summary>
    ///     Gets or sets the unique identifiers from different providers.
    ///     <example>
    ///         { "thetvdb": "1234", "themoviedb": "5678" }
    ///     </example>
    /// </summary>
    public Dictionary<string, string> ProviderIds { get; set; } = new();

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