using StreamHub.Persistence.Contexts;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Persistence.Repositories;

/// <summary>
///     Repository implementation for media library specific operations.
/// </summary>
public class MediaLibraryRepository(StreamHubDbContext dbContext)
    : GenericRepository<MediaLibrary>(dbContext), IMediaLibraryRepository
{
}