using JToolbox.DataAccess.L2DB.Entities;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JToolbox.DataAccess.L2DB.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        int Count(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions);

        int Create(DataConnection db, TEntity entity);

        int CreateMany(DataConnection db, List<TEntity> entities, BulkCopyOptions bulkCopyOptions = null);

        int CreateManyWithIdentities(DataConnection db, List<TEntity> entities);

        bool Delete(DataConnection db, int id);

        bool Delete(DataConnection db, TEntity entity);

        int DeleteBy(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions);

        int DeleteMany(DataConnection db, List<int> ids);

        int DeleteMany(DataConnection db, List<TEntity> entities);

        bool EntityExists(DataConnection db, TEntity entity, params Expression<Func<TEntity, bool>>[] expressions);

        bool EntityExists(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions);

        List<TEntity> GetAll(DataConnection db);

        int GetAndUpdate(DataConnection db, Expression<Func<TEntity, bool>> expression, Action<TEntity> action);

        List<TEntity> GetBy(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions);

        TEntity GetById(DataConnection db, int id);

        List<TEntity> GetByIds(DataConnection db, List<int> ids);

        TEntity GetFirstOrDefault(DataConnection db);

        TEntity GetFirstOrDefaultBy(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions);

        void Merge(DataConnection db,
            List<TEntity> newList,
            List<TEntity> currentList,
            IEqualityComparer<TEntity> equalityComparer = null);

        void PrepareEntity(TEntity entity);

        List<TEntity> Query(DataConnection db, string query, params object[] args);

        ITable<TEntity> Table(DataConnection db);

        bool Update(DataConnection db, TEntity entity);

        bool Update(DataConnection db, int id, Action<TEntity> action);

        int UpdateMany(DataConnection db, List<TEntity> entities);

        int UpdateMany(DataConnection db, List<int> ids, Action<TEntity> action);
    }
}