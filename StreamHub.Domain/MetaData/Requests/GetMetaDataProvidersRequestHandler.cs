using MediatR;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Domain.MetaData.Services.Contracts;

namespace StreamHub.Domain.MetaData.Requests;

/// <summary>
///     Request handler for getting all meta data providers.
/// </summary>
/// <param name="metaDataProviderResolver">
///     The resolver used to retrieve all available meta data providers.
/// </param>
public class GetMetaDataProvidersRequestHandler(IMetaDataProviderResolver metaDataProviderResolver)
    : IRequestHandler<GetMetaDataProvidersRequest, IEnumerable<MetaDataProvider>>
{
    /// <summary>
    ///     Handles the request to get all meta data providers.
    /// </summary>
    /// <param name="request">
    ///     The request object containing the details of the operation.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains
    ///     an enumerable collection of <see cref="MetaDataProvider" />.
    /// </returns>
    public Task<IEnumerable<MetaDataProvider>> Handle(GetMetaDataProvidersRequest request,
        CancellationToken cancellationToken)
    {
        // Return all available meta data providers
        return Task.FromResult(metaDataProviderResolver.GetAllProvider());
    }
}