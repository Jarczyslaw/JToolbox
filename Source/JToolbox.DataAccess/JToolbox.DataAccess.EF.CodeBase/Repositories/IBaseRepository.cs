using JToolbox.Core.Abstraction;
using JToolbox.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace JToolbox.DataAccess.EF.Repositories
{
    public interface IBaseRepository<TModel>
        where TModel : BaseModel, IKey
    {
        int Count(DbContext db, params Expression<Func<TModel, bool>>[] expressions);

        int Create(DbContext db, TModel model);

        int CreateMany(DbContext db, List<TModel> models);

        bool Delete(DbContext db, int id);

        bool Delete(DbContext db, TModel model);

        void DeleteMany(DbContext db, List<int> ids);

        void DeleteMany(DbContext db, List<TModel> models);

        bool EntityExists(DbContext db, TModel model, params Expression<Func<TModel, bool>>[] expressions);

        List<TModel> GetAll(DbContext db, bool noTracking = false);

        int GetAndUpdate(DbContext db, Expression<Func<TModel, bool>> expression, Action<TModel> action);

        List<TModel> GetBy(DbContext db, bool noTracking = false, IEnumerable<Expression<Func<TModel, object>>> includes = null, params Expression<Func<TModel, bool>>[] expressions);

        TModel GetById(DbContext db, int id, bool noTracking = false, IEnumerable<Expression<Func<TModel, object>>> includes = null);

        List<TModel> GetByIds(DbContext db, List<int> ids, bool noTracking = false, IEnumerable<Expression<Func<TModel, object>>> includes = null);

        void Merge(
            DbContext db,
            List<TModel> newList,
            List<TModel> currentList,
            IEqualityComparer<TModel> equalityComparer = null);

        bool Update(DbContext db, int id, Action<TModel> action);

        bool Update(DbContext db, TModel model);

        int UpdateMany(DbContext db, List<int> ids, Action<TModel> action);

        int UpdateMany(DbContext db, List<TModel> models);
    }
}