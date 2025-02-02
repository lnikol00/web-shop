﻿namespace WebShopApp.Infrastructure.Interface
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity, string createdBy = null)
        where TEntity : class;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class;

        void Delete<TEntity>(object id)
            where TEntity : class;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class;

        void DeleteRange<TEntity>(List<TEntity> lista) where TEntity : class;

        void Save();

        Task SaveAsync();
    }
}
