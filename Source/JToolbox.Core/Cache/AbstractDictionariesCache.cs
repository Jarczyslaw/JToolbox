using JToolbox.Core.Extensions;
using JToolbox.Core.TimeProvider;
using JToolbox.Core.Utilities;
using System;
using System.Threading;

namespace JToolbox.Core.Cache
{
    public abstract class AbstractDictionariesCache
    {
        protected readonly DictionariesContainer _container = new DictionariesContainer();
        protected readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        private readonly IDictionariesCacheDataSource _dictionariesCacheDataSource;
        private readonly DictionariesCacheListener _dictionariesCacheListener;
        private readonly ITimeProvider _timeProvider;
        private DateTime? _lastCleanUpDate;

        public AbstractDictionariesCache(
            IDictionariesCacheDataSource dictionariesCacheDataSource,
            ITimeProvider timeProvider,
            DictionariesCacheListener dictionariesCacheListener = null)
        {
            _dictionariesCacheDataSource = dictionariesCacheDataSource;
            _dictionariesCacheListener = dictionariesCacheListener ?? new DictionariesCacheListener();
            _timeProvider = timeProvider;
        }

        public TimeSpan? CleanUpInterval { get; set; }

        public int? LastId { get; private set; }

        public DateTime? LastModificationDate { get; private set; }

        public UpdateIndicators GetRefreshedData(Action<DictionariesContainer> dataAction)
        {
            UpdateIndicators updateIndicators = RefreshIfNeeded();

            _lock.AsReader(() => dataAction(_container));

            return updateIndicators;
        }

        public UpdateIndicators Initialize(InitializationMode initializationMode)
        {
            return _lock.AsWriter(() =>
            {
                _dictionariesCacheListener.InitializationStarted(this, initializationMode);

                DictionariesContainer initialData = _dictionariesCacheDataSource.GetFullData();

                _dictionariesCacheListener.InitializationDataLoaded(this, initialData);

                _container.Clear();
                ApplyDataToCache(initialData);

                UpdateIndicators updateIndicators = new UpdateIndicators
                {
                    UpdateMode = UpdateMode.Full,
                    LastId = LastId,
                    LastModificationDate = LastModificationDate,
                };

                (LastId, LastModificationDate) = GetUpdateIndicatorsFromContainer(initialData);

                updateIndicators.NewLastId = LastId;
                updateIndicators.NewLastModificationDate = LastModificationDate;

                _lastCleanUpDate = _timeProvider.Now;

                _dictionariesCacheListener.InitializationFinished(this, _container);

                return updateIndicators;
            });
        }

        public UpdateIndicators RefreshIfNeeded()
        {
            if (IsCleanUpRequired())
            {
                return Initialize(InitializationMode.Cleanup);
            }

            (int? newLastId, DateTime? newLastModificationDate) = _dictionariesCacheDataSource.GetUpdateIndicators();

            UpdateMode updateMode = GetUpdateModeByIndicators(newLastId, newLastModificationDate);
            if (updateMode == UpdateMode.None)
            {
                return CreateUpdateIndicatorsIfRefreshIsNotNeeded();
            }

            return _lock.AsWriter(() =>
            {
                (newLastId, newLastModificationDate) = _dictionariesCacheDataSource.GetUpdateIndicators();

                updateMode = GetUpdateModeByIndicators(newLastId, newLastModificationDate);
                if (updateMode == UpdateMode.None)
                {
                    return CreateUpdateIndicatorsIfRefreshIsNotNeeded();
                }

                UpdateIndicators updateIndicators = new UpdateIndicators
                {
                    UpdateMode = updateMode,
                    LastId = LastId,
                    LastModificationDate = LastModificationDate,
                    NewLastId = newLastId,
                    NewLastModificationDate = newLastModificationDate
                };

                _dictionariesCacheListener.UpdateStarted(this, updateIndicators);

                DictionariesContainer modifiedData = GetModifiedData(updateMode);

                _dictionariesCacheListener.UpdateDataLoaded(this, modifiedData);

                Apply(updateMode, modifiedData);

                _dictionariesCacheListener.UpdateFinished(this, _container);

                return updateIndicators;
            });
        }

        protected abstract void ApplyDataToCache(DictionariesContainer updatedData);

        protected abstract (int? newLastId, DateTime? newLastModificationDate) GetUpdateIndicatorsFromContainer(DictionariesContainer data);

        private void Apply(UpdateMode updateMode, DictionariesContainer updatedData)
        {
            if (updateMode == UpdateMode.Clear)
            {
                _container.Clear();
                LastId = null;
                LastModificationDate = null;
            }
            else if (updatedData.ContainsAnyData
                && (updateMode == UpdateMode.Incremental || updateMode == UpdateMode.Full))
            {
                if (updateMode == UpdateMode.Full)
                {
                    _container.Clear();
                }

                ApplyDataToCache(updatedData);

                (int? newLastId, DateTime? newLastModificationDate) = GetUpdateIndicatorsFromContainer(updatedData);

                ApplyNewLastId(newLastId);
                ApplyNewLastModificationDate(newLastModificationDate);
            }
        }

        private void ApplyNewLastId(int? newLastId)
        {
            LastId = LastId != null && newLastId != null
                ? Math.Max(LastId.Value, newLastId.Value)
                : newLastId;
        }

        private void ApplyNewLastModificationDate(DateTime? newLastModificationDate)
        {
            LastModificationDate = LastModificationDate != null && newLastModificationDate != null
                ? LastModificationDate > newLastModificationDate
                    ? LastModificationDate
                    : newLastModificationDate
                : newLastModificationDate;
        }

        private UpdateIndicators CreateUpdateIndicatorsIfRefreshIsNotNeeded() => new UpdateIndicators
        {
            UpdateMode = UpdateMode.None,
            LastId = LastId,
            LastModificationDate = LastModificationDate,
        };

        private DictionariesContainer GetModifiedData(UpdateMode updateMode)
        {
            if (updateMode == UpdateMode.Full)
            {
                return _dictionariesCacheDataSource.GetFullData();
            }
            else if (updateMode == UpdateMode.Incremental)
            {
                return _dictionariesCacheDataSource.GetModifiedData(LastId, LastModificationDate);
            }

            return new DictionariesContainer();
        }

        private UpdateMode GetUpdateModeByIndicators(
            int? newLastId,
            DateTime? newLastModificationDate)
        {
            if (LastId == null && newLastId == null)
            {
                return UpdateMode.None;
            }
            else if ((LastId != null && newLastId != null)
                || (newLastModificationDate != null && LastModificationDate != null))
            {
                return newLastModificationDate > LastModificationDate || newLastId > LastId
                    ? UpdateMode.Incremental
                    : UpdateMode.None;
            }
            else if (LastId == null && newLastId != null)
            {
                return UpdateMode.Full;
            }
            else if (LastId != null && newLastId == null)
            {
                return UpdateMode.Clear;
            }

            throw new Exception("Can not determine update mode");
        }

        private bool IsCleanUpRequired()
        {
            if (CleanUpInterval == null || CleanUpInterval == TimeSpan.Zero) { return false; }

            return _timeProvider.Now - _lastCleanUpDate >= CleanUpInterval;
        }
    }
}