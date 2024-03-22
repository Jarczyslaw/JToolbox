using JToolbox.DataAccess.SQLiteNet.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JToolbox.DataAccess.SQLiteNet.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        int Count(SQLiteConnection db, params Expression<Func<TEntity, bool>>[] expressions);

        int Create(SQLiteConnection db, TEntity entity);

        int CreateMany(SQLiteConnection db, List<TEntity> entities);

        bool Delete(SQLiteConnection db, int id);

        bool Delete(SQLiteConnection db, TEntity entity);

        void DeleteMany(SQLiteConnection db, List<int> ids);

        void DeleteMany(SQLiteConnection db, List<TEntity> entities);

        bool EntityExists(SQLiteConnection db, TEntity entity, params Expression<Func<TEntity, bool>>[] expressions);

        List<TEntity> GetAll(SQLiteConnection db);

        int GetAndUpdate(SQLiteConnection db, Expression<Func<TEntity, bool>> expression, Action<TEntity> action);

        List<TEntity> GetBy(SQLiteConnection db, params Expression<Func<TEntity, bool>>[] expressions);

        TEntity GetById(SQLiteConnection db, int id);

        List<TEntity> GetByIds(SQLiteConnection db, List<int> ids);

        TEntity GetFirstOrDefault(SQLiteConnection db);

        void Merge(SQLiteConnection db,
            List<TEntity> newList,
            List<TEntity> currentList,
            IEqualityComparer<TEntity> equalityComparer = null);

        List<TEntity> Query(SQLiteConnection db, string query, params object[] args);

        TableQuery<TEntity> Table(SQLiteConnection db);

        bool Update(SQLiteConnection db, TEntity entity);

        bool Update(SQLiteConnection db, int id, Action<TEntity> action);

        int UpdateMany(SQLiteConnection db, List<TEntity> entities);

        int UpdateMany(SQLiteConnection db, List<int> ids, Action<TEntity> action);
    }
}