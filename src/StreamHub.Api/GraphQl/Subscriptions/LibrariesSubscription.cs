using AutoMapper;
using StreamHub.Api.Models.Library;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Api.GraphQl.Subscriptions;

/// <summary>
///     GraphQL subscription for libraries.
/// </summary>
[ExtendObjectType(typeof(Subscription))]
public class LibrariesSubscription
{
    [Subscribe]
    [Topic("MediaLibrary_Added")]
    public MediaLibraryResponse OnMediaLibraryAdded([EventMessage] MediaLibrary mediaLibrary, [Service] IMapper mapper)
    {
        return mapper.Map<MediaLibraryResponse>(mediaLibrary);
    }

    [Subscribe]
    [Topic("MediaLibrary_Updated")]
    public MediaLibraryResponse OnMediaLibraryUpdated([EventMessage] MediaLibrary mediaLibrary,
        [Service] IMapper mapper)
    {
        return mapper.Map<MediaLibraryResponse>(mediaLibrary);
    }

    [Subscribe]
    [Topic("MediaLibrary_Deleted")]
    public int OnMediaLibraryDeleted([EventMessage] int id)
    {
        return id;
    }
}