using JToolbox.DataAccess.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JToolbox.DataAccess.EF.Repositories
{
    public interface IBaseExtendedRepository<TModel> : IBaseRepository<TModel>
        where TModel : BaseExtendedModel
    {
        bool SafeDelete(DbContext db, int id);

        void SafeDelete(DbContext db, List<int> ids);

        void SafeDelete(DbContext db, List<TModel> models);

        bool SafeDelete(DbContext db, TModel model);

        void SafeMerge(
            DbContext db,
            List<TModel> newList,
            List<TModel> currentList,
            IEqualityComparer<TModel> equalityComparer);
    }
}