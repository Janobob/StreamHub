using StreamHub.Persistence.Contexts;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Persistence.Repositories;

public class SeriesRepository(StreamHubDbContext dbContext)
    : GenericRepository<Series>(dbContext), ISeriesRepository
{
}