using Microsoft.EntityFrameworkCore;
using StreamHub.Persistence.Contexts;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Persistence.Repositories;

/// <summary>
///     Generic repository implementation for basic CRUD operations.
/// </summary>
/// <param name="dbContext">The database context used for accessing the database.</param>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public abstract class GenericRepository<TEntity>(StreamHubDbContext dbContext) : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity ?? default;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    /// <inheritdoc />
    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    /// <inheritdoc />
    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    /// <inheritdoc />
    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}