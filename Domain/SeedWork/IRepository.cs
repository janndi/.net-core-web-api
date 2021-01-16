using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.SeedWork
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void AddRange(TEntity obj);
        void AddRange(IEnumerable<TEntity> obj);

        TEntity GetById(Guid id);

        TEntity GetById(int id);

        TEntity GetById(string id);

        TEntity GetById(long id);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByIdAsync(string id);

        Task<TEntity> GetByIdAsync(long id);

        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(int pageNo);

        IQueryable<TEntity> GetAll(int pageNo, int pageSize);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(int pageNo);

        Task<IEnumerable<TEntity>> GetAllAsync(int pageNo, int pageSize);

        Task<IEnumerable<TEntity>> GetAllAsync(int pageNo, int pageSize, Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> GetAllAsync(int pageNo, int pageSize, Expression<Func<TEntity, bool>> filter, string sort);

        Task<IEnumerable<TEntity>> GetAllAsync(int pageNo, int pageSize, Expression<Func<TEntity, bool>> filter, string sort, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includeProperties);

        long Count();

        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter);

        Task<long> CountAsync();

        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool enableTracking = false,
            params Expression<Func<TEntity, object>>[] includeProperties
            );

        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity,
                bool>> filter = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            bool enableTracking = false,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> GetAllAsync(int pageNo, int pageSize, Expression<Func<TEntity, bool>> filter, string sort, params Expression<Func<TEntity, object>>[] includeProperties);

        void Update(TEntity obj);
        void UpdateRange(IEnumerable<TEntity> obj);
        void UpdateRange(TEntity obj);

        void Remove(Guid id);

        void Remove(int id);

        void Remove(TEntity obj);
        void RemoveRange(IEnumerable<TEntity> obj);
        void RemoveRange(TEntity obj);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
