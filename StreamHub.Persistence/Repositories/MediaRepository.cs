using StreamHub.Persistence.Contexts;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Persistence.Repositories;

/// <summary>
///     Repository implementation for media specific operations.
/// </summary>
public class MediaRepository(StreamHubDbContext dbContext)
    : GenericRepository<Media>(dbContext), IMediaRepository<Media>
{
}