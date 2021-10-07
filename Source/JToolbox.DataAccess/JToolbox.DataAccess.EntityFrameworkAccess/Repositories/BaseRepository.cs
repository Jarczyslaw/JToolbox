using JToolbox.DataAccess.Common;
using JToolbox.DataAccess.EntityFrameworkAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JToolbox.DataAccess.EntityFrameworkAccess.Repositories
{
    public class BaseRepository<TModel>
        where TModel : BaseModel
    {
        public virtual int Count(BaseContext db, Expression<Func<TModel, bool>> expression)
        {
            return db.Set<TModel>()
                .Where(expression)
                .Count();
        }

        public virtual int Create(BaseContext db, TModel model)
        {
            PrepareModel(model);
            db.Set<TModel>().Add(model);
            db.SaveChanges();
            return model.Id;
        }

        public virtual int CreateMany(BaseContext db, List<TModel> models)
        {
            if (models?.Count > 0)
            {
                models.ForEach(e => PrepareModel(e));
                db.Set<TModel>().AddRange(models);
                db.SaveChanges();
                return models.Count;
            }
            return 0;
        }

        public virtual bool Delete(BaseContext db, TModel model)
        {
            return Delete(db, model.Id);
        }

        public virtual bool Delete(BaseContext db, int id)
        {
            var model = GetById(db, id);
            if (model != null)
            {
                db.Set<TModel>().Remove(model);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public virtual void DeleteMany(BaseContext db, List<TModel> models)
        {
            if (models?.Count > 0)
            {
                var ids = models.ConvertAll(x => x.Id);
                DeleteMany(db, ids);
            }
        }

        public virtual void DeleteMany(BaseContext db, List<int> ids)
        {
            if (ids?.Count > 0)
            {
                var models = GetByIds(db, ids);
                db.Set<TModel>().RemoveRange(models);
                db.SaveChanges();
            }
        }

        public virtual bool EntityExists(BaseContext db, TModel model, Expression<Func<TModel, bool>> expression)
        {
            var expressions = new List<Expression<Func<TModel, bool>>>
            {
                x => x.Id != model.Id,
                expression
            };
            return GetBy(db, expressions).Count > 0;
        }

        public virtual List<TModel> GetAll(BaseContext db)
        {
            return db.Set<TModel>().ToList();
        }

        public virtual List<TModel> GetBy(BaseContext db, Expression<Func<TModel, bool>> expression)
        {
            return GetBy(db, new List<Expression<Func<TModel, bool>>> { expression });
        }

        public virtual List<TModel> GetBy(BaseContext db, IEnumerable<Expression<Func<TModel, bool>>> expressions)
        {
            var entities = db.Set<TModel>().AsQueryable();
            foreach (var expression in expressions)
            {
                entities = entities.Where(expression);
            }
            return entities.ToList();
        }

        public virtual TModel GetById(BaseContext db, int id)
        {
            return db.Set<TModel>().FirstOrDefault(e => e.Id == id);
        }

        public virtual List<TModel> GetByIds(BaseContext db, List<int> ids)
        {
            var lookup = ids.Distinct()
                .ToList();
            return db.Set<TModel>()
                .Where(x => lookup.Contains(x.Id))
                .ToList();
        }

        public virtual void Merge(BaseContext db, List<TModel> newList, List<TModel> currentList, IEqualityComparer<TModel> equalityComparer)
        {
            var merge = new Merge<TModel>();
            merge.MergeLists(newList, currentList, equalityComparer);

            if (merge.ToDelete.Count > 0)
            {
                DeleteMany(db, merge.ToDelete);
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

        public virtual bool Update(BaseContext db, TModel model)
        {
            PrepareModel(model);
            var attachedModel = GetById(db, model.Id);
            if (attachedModel != null)
            {
                db.Entry(attachedModel).CurrentValues.SetValues(model);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public virtual bool Update(BaseContext db, int id, Action<TModel> action)
        {
            return InternalGetAndUpdate(db, id, action);
        }

        public virtual int UpdateMany(BaseContext db, List<TModel> models)
        {
            if (models?.Count > 0)
            {
                models.ForEach(e => PrepareModel(e));
                var lookup = models.ToDictionary(x => x.Id);
                var attachedModels = GetByIds(db, models.ConvertAll(x => x.Id));
                foreach (var attachedModel in attachedModels)
                {
                    db.Entry(attachedModel).CurrentValues.SetValues(lookup[attachedModel.Id]);
                }
                db.SaveChanges();
                return attachedModels.Count;
            }
            return 0;
        }

        public virtual int UpdateMany(BaseContext db, List<int> ids, Action<TModel> action)
        {
            return InternalGetAndUpdate(db, ids, action);
        }

        protected bool InternalGetAndUpdate(BaseContext db, int id, Action<TModel> action)
        {
            var attachedModel = GetById(db, id);
            if (attachedModel != null)
            {
                PrepareModel(attachedModel);
                action(attachedModel);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        protected int InternalGetAndUpdate(BaseContext db, List<int> ids, Action<TModel> action)
        {
            var attachedModels = GetByIds(db, ids);
            if (attachedModels?.Count > 0)
            {
                foreach (var attachedModel in attachedModels)
                {
                    PrepareModel(attachedModel);
                    action(attachedModel);
                }
                db.SaveChanges();
                return attachedModels.Count;
            }

            return 0;
        }

        protected virtual void PrepareModel(TModel model)
        {
        }
    }
}