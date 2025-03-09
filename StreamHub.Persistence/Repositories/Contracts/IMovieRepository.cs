using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Repositories.Contracts;

/// <summary>
///     Repository interface for movie specific operations.
/// </summary>
public interface IMovieRepository : IMediaRepository<MovieEntity>
{
}