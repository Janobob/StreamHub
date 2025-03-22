using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Domain.MetaData.Services.Contracts;

namespace StreamHub.Domain.MetaData.Requests;

/// <summary>
///     Request handler for searching media metadata.
/// </summary>
/// <param name="metaDataProviderResolver">
///     The resolver used to search for media metadata.
/// </param>
public class
    SearchMediaMetaDataRequestHandler(IMetaDataProviderResolver metaDataProviderResolver)
    : IRequestHandler<SearchMediaMetaDataRequest,
        Result<IEnumerable<MetaDataSearchResult>>>
{
    public async Task<Result<IEnumerable<MetaDataSearchResult>>> Handle(SearchMediaMetaDataRequest request,
        CancellationToken cancellationToken)
    {
        return await metaDataProviderResolver.SearchMediaAsync(request.Query, request.Name, request.Limit,
            request.MediaType);
    }
}