using JToolbox.Core.Abstraction;
using JToolbox.Core.EqualityComparers;
using JToolbox.Core.TimeProvider;
using JToolbox.Core.Utilities.Merge;
using JToolbox.DataAccess.Common;
using JToolbox.DataAccess.EF.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JToolbox.DataAccess.EF.Repositories
{
    public class BaseExtendedRepository<TModel> : BaseRepository<TModel>, IBaseExtendedRepository<TModel>
        where TModel : BaseExtendedModel, IKey
    {
        private readonly ITimeProvider timeProvider;

        public BaseExtendedRepository(ITimeProvider timeProvider)
        {
            this.timeProvider = timeProvider;
        }

        public virtual bool SafeDelete(DbContext db, int id)
        {
            return Update(db, id, e => e.Deleted = true);
        }

        public virtual bool SafeDelete(DbContext db, TModel model)
        {
            return SafeDelete(db, model.Id);
        }

        public virtual void SafeDelete(DbContext db, List<int> ids)
        {
            UpdateMany(db, ids, x => x.Deleted = true);
        }

        public virtual void SafeDelete(DbContext db, List<TModel> models)
        {
            var ids = models.ConvertAll(x => x.Id);
            UpdateMany(db, ids, x => x.Deleted = true);
        }

        public virtual void SafeMerge(DbContext db, List<TModel> newList, List<TModel> currentList, IEqualityComparer<TModel> equalityComparer = null)
        {
            equalityComparer = equalityComparer ?? new KeyComparer();

            var merge = new MergeCollections<TModel>();
            merge.Merge(currentList, newList, equalityComparer);

            foreach (var entity in merge.ToDelete)
            {
                SafeDelete(db, entity);
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

        protected override void PrepareModel(TModel model)
        {
            model.UpdateDate = timeProvider.Now;
            if (model.CreateDate == default)
            {
                model.CreateDate = timeProvider.Now;
            }
        }
    }
}