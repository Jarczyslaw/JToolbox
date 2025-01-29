using JToolbox.Core.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Tests.DictionaryCacheTests
{
    [TestClass]
    public class DictionaryCacheTests
    {
        private readonly UsersCache _cache;
        private readonly UsersDataSource _dataSource;
        private readonly TestTimeProvider _timeProvider;

        public DictionaryCacheTests()
        {
            _timeProvider = new TestTimeProvider();
            _dataSource = new UsersDataSource(_timeProvider);
            _cache = new UsersCache(_dataSource, _timeProvider);
        }

        /// <summary>
        /// Checks if cache can do clean up if it's time to do some cleaning (determined by interval time)
        /// </summary>
        [TestMethod]
        public void CleanUp_ShouldBeCalledInProperMoment()
        {
            _cache.Initialize(InitializationMode.Startup);

            _cache.CleanUpInterval = TimeSpan.MinValue;

            UpdateIndicators indicators = _cache.RefreshIfNeeded();

            AssertValidIndicators(indicators, UpdateMode.Full);
            Assert.AreEqual(indicators.LastId, indicators.NewLastId);
            Assert.AreEqual(indicators.LastModificationDate, indicators.NewLastModificationDate);
        }

        /// <summary>
        /// Checks if cache is fully loaded with all data
        /// </summary>
        [TestMethod]
        public void Initialize_ShouldBeProperlyInitialized()
        {
            UpdateIndicators indicators = _cache.Initialize(InitializationMode.Startup);

            AssertValidIndicators(indicators, UpdateMode.Full);
            AssertValidUsersCount(_cache.GetUsers());
        }

        /// <summary>
        /// Checks if cache not reload because no new data is present in data source
        /// </summary>
        [TestMethod]
        public void RefreshIfNeeded_NoUpdateRequired()
        {
            _cache.Initialize(InitializationMode.Startup);

            UpdateIndicators indicators = _cache.RefreshIfNeeded();

            AssertValidIndicators(indicators, UpdateMode.None);
        }

        /// <summary>
        /// Checks if cache clears all data if no data in data source is present. Checks also if cache loads all data again if any data
        /// appears in data source
        /// </summary>
        [TestMethod]
        public void RefreshIfNeeded_SourceDataClearedAndFilledAgain_CacheShouldBeAlsoClearedAndFullyLoaded()
        {
            _cache.Initialize(InitializationMode.Startup);

            _dataSource.DeleteAll();

            UpdateIndicators indicators = _cache.GetRefreshedData(x => AssertValidUsersCount(x));
            AssertValidIndicators(indicators, UpdateMode.Clear);

            _dataSource.Add(new User { Name = "Ben" });
            _dataSource.Add(new User { Name = "Tom" });

            indicators = _cache.GetRefreshedData(x => AssertValidUsersCount(x));
            AssertValidIndicators(indicators, UpdateMode.Full);
        }

        /// <summary>
        /// Checks if new and updated data is loaded to cache
        /// </summary>
        [TestMethod]
        public void RefreshIfNeeded_TwoNewUsersAddedAndOneUpdated_UpdateModeAndDataShouldBeValid()
        {
            _cache.Initialize(InitializationMode.Startup);

            _dataSource.Add(new User { Name = "Ben" });
            _dataSource.Add(new User { Name = "Tom" });
            _dataSource.Update(new User
            {
                Id = 1,
                Name = "John2"
            });

            UpdateIndicators indicators = _cache.GetRefreshedData(x =>
            {
                AssertValidUsersCount(x);

                Assert.IsTrue(DoesUserExistByName(x, "Ben"));
                Assert.IsTrue(DoesUserExistByName(x, "Tom"));
                Assert.IsTrue(DoesUserExistByName(x, "John2"));

                Assert.IsTrue(_dataSource.AreReferencesDifferent(x));
            });

            AssertValidIndicators(indicators, UpdateMode.Incremental);
        }

        /// <summary>
        /// Checkes if remove condition works when data in data source is marked as deleted
        /// </summary>
        [TestMethod]
        public void RefreshIfNeeded_TwoUsersMarkedAsDeleted_UsersShouldBeDeleted()
        {
            _cache.Initialize(InitializationMode.Startup);

            _dataSource.MarkAsDeleted(1);
            _dataSource.MarkAsDeleted(2);

            UpdateIndicators indicators = _cache.GetRefreshedData(x => AssertValidUsersCount(x));

            AssertValidIndicators(indicators, UpdateMode.Incremental);
        }

        /// <summary>
        /// Checks if data inserted with earlier modification data can be loaded to cache
        /// </summary>
        [TestMethod]
        public void RefreshIfNeeded_UserWithEarlierModificationDateAdded_UpdateModeAndDataShouldBeValid()
        {
            _cache.Initialize(InitializationMode.Startup);

            _timeProvider.CurrentTime = _timeProvider.CurrentTime.AddHours(-1);

            _dataSource.Add(new User { Name = "Ben" });

            UpdateIndicators indicators = _cache.GetRefreshedData(x =>
            {
                AssertValidUsersCount(x);

                Assert.IsTrue(DoesUserExistByName(x, "Ben"));
            });

            AssertValidIndicators(indicators, UpdateMode.Incremental);
        }

        private void AssertValidIndicators(UpdateIndicators indicators, UpdateMode expectedUpdateMode)
        {
            (int? lastId, DateTime? lastModificationDate) = _dataSource.GetUpdateIndicators();

            Assert.AreEqual(indicators.UpdateMode, expectedUpdateMode);
            Assert.AreEqual(_cache.LastId, lastId);
            Assert.AreEqual(_cache.LastModificationDate, lastModificationDate);
        }

        private void AssertValidUsersCount(Dictionary<int, User> users)
        {
            Assert.AreEqual(
                _dataSource.Users.Count(x => !x.IsDeleted),
                users.Values.Count(x => !x.IsDeleted));
        }

        private bool DoesUserExistByName(Dictionary<int, User> users, string name)
            => users.Values.Single(y => y.Name == name) != null;
    }
}