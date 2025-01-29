using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace JToolbox.Core.Utilities
{
    public delegate void TimeBufferFlush<T>(TimeBuffer<T> sender, List<T> itemsToFlush);

    public abstract class TimeBuffer<T>
    {
        private static readonly object _lock = new object();
        private readonly List<T> _items = new List<T>();
        private readonly Timer _timer;

        protected TimeBuffer()
        {
            _timer = new Timer();
            _timer.Elapsed += TimerElapsed;
        }

        public event TimeBufferFlush<T> OnFlush;

        public int? MaxItemsCount { get; set; }

        public TimeSpan? Timeout { get; set; }

        public void Add(T item)
        {
            List<T> itemsToFlush = null;

            lock (_lock)
            {
                _items.Add(item);

                if (IsFlushNeeded(_items))
                {
                    itemsToFlush = GetItemsToFlushAndReset();
                }
                else if (_items.Count == 1 && Timeout != null)
                {
                    _timer.Interval = Timeout.Value.TotalMilliseconds;
                    _timer.Start();
                }
            }

            TryFlush(itemsToFlush);
        }

        public void Clear(bool flush)
        {
            lock (_lock)
            {
                List<T> itemsToFlush = GetItemsToFlushAndReset();

                if (flush)
                {
                    TryFlush(itemsToFlush);
                }
            }
        }

        protected virtual void Flush(List<T> itemsToFlush)
        { }

        protected virtual bool IsFlushNeeded(List<T> items)
        {
            return MaxItemsCount != null
                && items.Count >= MaxItemsCount.Value;
        }

        private List<T> GetItemsToFlushAndReset()
        {
            List<T> itemsToFlush = _items.ToList();
            _items.Clear();
            _timer.Stop();

            return itemsToFlush;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            List<T> itemsToFlush = null;

            lock (_lock)
            {
                if (_items.Count == 0) { return; }

                itemsToFlush = GetItemsToFlushAndReset();
            }

            TryFlush(itemsToFlush);
        }

        private void TryFlush(List<T> itemsToFlush)
        {
            if (itemsToFlush == null || itemsToFlush.Count == 0) { return; }

            Flush(itemsToFlush);
            OnFlush?.Invoke(this, itemsToFlush);
        }
    }
}