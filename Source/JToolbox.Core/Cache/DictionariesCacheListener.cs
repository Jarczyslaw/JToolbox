using JToolbox.Core.Utilities;

namespace JToolbox.Core.Cache
{
    public class DictionariesCacheListener
    {
        public virtual void InitializationDataLoaded(AbstractDictionariesCache cache, DictionariesContainer initialData)
        {
        }

        public virtual void InitializationFinished(AbstractDictionariesCache cache, DictionariesContainer cachedData)
        {
        }

        public virtual void InitializationStarted(AbstractDictionariesCache cache, InitializationMode initializationMode)
        {
        }

        public virtual void UpdateDataLoaded(AbstractDictionariesCache cache, DictionariesContainer modifiedData)
        {
        }

        public virtual void UpdateFinished(AbstractDictionariesCache cache, DictionariesContainer cachedData)
        {
        }

        public virtual void UpdateStarted(AbstractDictionariesCache cache, UpdateIndicators updateIndicators)
        {
        }
    }
}