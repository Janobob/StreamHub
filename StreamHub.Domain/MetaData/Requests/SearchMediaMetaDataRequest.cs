using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Persistence.Enums;

namespace StreamHub.Domain.MetaData.Requests;

/// <summary>
///     Request to search for media metadata items from metadata providers based on the provided query.
/// </summary>
/// <param name="Query">The search query string.</param>
/// <param name="Name">The name of the metadata provider to search within (optional).</param>
/// <param name="MediaType">The type of media to search for (e.g., Movie, Series).</param>
/// <param name="Limit">The maximum number of search results to return. Default is 10.</param>
public record SearchMediaMetaDataRequest(string Query, string? Name, MediaType MediaType, int Limit)
    : IRequest<Result<IEnumerable<MetaDataSearchResult>>>
{
}