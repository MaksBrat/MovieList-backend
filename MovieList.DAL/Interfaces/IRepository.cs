﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieList.Domain.Pagination;

namespace MovieList.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region GetFirstOrDefault

        TEntity? GetFirstOrDefault(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        Task<TEntity?> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        TResult? GetFirstOrDefault<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        Task<TResult>? GetFirstOrDefaultAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        #endregion

        #region GetAll

        public IList<TEntity> GetAll();

        Task<IList<TEntity>> GetAllAsync();

        Task<IList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int take = 0);

        #endregion

        #region PagedList

        public Task<IPagedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int pageIndex = 0,
            int pageSize = 20);

        #endregion

        #region Get

        public TEntity GetById(int id);

        #endregion

        #region Insert

        TEntity Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        #endregion

        #region Update

        void Update(TEntity entity);

        #endregion

        #region Delete

        void Delete(int id);

        void Delete(TEntity entity);

        void DeleteRange(List<TEntity> entities);

        #endregion

        #region Aggregations

        T? Max<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>>? predicate = null);

        Task<T> MaxAsync<T>(
            Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        T? Min<T>(Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>>? predicate = null);

        Task<T> MinAsync<T>(
            Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        decimal Average(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>>? predicate = null);

        Task<decimal> AverageAsync(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        decimal Sum(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>>? predicate = null);

        Task<decimal> SumAsync(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        #endregion
    }
}
