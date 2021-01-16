using Domain.SeedWork;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected TestDbContext TestDbContext;
        protected readonly DbSet<TEntity> DBSet;

        public BaseRepository(TestDbContext testDbContext)
        {
            TestDbContext = testDbContext;
            DBSet = TestDbContext.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DBSet.Add(obj);
        }
        public virtual void AddRange(TEntity obj)
        {
            DBSet.AddRange(obj);
        }
        public virtual void AddRange(IEnumerable<TEntity> obj)
        {
            DBSet.AddRange(obj);
        }

        public virtual TEntity GetById(Guid id)
        {
            return DBSet.Find(id);
        }

        public TEntity GetById(int id)
        {
            return DBSet.Find(id);
        }

        public TEntity GetById(string id)
        {
            return DBSet.Find(id);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DBSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DBSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await DBSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll(int pageNo, int pageSize)
        {
            return DBSet.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool enableTracking = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DBSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);

            return enableTracking ? query.ToList() : query.AsNoTracking().ToList();
        }

        public virtual void Update(TEntity obj)
        {
            DBSet.Update(obj);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> obj)
        {
            DBSet.UpdateRange(obj);
        }
        public void UpdateRange(TEntity obj)
        {
            DBSet.UpdateRange(obj);
        }

        public virtual void Remove(Guid id)
        {
            DBSet.Remove(DBSet.Find(id));
        }

        public void Remove(int id)
        {
            DBSet.Remove(DBSet.Find(id));
        }

        public void Remove(TEntity obj)
        {
            DBSet.Remove(obj);
        }

        public void RemoveRange(IEnumerable<TEntity> obj)
        {
            DBSet.RemoveRange(obj);
        }

        public void RemoveRange(TEntity obj)
        {
            DBSet.RemoveRange(obj);
        }

        public int SaveChanges()
        {
            return TestDbContext.SaveChanges();
        }

        public long Count()
        {
            return DBSet.Count();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int pageNo, int pageSize)
        {
            return await DBSet.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DBSet.Take(20);
        }

        public IQueryable<TEntity> GetAll(int pageNo)
        {
            return DBSet.Skip((pageNo - 1) * 20).Take(20);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DBSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int pageNo)
        {
            return await DBSet.Skip((pageNo - 1) * 20).Take(20).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int pageNo, int pageSize, Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = DBSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = DBSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

        public async Task<long> CountAsync()
        {
            return await DBSet.CountAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int pageNo, int pageSize, Expression<Func<TEntity, bool>> filter, string sort)
        {
            IQueryable<TEntity> query = DBSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                query = query.OrderBy(x => sort);
            }

            return await query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await TestDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int pageNo, int pageSize, Expression<Func<TEntity, bool>> filter, string sort, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DBSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                query = query.OrderBy(x => sort);
            }

            return await query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int pageNo, int pageSize, Expression<Func<TEntity, bool>> filter, string sort, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DBSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
                query = orderBy(query);

            if (!string.IsNullOrEmpty(sort))
            {
                query = query.OrderBy(x => sort);
            }

            return await query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DBSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DBSet;
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool enableTracking = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DBSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);

            return enableTracking ? await query.ToListAsync() : await query.AsNoTracking().ToListAsync();
        }

        public TEntity GetById(long id)
        {
            return DBSet.Find(id);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (TestDbContext != null)
                {
                    TestDbContext.Dispose();
                    TestDbContext = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await DBSet.FindAsync(id);
        }
    }
}
