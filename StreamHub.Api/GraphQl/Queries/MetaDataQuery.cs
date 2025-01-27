using MediatR;
using StreamHub.Api.Extensions;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Domain.MetaData.Requests;

namespace StreamHub.Api.GraphQl.Queries;

/// <summary>
///     GraphQL query for metadata-related operations.
/// </summary>
[ExtendObjectType(typeof(Query))]
public class MetaDataQuery
{
    /// <summary>
    ///     Retrieves all registered and available metadata providers.
    /// </summary>
    /// <param name="mediator">The mediator instance for handling requests.</param>
    /// <returns>A list of <see cref="MetaDataProvider" /> representing the metadata providers.</returns>
    public async Task<IEnumerable<MetaDataProvider>> GetMetaDataProvidersAsync(IMediator mediator)
    {
        var result = await mediator.Send(new GetMetaDataProvidersRequest());

        return result.ToGraphQlAction();
    }

    /// <summary>
    ///     Retrieves a specific metadata provider by its name.
    /// </summary>
    /// <param name="mediator">The mediator instance used to send the request.</param>
    /// <param name="name">The name of the metadata provider to retrieve.</param>
    /// <returns>
    ///     The <see cref="MetaDataProvider" /> corresponding to the specified name.
    /// </returns>
    public async Task<MetaDataProvider> GetMetaDataProviderAsync(IMediator mediator, string name)
    {
        var result = await mediator.Send(new GetMetaDataProviderRequest(name));

        return result.ToGraphQlAction();
    }
}