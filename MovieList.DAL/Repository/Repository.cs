using MovieList.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using MovieList.Domain.Pagination;
using MovieList.Common.Extensions;
using Microsoft.Xrm.Sdk;

namespace MovieList.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        #region GetById

        public TEntity GetById(int id) => _dbSet.Find(id);

        #endregion

        #region GetAll

        public IList<TEntity> GetAll() => _dbSet.AsNoTracking().ToList();

        public async Task<IList<TEntity>> GetAllAsync() =>
            await _dbSet.AsNoTracking().ToListAsync();

        public async Task<IList<TResult>> GetAllAsync<TResult>(
         Expression<Func<TEntity, TResult>> selector,
         Expression<Func<TEntity, bool>>? predicate = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            return orderBy is not null
                ? await orderBy(query).Select(selector).ToListAsync()
                : await query.Select(selector).ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int take = 0)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();       

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            if(orderBy is not null)
            {
                query = orderBy(query);
            }

            if(take != 0)
            {
                return await query.Take(take).ToListAsync();
            }

            return await query.ToListAsync();
        }

        #endregion

        #region GetFirstOrDefault

        public TEntity? GetFirstOrDefault(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            return orderBy is not null
                ? orderBy(query).FirstOrDefault()
                : query.FirstOrDefault();
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>? predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            return orderBy is not null
                ? await orderBy(query).FirstOrDefaultAsync()
                : await query.FirstOrDefaultAsync();
        }

        public TResult? GetFirstOrDefault<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            return orderBy is not null
                ? orderBy(query).Select(selector).FirstOrDefault()
                : query.Select(selector).FirstOrDefault();
        }

        public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            return orderBy is not null
                ? await orderBy(query).Select(selector).FirstOrDefaultAsync()
                : await query.Select(selector).FirstOrDefaultAsync();
        }

        #endregion

        #region PagedList

        public async Task<IPagedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int pageIndex = 0,
            int pageSize = 20)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            return orderBy is not null
                ? await orderBy(query).ToPagedListAsync(pageIndex, pageSize)
                : await query.ToPagedListAsync(pageIndex, pageSize);
        }

        #endregion

        #region GroupBy

        public IEnumerable<TResult> GetGrouped<TKey, TResult>(
            Expression<Func<TEntity, TKey>> groupingKey,
            Expression<Func<IGrouping<TKey, TEntity>, TResult>> resultSelector,
            Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.GroupBy(groupingKey).Select(resultSelector);
        }

        #endregion

        #region Insert

        public TEntity Insert(TEntity entity) => _dbSet.Add(entity).Entity;

        public void Insert(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

        #endregion

        #region Update

        public void Update(TEntity entity) => _dbSet.Update(entity);

        public void UpdateRange(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);

        #endregion

        #region Delete
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public void DeleteRange(List<TEntity> entities) => _dbSet.RemoveRange(entities);

        #endregion

        #region Aggregations

        public T? Max<T>(
            Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>>? predicate = null) =>
            predicate is null
                ? _dbSet.Max(selector)
                : _dbSet.Where(predicate).Max(selector);

        public Task<T> MaxAsync<T>(
            Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default) =>
            predicate is null
                ? _dbSet.MaxAsync(selector, cancellationToken)
                : _dbSet.Where(predicate).MaxAsync(selector, cancellationToken);

        public T? Min<T>(
            Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>>? predicate = null) =>
            predicate is null
                ? _dbSet.Min(selector)
                : _dbSet.Where(predicate).Min(selector);

        public Task<T> MinAsync<T>(
            Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default) =>
            predicate is null
                ? _dbSet.MinAsync(selector, cancellationToken)
                : _dbSet.Where(predicate).MinAsync(selector, cancellationToken);

        public decimal Average(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>>? predicate = null) =>
            predicate is null
                ? _dbSet.Average(selector)
                : _dbSet.Where(predicate).Average(selector);

        public Task<decimal> AverageAsync(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default) =>
            predicate is null
                ? _dbSet.AverageAsync(selector, cancellationToken)
                : _dbSet.Where(predicate).AverageAsync(selector, cancellationToken);

        public decimal Sum(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>>? predicate = null) =>
            predicate is null
                ? _dbSet.Sum(selector)
                : _dbSet.Where(predicate).Sum(selector);

        public Task<decimal> SumAsync(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default) =>
            predicate is null
                ? _dbSet.SumAsync(selector, cancellationToken)
                : _dbSet.Where(predicate).SumAsync(selector, cancellationToken);

        #endregion
    }
}
