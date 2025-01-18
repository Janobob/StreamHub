using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Repositories.Contracts;

/// <summary>
///     Repository interface for media specific operations.
/// </summary>
public interface IMediaRepository<TMedia> : IGenericRepository<TMedia> where TMedia : Media
{
}