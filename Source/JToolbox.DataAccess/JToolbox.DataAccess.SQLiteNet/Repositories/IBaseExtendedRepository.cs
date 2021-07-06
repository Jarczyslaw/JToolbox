using JToolbox.DataAccess.SQLiteNet.Entities;
using SQLite;
using System.Collections.Generic;

namespace JToolbox.DataAccess.SQLiteNet.Repositories
{
    public interface IBaseExtendedRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseExtendedEntity, new()
    {
        bool SafeDelete(SQLiteConnection db, int id);

        void SafeDelete(SQLiteConnection db, List<int> ids);

        void SafeDelete(SQLiteConnection db, List<TEntity> entities);

        bool SafeDelete(SQLiteConnection db, TEntity entity);

        void SafeMerge(SQLiteConnection db, List<TEntity> newList, List<TEntity> currentList, IEqualityComparer<TEntity> equalityComparer);

        bool SetLock(SQLiteConnection db, int id, bool lockState);

        bool SetLock(SQLiteConnection db, TEntity entity, bool lockState);
    }
}