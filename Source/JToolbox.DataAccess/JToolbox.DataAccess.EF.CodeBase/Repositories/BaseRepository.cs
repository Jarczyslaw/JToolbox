using JToolbox.Core.Abstraction;
using JToolbox.DataAccess.Common;
using JToolbox.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace JToolbox.DataAccess.EF.Repositories
{
    public class BaseRepository<TModel> : IBaseRepository<TModel>
        where TModel : BaseModel, IKey
    {
        public virtual int Count(DbContext db, Expression<Func<TModel, bool>> expression)
        {
            return db.Set<TModel>()
                .Where(expression)
                .Count();
        }

        public virtual int Create(DbContext db, TModel model)
        {
            PrepareModel(model);
            db.Set<TModel>().Add(model);
            db.SaveChanges();
            return model.Id;
        }

        public virtual int CreateMany(DbContext db, List<TModel> models)
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

        public virtual bool Delete(DbContext db, TModel model)
        {
            return Delete(db, model.Id);
        }

        public virtual bool Delete(DbContext db, int id)
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

        public virtual void DeleteMany(DbContext db, List<TModel> models)
        {
            if (models?.Count > 0)
            {
                var ids = models.ConvertAll(x => x.Id);
                DeleteMany(db, ids);
            }
        }

        public virtual void DeleteMany(DbContext db, List<int> ids)
        {
            if (ids?.Count > 0)
            {
                var models = GetByIds(db, ids);
                db.Set<TModel>().RemoveRange(models);
                db.SaveChanges();
            }
        }

        public virtual bool EntityExists(DbContext db, TModel model, Expression<Func<TModel, bool>> expression)
        {
            var expressions = new List<Expression<Func<TModel, bool>>>
            {
                x => x.Id != model.Id,
                expression
            };
            return GetBy(db, expressions).Count > 0;
        }

        public virtual List<TModel> GetAll(DbContext db)
        {
            return db.Set<TModel>().ToList();
        }

        public int GetAndUpdate(DbContext db, Expression<Func<TModel, bool>> expression, Action<TModel> action)
        {
            var attachedModels = GetBy(db, expression);
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

        public virtual List<TModel> GetBy(DbContext db, Expression<Func<TModel, bool>> expression)
        {
            return GetBy(db, new List<Expression<Func<TModel, bool>>> { expression });
        }

        public virtual List<TModel> GetBy(DbContext db, IEnumerable<Expression<Func<TModel, bool>>> expressions)
        {
            var entities = db.Set<TModel>().AsQueryable();
            foreach (var expression in expressions)
            {
                entities = entities.Where(expression);
            }
            return entities.ToList();
        }

        public virtual TModel GetById(DbContext db, int id)
        {
            return db.Set<TModel>().FirstOrDefault(e => e.Id == id);
        }

        public virtual List<TModel> GetByIds(DbContext db, List<int> ids)
        {
            var lookup = ids.Distinct()
                .ToList();
            return db.Set<TModel>()
                .Where(x => lookup.Contains(x.Id))
                .ToList();
        }

        public virtual void Merge(DbContext db, List<TModel> newList, List<TModel> currentList, IEqualityComparer<TModel> equalityComparer)
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

        public virtual bool Update(DbContext db, TModel model)
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

        public virtual bool Update(DbContext db, int id, Action<TModel> action)
        {
            return GetAndUpdate(db, x => x.Id == id, action) > 0;
        }

        public virtual int UpdateMany(DbContext db, List<TModel> models)
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

        public virtual int UpdateMany(DbContext db, List<int> ids, Action<TModel> action)
        {
            ids = ids.Distinct()
                .ToList();
            return GetAndUpdate(db, x => ids.Contains(x.Id), action);
        }

        protected virtual void PrepareModel(TModel model)
        {
        }
    }
}