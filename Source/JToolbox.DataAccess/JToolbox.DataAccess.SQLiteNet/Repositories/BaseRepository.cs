using JToolbox.DataAccess.SQLiteNet.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JToolbox.DataAccess.SQLiteNet.Repositories
{
    public abstract class BaseRepository<TEntity> : CommonRepository, IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public virtual int Count(SQLiteConnection db, Expression<Func<TEntity, bool>> expression)
        {
            return db.Table<TEntity>().Where(expression).Count();
        }

        public virtual int Create(SQLiteConnection db, TEntity entity)
        {
            PrepareEntity(entity);
            db.Insert(entity);
            return entity.Id;
        }

        public virtual int CreateMany(SQLiteConnection db, List<TEntity> entities)
        {
            if (entities?.Count > 0)
            {
                entities.ForEach(e => PrepareEntity(e));
                return db.InsertAll(entities, true);
            }
            return 0;
        }

        public virtual bool Delete(SQLiteConnection db, TEntity entity)
        {
            return Delete(db, entity.Id);
        }

        public virtual bool Delete(SQLiteConnection db, int id)
        {
            return db.Delete<TEntity>(id) > 0;
        }

        public virtual void DeleteMany(SQLiteConnection db, List<TEntity> entities)
        {
            if (entities?.Count > 0)
            {
                var ids = entities.ConvertAll(x => x.Id);
                DeleteMany(db, ids);
            }
        }

        public virtual void DeleteMany(SQLiteConnection db, List<int> ids)
        {
            if (ids?.Count > 0)
            {
                foreach (var id in ids)
                {
                    db.Delete<TEntity>(id);
                }
            }
        }

        public virtual bool EntityExists(SQLiteConnection db, TEntity entity, Expression<Func<TEntity, bool>> expression)
        {
            var expressions = new List<Expression<Func<TEntity, bool>>>
            {
                x => x.Id != entity.Id,
                expression
            };
            return GetBy(db, expressions).Count > 0;
        }

        public virtual List<TEntity> GetAll(SQLiteConnection db)
        {
            return db.Table<TEntity>().ToList();
        }

        public virtual List<TEntity> GetBy(SQLiteConnection db, Expression<Func<TEntity, bool>> expression)
        {
            return GetBy(db, new List<Expression<Func<TEntity, bool>>> { expression });
        }

        public virtual List<TEntity> GetBy(SQLiteConnection db, IEnumerable<Expression<Func<TEntity, bool>>> expressions)
        {
            var entities = db.Table<TEntity>();
            foreach (var expression in expressions)
            {
                entities = entities.Where(expression);
            }
            return entities.ToList();
        }

        public virtual TEntity GetById(SQLiteConnection db, int id)
        {
            return db.Table<TEntity>().FirstOrDefault(e => e.Id == id);
        }

        public virtual List<TEntity> GetByIds(SQLiteConnection db, List<int> ids)
        {
            var lookup = ids.Distinct()
                .ToList();
            return db.Table<TEntity>()
                .Where(x => lookup.Contains(x.Id))
                .ToList();
        }

        public virtual void Merge(SQLiteConnection db, List<TEntity> newList, List<TEntity> currentList, IEqualityComparer<TEntity> equalityComparer)
        {
            var merge = new Merge<TEntity>();
            merge.MergeLists(newList, currentList, equalityComparer);

            foreach (var entity in merge.ToDelete)
            {
                Delete(db, entity);
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

        public virtual bool Update(SQLiteConnection db, TEntity entity)
        {
            PrepareEntity(entity);
            return db.Update(entity) > 0;
        }

        public virtual int UpdateMany(SQLiteConnection db, List<TEntity> entities)
        {
            if (entities?.Count > 0)
            {
                entities.ForEach(e => PrepareEntity(e));
                return db.UpdateAll(entities);
            }
            return 0;
        }

        protected virtual void PrepareEntity(TEntity entity)
        {
        }
    }
}