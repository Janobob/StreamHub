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
    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity == null)
        {
            return false; // Falls die Entität nicht existiert, brechen wir ab
        }

        _dbSet.Remove(entity);
        return true;
    }

    /// <inheritdoc />
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        return (await _dbSet.AddAsync(entity)).Entity;
    }
}