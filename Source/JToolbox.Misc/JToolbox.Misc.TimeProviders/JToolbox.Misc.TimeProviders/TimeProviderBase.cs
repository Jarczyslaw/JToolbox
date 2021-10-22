using System;
using System.Diagnostics;

namespace JToolbox.Misc.TimeProviders
{
    public abstract class TimeProviderBase : ITimeProvider
    {
        protected DateTime? synchronizationDate;
        private static readonly object sync = new object();
        private readonly Stopwatch stopwatch = new Stopwatch();
        public TimeSpan MaxOffset { get; set; } = TimeSpan.FromMinutes(30);
        public TimeSpan SynchronizationInterval { get; set; } = TimeSpan.FromHours(12);

        public DateTime Now()
        {
            lock (sync)
            {
                if (!synchronizationDate.HasValue || stopwatch.Elapsed > SynchronizationInterval)
                {
                    synchronizationDate = Synchronize();
                    stopwatch.Restart();
                }

                DateTime now = synchronizationDate.Value + stopwatch.Elapsed;
                if ((DateTime.Now - now).Ticks > MaxOffset.Ticks)
                {
                    synchronizationDate = Synchronize();
                    stopwatch.Restart();
                }

                return synchronizationDate.Value + stopwatch.Elapsed;
            }
        }

        protected abstract DateTime Synchronize();
    }
}