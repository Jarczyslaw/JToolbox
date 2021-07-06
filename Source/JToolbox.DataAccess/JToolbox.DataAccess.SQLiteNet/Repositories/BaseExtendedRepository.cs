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
            return InternalGetUpdate(db, id, e => e.Deleted = true);
        }

        public virtual bool SafeDelete(SQLiteConnection db, TEntity entity)
        {
            return SafeDelete(db, entity.Id);
        }

        public virtual void SafeDelete(SQLiteConnection db, List<int> ids)
        {
            InternalGetUpdate(db, ids, x => x.Deleted = true);
        }

        public virtual void SafeDelete(SQLiteConnection db, List<TEntity> entities)
        {
            var ids = entities.Select(x => x.Id)
                .ToList();
            InternalGetUpdate(db, ids, x => x.Deleted = true);
        }

        public virtual bool SetLock(SQLiteConnection db, int id, bool lockState)
        {
            return InternalGetUpdate(db, id, e => e.Locked = lockState);
        }

        public virtual bool SetLock(SQLiteConnection db, TEntity entity, bool lockState)
        {
            return SetLock(db, entity.Id, lockState);
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

        private bool InternalGetUpdate(SQLiteConnection db, int id, Action<TEntity> action)
        {
            var entity = db.Table<TEntity>().Where(t => t.Id == id).FirstOrDefault();
            if (entity != null)
            {
                PrepareEntity(entity);
                action(entity);
                return db.Update(entity) > 0;
            }
            return false;
        }

        private void InternalGetUpdate(SQLiteConnection db, List<int> ids, Action<TEntity> action)
        {
            var entities = db.Table<TEntity>().Where(t => ids.Contains(t.Id))
                .ToList();
            if (entities?.Count > 0)
            {
                foreach (var entity in entities)
                {
                    PrepareEntity(entity);
                    action(entity);
                }

                db.UpdateAll(entities, true);
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