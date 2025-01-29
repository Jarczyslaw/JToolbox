using Examples.Desktop.Base;
using Examples.Desktop.Caches.Cache;
using JToolbox.Core.TimeProvider;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.Desktop.Caches
{
    internal class CacheExample : IDesktopExample
    {
        public string CustomActionTitle => null;

        public string Title => "Cache test";

        public Task CleanUp() => Task.CompletedTask;

        public void CustomAction()
        {
            throw new NotImplementedException();
        }

        public async Task Run(IOutputInput outputInput)
        {
            ITimeProvider timeProvider = new LocalTimeProvider();
            UsersDataSource dataSource = new UsersDataSource(timeProvider);
            UsersCache cache = new UsersCache(dataSource, timeProvider);

            CreateInfoTask(outputInput, cache);
            CreateValidationTask(outputInput, dataSource, cache);

            CreateAddAndUpdateTask(dataSource, 13);
            CreateAddAndUpdateTask(dataSource, 17);

            await outputInput.Wait();
        }

        private void CreateAddAndUpdateTask(
            UsersDataSource dataSource,
            int delay)
        {
            StartLongRunningTask(() =>
            {
                while (true)
                {
                    Thread.Sleep(delay);
                    dataSource.AddAndUpdate();
                }
            });
        }

        private void CreateInfoTask(IOutputInput outputInput, UsersCache cache)
        {
            StartLongRunningTask(() =>
            {
                while (true)
                {
                    Thread.Sleep(2000);

                    cache.GetRefreshedData(x => outputInput.WriteLine($"Users count: {x.Values.Count}, updated: {x.Values.Count(y => y.IsUpdated)}"));
                }
            });
        }

        private void CreateValidationTask(IOutputInput outputInput, UsersDataSource dataSource, UsersCache cache)
        {
            StartLongRunningTask(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);

                    cache.GetRefreshedData(x =>
                    {
                        if (!dataSource.ValidateUsers(x))
                        {
                            outputInput.WriteLine("Validation error!");
                        }
                    });
                }
            });
        }

        private void StartLongRunningTask(Action action)
            => Task.Factory.StartNew(action, TaskCreationOptions.LongRunning);
    }
}