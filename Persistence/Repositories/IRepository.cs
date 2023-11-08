namespace Persistence.Repositories
{
    using System.Linq.Expressions;

    /// <summary>
    /// Interface containing repository access methods
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Saves all made changes in transaction
        /// </summary>
        /// <returns>Count of changes</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<T> All<T>() where T : class;

        /// <summary>
        /// All records in a table which meet a certain condition
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<T> All<T>(Expression<Func<T, bool>> expression) where T : class;
        
        /// <summary>
        /// All records in a table but they won't be tracked by the change tracker
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<T> AllReadonly<T>() where T : class;

        /// <summary>
        /// All records from a table which meet a certain condition but they 
        /// won't be tracked by the change tracker
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<T> AllReadonly<T>(Expression<Func<T, bool>> expression) where T : class;

        /// <summary>
        /// Checks if there are any entities in the table
        /// </summary>
        /// <returns>Boolean</returns>
        Task<bool> AnyAsync<T>() where T : class;

        /// <summary>
        /// Checks if there are any entities which meet a certain condition
        /// </summary>
        /// <returns>Boolean</returns>
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class;

        /// <summary>
        /// Gets entity from a table with certain id 
        /// </summary>
        /// <returns>Entity or null</returns>
        Task<T?> GetById<T> (object id) where T : class;

        /// <summary>
        /// Gets entity from the table with collection of ids
        /// </summary>
        /// <returns>Entity or null</returns>
        Task<T?> GetByIds<T>(params object[] ids) where T : class;

        /// <summary>
        /// Adds an entity to the table
        /// </summary>
        /// <param name="entity">Entity to add</param>
        Task AddAsync<T>(T entity) where T : class;

        /// <summary>
        /// Adds a collection of entities to the table
        /// </summary>
        /// <param name="entities">Entities to add</param>
        Task AddRangeAsync<T>(T[] entities) where T : class;

        /// <summary>
        /// Delete an entity from the table by id
        /// </summary>
        /// <param name="id">Unique identifier of record to be deleted</param>
        Task DeleteAsync<T>(object id) where T : class;
         
        /// <summary>
        /// Delete an entity from the table
        /// </summary>
        /// <param name="entity">Entity representing record to be deleted</param>
        void Delete<T>(T entity) where T : class;

        /// <summary>
        /// Delete entities from the table
        /// </summary>
        /// <param name="entities">Collection of entities to be deleted</param>
        void DeleteRange<T>(T[] entities) where T : class;

        /// <summary>
        /// Delete entities from the table meeting a condition
        /// </summary>
        void DeleteRange<T>(Expression<Func<T, bool>> expression) where T : class;

        /// <summary>
        /// Getting entity meeting a certain condition
        /// </summary>
        /// <returns>Entity or null</returns>
        Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class;

        /// <summary>
        /// Detach en entity from the change tracker
        /// </summary>
        /// <param name="entity">Entity to be detached</param>
        void Detach<T>(T entity) where T : class;
    }
}