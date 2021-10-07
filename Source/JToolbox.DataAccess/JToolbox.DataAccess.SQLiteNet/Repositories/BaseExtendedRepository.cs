using JToolbox.DataAccess.Common;
using JToolbox.DataAccess.SQLiteNet.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.DataAccess.SQLiteNet.Repositories
{
    public abstract class BaseExtendedRepository<TEntity> : BaseRepository<TEntity>, IBaseExtendedRepository<TEntity> where TEntity : BaseExtendedEntity, new()
    {
        public virtual bool SafeDelete(SQLiteConnection db, int id)
        {
            return InternalGetAndUpdate(db, id, e => e.Deleted = true);
        }

        public virtual bool SafeDelete(SQLiteConnection db, TEntity entity)
        {
            return SafeDelete(db, entity.Id);
        }

        public virtual void SafeDelete(SQLiteConnection db, List<int> ids)
        {
            InternalGetAndUpdate(db, ids, x => x.Deleted = true);
        }

        public virtual void SafeDelete(SQLiteConnection db, List<TEntity> entities)
        {
            var ids = entities.Select(x => x.Id)
                .ToList();
            InternalGetAndUpdate(db, ids, x => x.Deleted = true);
        }

        public virtual void SafeMerge(SQLiteConnection db, List<TEntity> newList, List<TEntity> currentList, IEqualityComparer<TEntity> equalityComparer)
        {
            var merge = new Merge<TEntity>();
            merge.MergeLists(newList, currentList, equalityComparer);

            foreach (var entity in merge.ToDelete)
            {
                SafeDelete(db, entity);
            }

            if (merge.ToUpdate.Count > 0)
            {
                UpdateMany(db, merge.ToUpdate);
            }

            if (merge.ToCreate.Count > 0)
            {
                CreateMany(db, merge.ToCreate);
            }
        }

        protected override void PrepareEntity(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;
            if (entity.CreateDate == default)
            {
                entity.CreateDate = DateTime.Now;
            }
        }
    }
}