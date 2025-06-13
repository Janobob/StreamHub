namespace StreamHub.Persistence.Repositories.Contracts;

/// <summary>
///     Generic repository interface for basic CRUD operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    ///     Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity if found, or <c>default</c> if not.</returns>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    ///     Retrieves all entities of the specified type.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    ///     Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    ///     Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    void Update(TEntity entity);

    /// <summary>
    ///     Deletes an entity from the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    ///     Saves all pending changes to the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task<int> SaveChangesAsync();
}