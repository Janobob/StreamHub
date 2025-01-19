using StreamHub.Persistence.Contexts;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Persistence.Repositories;

/// <summary>
///     Repository implementation for episode specific operations.
/// </summary>
/// <param name="dbContext">The database context used for accessing the database.</param>
public class EpisodeRepository(StreamHubDbContext dbContext) : GenericRepository<Episode>(dbContext), IEpisodeRepository
{
}