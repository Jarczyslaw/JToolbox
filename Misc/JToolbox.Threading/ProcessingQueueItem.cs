using System;

namespace JToolbox.Threading
{
    public class ProcessingQueueItem<TItem, TResult>
    {
        public ProcessingQueueItem(TItem item)
        {
            Item = item;
        }

        public TItem Item { get; }
        public TResult Result { get; set; }
        public Exception Exception { get; set; }
        public bool Processed { get; set; }

        public void Clear()
        {
            Processed = false;
            Exception = null;
            Result = default;
        }
    }
}