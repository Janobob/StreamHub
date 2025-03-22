using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Domain.MetaData.Services.Contracts;

namespace StreamHub.Domain.MetaData.Requests;

/// <summary>
///     Request handler for getting a meta data provider.
/// </summary>
/// <param name="metaDataProviderResolver">The resolver used to retrieve all available meta data providers.</param>
public class GetMetaDataProviderRequestHandler(IMetaDataProviderResolver metaDataProviderResolver)
    : IRequestHandler<GetMetaDataProviderRequest, Result<MetaDataProvider>>
{
    public Task<Result<MetaDataProvider>> Handle(GetMetaDataProviderRequest request,
        CancellationToken cancellationToken)
    {
        // Return all available meta data providers
        return Task.FromResult(metaDataProviderResolver.GetProviderByName(request.Name));
    }
}