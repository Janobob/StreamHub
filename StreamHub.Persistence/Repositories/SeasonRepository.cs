using StreamHub.Persistence.Contexts;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Persistence.Repositories;

public class SeasonRepository(StreamHubDbContext dbContext) : GenericRepository<Season>(dbContext), ISeasonRepository
{
}