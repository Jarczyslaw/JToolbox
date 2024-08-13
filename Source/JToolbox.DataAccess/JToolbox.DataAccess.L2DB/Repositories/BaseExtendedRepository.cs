using JToolbox.Core.EqualityComparers;
using JToolbox.Core.TimeProvider;
using JToolbox.Core.Utilities.Merge;
using JToolbox.DataAccess.Common;
using JToolbox.DataAccess.L2DB.Abstraction;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Linq;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.DataAccess.L2DB.Repositories
{
    public abstract class BaseExtendedRepository<TEntity> : BaseRepository<TEntity>, IBaseExtendedRepository<TEntity>
        where TEntity : class, IBaseExtendedEntity, new()
    {
        protected readonly ITimeProvider _timeProvider;
        protected readonly IUserIdProvider _userIdProvider;

        protected BaseExtendedRepository(
            ITimeProvider timeProvider,
            IUserIdProvider userIdProvider)
        {
            _timeProvider = timeProvider;
            _userIdProvider = userIdProvider;
        }

        public override void PrepareEntity(TEntity entity)
        {
            entity.UpdateDate = _timeProvider.Now;
            entity.UpdateUser = _userIdProvider.UserId;

            if (entity.CreateDate == default)
            {
                entity.CreateDate = entity.UpdateDate;
                entity.CreateUser = _userIdProvider.UserId;
            }
        }

        public virtual bool SafeDelete(DataConnection db, int id)
        {
            IQueryable<TEntity> queryable = db.GetTable<TEntity>().Where(x => x.Id == id);
            IUpdatable<TEntity> updatable = SetDeletedProperties(queryable);

            return updatable.Update() > 0;
        }

        public virtual bool SafeDelete(DataConnection db, TEntity entity)
        {
            return SafeDelete(db, entity.Id);
        }

        public virtual int SafeDelete(DataConnection db, List<int> ids)
        {
            IEnumerable<int> lookup = ids.Distinct();

            IQueryable<TEntity> queryable = db.GetTable<TEntity>().Where(x => lookup.Contains(x.Id));
            IUpdatable<TEntity> updatable = SetDeletedProperties(queryable);

            return updatable.Update();
        }

        public virtual int SafeDelete(DataConnection db, List<TEntity> entities)
        {
            return SafeDelete(db, entities.ConvertAll(x => x.Id));
        }

        public virtual void SafeMerge(
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
                SafeDelete(db, entity.Id);
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

        private IUpdatable<TEntity> SetDeletedProperties(IQueryable<TEntity> queryable)
        {
            return queryable.Set(x => x.IsDeleted, true)
                .Set(x => x.UpdateDate, _timeProvider.Now)
                .Set(x => x.UpdateUser, _userIdProvider.UserId);
        }
    }
}