using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using MediatR;
using StreamHub.Api.Extensions;
using StreamHub.Api.Models.Library;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.Library.Requests;

namespace StreamHub.Api.GraphQl.Queries;

/// <summary>
///     GraphQL query for libraries.
/// </summary>
[ExtendObjectType(typeof(Query))]
public class LibrariesQuery
{
    /// <summary>
    ///     Retrieves all media libraries asynchronously.
    /// </summary>
    /// <param name="mediator">The mediator service for sending requests.</param>
    /// <param name="mapper">The mapper service for mapping objects.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains an enumerable of
    ///     MediaLibraryResponse.
    /// </returns>
    [SuppressMessage("Minor Code Smell", "S2325:Make methods static",
        Justification = "Required for GraphQL Dependency Injection")]
    public async Task<IEnumerable<MediaLibraryResponse>> GetAllMediaLibrariesAsync([Service] IMediator mediator,
        [Service] IMapper mapper)
    {
        var result = await mediator.Send(new GetAllMediaLibrarysRequest());

        return result.MapList<MediaLibrary, MediaLibraryResponse>(mapper)
            .ToGraphQlAction();
    }

    /// <summary>
    ///     Retrieves a specific media library asynchronously by its ID.
    /// </summary>
    /// <param name="mediator">The mediator service for sending requests.</param>
    /// <param name="mapper">The mapper service for mapping objects.</param>
    /// <param name="id">The ID of the media library to retrieve.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the MediaLibraryResponse.
    /// </returns>
    [SuppressMessage("Minor Code Smell", "S2325:Make methods static",
        Justification = "Required for GraphQL Dependency Injection")]
    public async Task<MediaLibraryResponse> GetMediaLibraryAsync([Service] IMediator mediator, [Service] IMapper mapper,
        int id)
    {
        var result = await mediator.Send(new GetMediaLibraryRequest(id));

        return result.Map<MediaLibrary, MediaLibraryResponse>(mapper)
            .ToGraphQlAction();
    }

    /// <summary>
    ///     Adds a new media library asynchronously.
    /// </summary>
    /// <param name="mediator">The mediator service for sending requests.</param>
    /// <param name="mapper">The mapper service for mapping objects.</param>
    /// <param name="mediaLibraryRequest">The request object containing media library details.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the MediaLibraryResponse.
    /// </returns>
    [SuppressMessage("Minor Code Smell", "S2325:Make methods static",
        Justification = "Required for GraphQL Dependency Injection")]
    public async Task<MediaLibraryResponse> AddMediaLibraryAsync([Service] IMediator mediator, [Service] IMapper mapper,
        MediaLibraryRequest mediaLibraryRequest)
    {
        var mediaLibrary = mapper.Map<MediaLibrary>(mediaLibraryRequest);
        var result = await mediator.Send(new AddMediaLibraryRequest(mediaLibrary));

        return result.Map<MediaLibrary, MediaLibraryResponse>(mapper)
            .ToGraphQlAction();
    }

    /// <summary>
    ///     Updates an existing media library asynchronously.
    /// </summary>
    /// <param name="mediator">The mediator service for sending requests.</param>
    /// <param name="mapper">The mapper service for mapping objects.</param>
    /// <param name="mediaLibraryRequest">The request object containing updated media library details.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the MediaLibraryResponse.
    /// </returns>
    [SuppressMessage("Minor Code Smell", "S2325:Make methods static",
        Justification = "Required for GraphQL Dependency Injection")]
    public async Task<MediaLibraryResponse> UpdateMediaLibraryAsync([Service] IMediator mediator,
        [Service] IMapper mapper,
        MediaLibraryRequest mediaLibraryRequest)
    {
        var mediaLibrary = mapper.Map<MediaLibrary>(mediaLibraryRequest);
        var result = await mediator.Send(new UpdateMediaLibraryRequest(mediaLibrary));

        return result.Map<MediaLibrary, MediaLibraryResponse>(mapper)
            .ToGraphQlAction();
    }

    /// <summary>
    ///     Deletes a media library asynchronously.
    /// </summary>
    /// <param name="mediator">The mediator service for sending requests.</param>
    /// <param name="mapper">The mapper service for mapping objects.</param>
    /// <param name="mediaLibraryRequest">The request object containing media library details.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the MediaLibraryResponse.
    /// </returns>
    [SuppressMessage("Minor Code Smell", "S2325:Make methods static",
        Justification = "Required for GraphQL Dependency Injection")]
    public async Task<MediaLibraryResponse> DeleteMediaLibraryAsync([Service] IMediator mediator,
        [Service] IMapper mapper,
        MediaLibraryRequest mediaLibraryRequest)
    {
        var mediaLibrary = mapper.Map<MediaLibrary>(mediaLibraryRequest);
        var result = await mediator.Send(new DeleteMediaLibraryRequest(mediaLibrary));

        return result.Map<MediaLibrary, MediaLibraryResponse>(mapper)
            .ToGraphQlAction();
    }
}