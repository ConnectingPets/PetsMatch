namespace Persistence.Repositories
{
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;

    public interface IRepository
    {
        Task SaveChangesAsync();

        IQueryable<T> All<T>() where T : class;

        IQueryable<T> AllReadonly<T>() where T : class;

        IQueryable<T> All<T>(Expression<Func<T, bool>> expression) where T : class;

        IQueryable<T> AllReadonly<T>(Expression<Func<T, bool>> expression) where T : class;

        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class;

        Task<bool> AnyAsync<T>() where T : class;

        Task<T?> GetById<T> (object id) where T : class;

        Task<T?> GetByIds<T>(params object[] ids) where T : class;

        Task AddAsync<T>(T entity) where T : class;

        Task AddRangeAsync<T>(T[] entities) where T : class;

        Task DeleteAsync<T>(object id) where T : class;
         
        void Delete<T>(T entity) where T : class;

        void DeleteRange<T>(T[] entities) where T : class;

        void DeleteRange<T>(Expression<Func<T, bool>> expression) where T : class;

        Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class;

        void Detach<T>(T entity) where T : class;
    }
}