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
        public virtual int Count(DbContext db, params Expression<Func<TModel, bool>>[] expressions)
        {
            var set = db.Set<TModel>()
                .AsQueryable();
            set = ApplyExpressions(set, expressions);

            return set.Count();
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

        public virtual bool EntityExists(DbContext db, TModel model, params Expression<Func<TModel, bool>>[] expressions)
        {
            var tempExpressions = new List<Expression<Func<TModel, bool>>>
            {
                x => x.Id != model.Id,
            };
            tempExpressions.AddRange(expressions);

            return GetBy(db, true, null, expressions).Count > 0;
        }

        public virtual List<TModel> GetAll(DbContext db, bool noTracking = false)
        {
            var query = db.Set<TModel>()
                .AsQueryable();
            query = ApplyNoTracking(query, noTracking);

            return query.ToList();
        }

        public int GetAndUpdate(DbContext db, Expression<Func<TModel, bool>> expression, Action<TModel> action)
        {
            var attachedModels = GetBy(db, false, null, expression);
            if (attachedModels?.Count > 0)
            {
                foreach (var attachedModel in attachedModels)
                {
                    action(attachedModel);
                    PrepareModel(attachedModel);
                }
                db.SaveChanges();
                return attachedModels.Count;
            }

            return 0;
        }

        public virtual List<TModel> GetBy(DbContext db,
            bool noTracking = false,
            IEnumerable<Expression<Func<TModel, object>>> includes = null,
            params Expression<Func<TModel, bool>>[] expressions)
        {
            var query = db.Set<TModel>()
                .AsQueryable();

            query = ApplyNoTracking(query, noTracking);
            query = ApplyExpressions(query, expressions);
            query = ApplyIncludes(query, includes);

            return query.ToList();
        }

        public virtual TModel GetById(DbContext db,
            int id,
            bool noTracking = false,
            IEnumerable<Expression<Func<TModel, object>>> includes = null)
        {
            var query = db.Set<TModel>()
                .AsQueryable();

            query = ApplyNoTracking(query, noTracking);
            query = ApplyIncludes(query, includes);

            return query.FirstOrDefault(e => e.Id == id);
        }

        public virtual List<TModel> GetByIds(DbContext db,
            List<int> ids,
            bool noTracking = false,
            IEnumerable<Expression<Func<TModel, object>>> includes = null)
        {
            var query = db.Set<TModel>()
                .AsQueryable();

            query = ApplyNoTracking(query, noTracking);
            query = ApplyIncludes(query, includes);

            var lookup = ids.Distinct()
                .ToList();

            return query.Where(x => lookup.Contains(x.Id))
                .ToList();
        }

        public virtual void Merge(DbContext db,
            List<TModel> newList,
            List<TModel> currentList,
            IEqualityComparer<TModel> equalityComparer)
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

        private IQueryable<TModel> ApplyExpressions(IQueryable<TModel> query, Expression<Func<TModel, bool>>[] expressions)
        {
            if (expressions?.Length > 0)
            {
                foreach (var expression in expressions)
                {
                    query = query.Where(expression);
                }
            }
            return query;
        }

        private IQueryable<TModel> ApplyIncludes(IQueryable<TModel> query, IEnumerable<Expression<Func<TModel, object>>> includes)
        {
            if (includes?.Count() > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        private IQueryable<TModel> ApplyNoTracking(IQueryable<TModel> query, bool noTracking)
        {
            if (noTracking)
            {
                query.AsNoTracking();
            }

            return query;
        }
    }
}