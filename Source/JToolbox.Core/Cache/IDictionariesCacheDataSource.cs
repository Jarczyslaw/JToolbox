using JToolbox.Core.Utilities;
using System;

namespace JToolbox.Core.Cache
{
    public interface IDictionariesCacheDataSource
    {
        DictionariesContainer GetFullData();

        DictionariesContainer GetModifiedData(int? id, DateTime? modificationDate);

        (int? id, DateTime? modificationDate) GetUpdateIndicators();
    }
}