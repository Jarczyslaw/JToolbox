using JToolbox.Core.EqualityComparers;
using JToolbox.Core.Utilities.Merge;
using JToolbox.DataAccess.L2DB.Entities;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JToolbox.DataAccess.L2DB.Repositories
{
    public abstract class BaseRepository<TEntity> : CommonRepository, IBaseRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        public virtual bool Any(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions)
        {
            return GetQueryableWithExpressions(db, expressions).Any();
        }

        public virtual int Count(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions)
        {
            return GetQueryableWithExpressions(db, expressions).Count();
        }

        public virtual int Create(DataConnection db, TEntity entity)
        {
            PrepareEntity(entity);
            entity.Id = db.InsertWithInt32Identity(entity);
            return entity.Id;
        }

        public virtual int CreateMany(DataConnection db, List<TEntity> entities, BulkCopyOptions bulkCopyOptions = null)
        {
            if (entities?.Count > 0)
            {
                entities.ForEach(e => PrepareEntity(e));

                bulkCopyOptions = bulkCopyOptions ?? new BulkCopyOptions(BulkCopyType: BulkCopyType.MultipleRows);
                return (int)db.BulkCopy(bulkCopyOptions, entities).RowsCopied;
            }
            return 0;
        }

        public virtual bool Delete(DataConnection db, TEntity entity)
        {
            return db.Delete(entity) == 1;
        }

        public virtual bool Delete(DataConnection db, int id)
        {
            int deletedRows = db.GetTable<TEntity>()
                .Delete(x => x.Id == id);
            return deletedRows == 1;
        }

        public virtual int DeleteBy(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions)
        {
            return GetQueryableWithExpressions(db, expressions).Delete();
        }

        public virtual int DeleteMany(DataConnection db, List<TEntity> entities)
        {
            List<int> ids = entities?.ConvertAll(x => x.Id);
            return DeleteMany(db, ids);
        }

        public virtual int DeleteMany(DataConnection db, List<int> ids)
        {
            if (ids == null || ids.Count == 0) { return 0; }

            return db.GetTable<TEntity>()
                .Delete(x => ids.Contains(x.Id));
        }

        public virtual bool EntityExists(DataConnection db, TEntity entity, params Expression<Func<TEntity, bool>>[] expressions)
        {
            var tempExpressions = new List<Expression<Func<TEntity, bool>>>
            {
                x => x.Id != entity.Id,
            };
            tempExpressions.AddRange(expressions);

            return GetBy(db, tempExpressions.ToArray()).Count > 0;
        }

        public virtual bool EntityExists(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions)
        {
            return GetBy(db, expressions).Count > 0;
        }

        public virtual List<TEntity> GetAll(DataConnection db)
        {
            return db.GetTable<TEntity>().ToList();
        }

        public int GetAndUpdate(DataConnection db, Expression<Func<TEntity, bool>> expression, Action<TEntity> action)
        {
            var entities = db.GetTable<TEntity>()
                .Where(expression)
                .ToList();

            int updatedRows = 0;
            if (entities?.Count > 0)
            {
                foreach (var entity in entities)
                {
                    PrepareEntity(entity);
                    action(entity);

                    updatedRows += db.Update(entity);
                }
            }

            return updatedRows;
        }

        public virtual List<TEntity> GetBy(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions)
        {
            return GetQueryableWithExpressions(db, expressions).ToList();
        }

        public virtual TEntity GetById(DataConnection db, int id)
        {
            return db.GetTable<TEntity>()
                .FirstOrDefault(x => x.Id == id);
        }

        public virtual List<TEntity> GetByIds(DataConnection db, List<int> ids)
        {
            IEnumerable<int> lookup = ids.Distinct();

            return db.GetTable<TEntity>()
                .Where(x => lookup.Contains(x.Id))
                .ToList();
        }

        public virtual TEntity GetFirstOrDefault(DataConnection db)
            => GetFirstOrDefaultBy(db);

        public virtual TEntity GetFirstOrDefaultBy(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions)
        {
            IQueryable<TEntity> query = GetQueryableWithExpressions(db, expressions);
            return query.FirstOrDefault();
        }

        public virtual void Merge(
            DataConnection db,
            List<TEntity> newList,
            List<TEntity> currentList,
            IEqualityComparer<TEntity> equalityComparer = null)
        {
            equalityComparer = equalityComparer ?? new KeyComparer();

            var merge = new MergeCollections<TEntity>();
            merge.Merge(currentList, newList, equalityComparer);

            foreach (var entity in merge.ToDelete)
            {
                Delete(db, entity);
            }

            if (merge.ToUpdate.Count > 0)
            {
                UpdateMany(db, merge.ToUpdate.ConvertAll(x => x.NewItem));
            }

            if (merge.ToCreate.Count > 0)
            {
                CreateMany(db, merge.ToCreate);
            }
        }

        public virtual void PrepareEntity(TEntity entity)
        {
        }

        public virtual List<TEntity> Query(DataConnection db, string query, params object[] args)
            => db.Query<TEntity>(query, args).ToList();

        public ITable<TEntity> Table(DataConnection db) => db.GetTable<TEntity>();

        public virtual bool Update(DataConnection db, TEntity entity)
        {
            PrepareEntity(entity);
            return db.Update(entity) > 0;
        }

        public virtual bool Update(DataConnection db, int id, Action<TEntity> action)
        {
            return GetAndUpdate(db, x => x.Id == id, action) > 0;
        }

        public virtual int UpdateMany(DataConnection db, List<TEntity> entities)
        {
            int updatedRows = 0;
            if (entities?.Count > 0)
            {
                entities.ForEach(x =>
                {
                    PrepareEntity(x);
                    updatedRows += db.Update(x);
                });
            }
            return updatedRows;
        }

        public virtual int UpdateMany(DataConnection db, List<int> ids, Action<TEntity> action)
        {
            IEnumerable<int> lookup = ids.Distinct();
            return GetAndUpdate(db, x => lookup.Contains(x.Id), action);
        }

        private IQueryable<TEntity> ApplyExpressions(IQueryable<TEntity> tableQuery, Expression<Func<TEntity, bool>>[] expressions)
        {
            if (expressions?.Length > 0)
            {
                foreach (var expression in expressions)
                {
                    tableQuery = tableQuery.Where(expression);
                }
            }

            return tableQuery;
        }

        private IQueryable<TEntity> GetQueryableWithExpressions(DataConnection db, params Expression<Func<TEntity, bool>>[] expressions)
        {
            IQueryable<TEntity> tableQuery = db.GetTable<TEntity>();
            return ApplyExpressions(tableQuery, expressions);
        }
    }
}