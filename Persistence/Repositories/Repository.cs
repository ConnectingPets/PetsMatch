namespace Persistence.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using static Common.ExceptionMessages.Repository;

    /// <summary>
    /// Implementation of the IRepository interface
    /// </summary>
    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Representation of table in the database
        /// </summary>
        protected DbSet<T> Set<T>() where T : class
            => this.context.Set<T>();

        public async Task AddAsync<T>(T entity) where T : class
            => await this.Set<T>()
                         .AddAsync(entity);

        public async Task AddRangeAsync<T>(T[] entities) where T : class
            => await this.Set<T>()
                         .AddRangeAsync(entities);

        public IQueryable<T> All<T>() where T : class
            => this.Set<T>()
                   .AsQueryable();

        public IQueryable<T> All<T>(Expression<Func<T, bool>> expression) where T : class
            => this.Set<T>()
                   .Where(expression);

        public IQueryable<T> AllReadonly<T>() where T : class
            => this.All<T>()
                   .AsNoTracking();

        public IQueryable<T> AllReadonly<T>(Expression<Func<T, bool>> expression) where T : class
            => this.All(expression)
                   .AsNoTracking();

        public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class
            => await this.Set<T>()
                         .AnyAsync(expression);

        public async Task<bool> AnyAsync<T>() where T : class
            => await this.Set<T>()
                         .AnyAsync();

        public async Task DeleteAsync<T>(object id) where T : class
        {
            T? entity = await this.GetById<T>(id);

            if (entity == null)
            {
                throw new InvalidOperationException(EntityNotFound);
            }

            this.Delete(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            EntityEntry entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        public void DeleteRange<T>(T[] entities) where T : class
            => this.Set<T>()
                   .RemoveRange(entities);

        public void DeleteRange<T>(Expression<Func<T, bool>> expression) where T : class
        {
            T[] entities =  this.All<T>().ToArray();

            this.DeleteRange(entities);
        }

        public void Detach<T>(T entity) where T : class
        {
            EntityEntry entry = this.context.Entry(entity);

            entry.State = EntityState.Detached;
        }

        public async Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class
            => await this.Set<T>()
                         .FirstOrDefaultAsync(expression);

        public async Task<T?> GetById<T>(object id) where T : class
            => await this.Set<T>()
                         .FindAsync(id);

        public async Task<T?> GetByIds<T>(object[] id) where T : class
            => await this.Set<T>()
                         .FindAsync(id);

        public async Task<int> SaveChangesAsync()
            => await this.context.SaveChangesAsync();
    }
}