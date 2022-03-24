using System;
using System.Diagnostics;
using System.Globalization;

namespace JToolbox.Core.TimeProvider
{
    public abstract class TimeProviderBase : ITimeProvider
    {
        protected DateTime? synchronizationDate;
        private static readonly object sync = new object();
        private readonly Stopwatch stopwatch = new Stopwatch();
        public TimeSpan MaxOffset { get; set; } = TimeSpan.FromMinutes(30);

        public DateTime Now
        {
            get
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
        }

        public TimeSpan SynchronizationInterval { get; set; } = TimeSpan.FromHours(12);

        protected DateTime ParseDateTimeOffset(string dateTimeOffset, string format)
        {
            var dt = DateTimeOffset.ParseExact(dateTimeOffset,
                format,
                CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.AssumeUniversal);
            return dt.UtcDateTime.ToLocalTime();
        }

        protected abstract DateTime Synchronize();
    }
}