using Microsoft.EntityFrameworkCore;
using WebShopApp.DAL;
using WebShopApp.Infrastructure.Interface;

namespace WebShopApp.Infrastructure.Repository
{
    public class EFRepository<TContext> : EFReadOnlyRepository<TContext>, IRepository
      where TContext : Context
    {
        public EFRepository(TContext context)
            : base(context)
        {
        }

        public virtual void Create<TEntity>(TEntity entity, string createdBy = null)
           where TEntity : class
        {
            if (context.Entry(entity).State == EntityState.Deleted)
            {
            }
            context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                context.Set<TEntity>().Attach(entity);
            }
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(object id)
            where TEntity : class
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var dbSet = context.Set<TEntity>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void DeleteRange<TEntity>(List<TEntity> lista) where TEntity : class
        {
            var dbSet = context.Set<TEntity>();
            dbSet.RemoveRange(lista);
        }


        public virtual void Save()
        {
            context.SaveChanges();
        }

        public virtual Task SaveAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
