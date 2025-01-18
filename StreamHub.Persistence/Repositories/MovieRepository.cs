using StreamHub.Persistence.Contexts;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Persistence.Repositories;

/// <summary>
///     Repository implementation for movie specific operations.
/// </summary>
public class MovieRepository(StreamHubDbContext dbContext)
    : GenericRepository<Movie>(dbContext), IMovieRepository
{
}