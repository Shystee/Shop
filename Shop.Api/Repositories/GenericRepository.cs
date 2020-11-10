using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.DataAccess;

namespace Shop.Api.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity model);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(object id);

        bool HasChanges();

        void Remove(TEntity model);

        Task<bool> SaveAsync();

        void Update(TEntity model);
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext Context;

        protected GenericRepository(DataContext context)
        {
            Context = context;
        }

        public virtual async Task AddAsync(TEntity model)
        {
            await Context.Set<TEntity>().AddAsync(model).ConfigureAwait(false);
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

        public virtual void Update(TEntity model)
        {
            Context.Set<TEntity>().Update(model);
        }
    }
}