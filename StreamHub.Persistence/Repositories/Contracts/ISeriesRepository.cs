using StreamHub.Persistence.Entities;

namespace StreamHub.Persistence.Repositories.Contracts;

/// <summary>
///     Repository interface for series specific operations.
/// </summary>
public interface ISeriesRepository : IMediaRepository<Series>
{
}