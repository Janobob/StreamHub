using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using MediatR;
using StreamHub.Api.Extensions;
using StreamHub.Api.Models.MetaData;
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
    /// <param name="mapper">The mapper instance for mapping models.</param>
    /// <returns>A list of <see cref="MetaDataProvider" /> representing the metadata providers.</returns>
    [SuppressMessage("Minor Code Smell", "S2325:Make methods static",
        Justification = "Required for GraphQL Dependency Injection")]
    public async Task<IEnumerable<MetaDataProviderResponse>> GetMetaDataProvidersAsync([Service] IMediator mediator,
        [Service] IMapper mapper)
    {
        var result = await mediator.Send(new GetAllMetaDataProvidersRequest());

        return result.MapList<MetaDataProvider, MetaDataProviderResponse>(mapper).ToGraphQlAction();
    }

    /// <summary>
    ///     Retrieves a specific metadata provider by its name.
    /// </summary>
    /// <param name="mediator">The mediator instance used to send the request.</param>
    /// <param name="mapper">The mapper instance for mapping models.</param>
    /// <param name="name">The name of the metadata provider to retrieve.</param>
    /// <returns>
    ///     The <see cref="MetaDataProvider" /> corresponding to the specified name.
    /// </returns>
    [SuppressMessage("Minor Code Smell", "S2325:Make methods static",
        Justification = "Required for GraphQL Dependency Injection")]
    public async Task<MetaDataProviderResponse> GetMetaDataProviderAsync([Service] IMediator mediator,
        [Service] IMapper mapper,
        string name)
    {
        var result = await mediator.Send(new GetMetaDataProviderRequest(name));

        return result.Map<MetaDataProvider, MetaDataProviderResponse>(mapper).ToGraphQlAction();
    }
}