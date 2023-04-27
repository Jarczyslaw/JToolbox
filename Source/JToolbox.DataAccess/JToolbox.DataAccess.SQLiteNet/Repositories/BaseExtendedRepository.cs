using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.Common;
using JToolbox.DataAccess.SQLiteNet.Entities;
using SQLite;
using System.Collections.Generic;

namespace JToolbox.DataAccess.SQLiteNet.Repositories
{
    public abstract class BaseExtendedRepository<TEntity> : BaseRepository<TEntity>, IBaseExtendedRepository<TEntity> where TEntity : BaseExtendedEntity, new()
    {
        private readonly ITimeProvider timeProvider;

        protected BaseExtendedRepository(ITimeProvider timeProvider)
        {
            this.timeProvider = timeProvider;
        }

        public virtual bool SafeDelete(SQLiteConnection db, int id)
        {
            var where = $"{SqliteExtensions.GetColumnName<BaseExtendedEntity>(nameof(BaseExtendedEntity.Id))} = {id}";
            var query = CreateSafeDeleteQuery(where);

            return db.Execute(query) > 0;
        }

        public virtual bool SafeDelete(SQLiteConnection db, TEntity entity)
        {
            return SafeDelete(db, entity.Id);
        }

        public virtual void SafeDelete(SQLiteConnection db, List<int> ids)
        {
            var whereIds = string.Join(", ", ids);
            var where = $"{SqliteExtensions.GetColumnName<BaseExtendedEntity>(nameof(BaseExtendedEntity.Id))} IN ({whereIds})";
            var query = CreateSafeDeleteQuery(where);

            db.Execute(query);
        }

        public virtual void SafeDelete(SQLiteConnection db, List<TEntity> entities)
        {
            var ids = entities.ConvertAll(x => x.Id);
            SafeDelete(db, ids);
        }

        public virtual void SafeMerge(SQLiteConnection db, List<TEntity> newList, List<TEntity> currentList, IEqualityComparer<TEntity> equalityComparer)
        {
            var merge = new Merge<TEntity>();
            merge.MergeLists(newList, currentList, equalityComparer);

            foreach (var entity in merge.ToDelete)
            {
                SafeDelete(db, entity.Id);
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
            entity.UpdateDate = timeProvider.Now;
            if (entity.CreateDate == default)
            {
                entity.CreateDate = timeProvider.Now;
            }
        }

        private string CreateSafeDeleteQuery(string whereCondition)
        {
            var query = $@"
UPDATE {SqliteExtensions.GetTableName<TEntity>()}
SET {SqliteExtensions.GetColumnName<BaseExtendedEntity>(nameof(BaseExtendedEntity.Deleted))} = 1
WHERE {whereCondition}";

            return query;
        }
    }
}