using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using StreamHub.Common.Enums;

namespace StreamHub.Api.Models.MetaData;

/// <summary>
///     Represents the response model for a media metadata search result.
/// </summary>
/// <param name="ProviderIds">
///     A dictionary of unique identifiers from different providers.
///     <example>{ "thetvdb": "1234", "themoviedb": "5678" }</example>
/// </param>
/// <param name="Name">The name of the media metadata search result.</param>
/// <param name="Description">The description of the media metadata search result.</param>
/// <param name="MediaType">The type of media associated with the search result.</param>
public record MetaDataSearchResultResponse(
    [property: Required]
    [property: Description("A dictionary of unique identifiers from different providers.")]
    Dictionary<string, string> ProviderIds,
    [property: Required]
    [property: Description("The name of the media metadata search result.")]
    string Name,
    [property: Required]
    [property: Description("The description of the media metadata search result.")]
    string Description,
    [property: Required]
    [property: Description("The type of media associated with the search result.")]
    MediaType MediaType
);