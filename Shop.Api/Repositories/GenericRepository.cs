using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.DataAccess;

namespace Shop.Api.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity model);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(object id);

        bool HasChanges();

        void Remove(TEntity model);

        Task<bool> SaveAsync();
    }

    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
            where TEntity : class where TContext : DataContext
    {
        protected readonly TContext Context;

        protected GenericRepository(TContext context)
        {
            Context = context;
        }

        public void Add(TEntity model)
        {
            Context.Set<TEntity>().Add(model);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await Context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
        }

        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }

        public void Remove(TEntity model)
        {
            Context.Set<TEntity>().Remove(model);
        }

        public virtual async Task<bool> SaveAsync()
        {
            var changedRows = await Context.SaveChangesAsync().ConfigureAwait(false);

            return changedRows > 0;
        }
    }
}