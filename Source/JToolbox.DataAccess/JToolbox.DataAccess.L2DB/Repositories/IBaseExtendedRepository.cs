using JToolbox.DataAccess.L2DB.Abstraction;
using LinqToDB.Data;
using System.Collections.Generic;

namespace JToolbox.DataAccess.L2DB.Repositories
{
    public interface IBaseExtendedRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IBaseExtendedEntity, new()
    {
        bool SafeDelete(DataConnection db, int id);

        int SafeDelete(DataConnection db, List<int> ids);

        int SafeDelete(DataConnection db, List<TEntity> entities);

        bool SafeDelete(DataConnection db, TEntity entity);

        void SafeMerge(
            DataConnection db,
            List<TEntity> newList,
            List<TEntity> currentList,
            IEqualityComparer<TEntity> equalityComparer = null);
    }
}