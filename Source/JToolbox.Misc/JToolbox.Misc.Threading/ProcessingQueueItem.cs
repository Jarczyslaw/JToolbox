﻿using System;

namespace JToolbox.Misc.Threading
{
    public class ProcessingQueueItem<TInput, TOutput>
    {
        public ProcessingQueueItem(TInput input)
        {
            Input = input;
        }

        public Exception Exception { get; set; }
        public TInput Input { get; }
        public TOutput Output { get; set; }
        public bool Processed { get; set; }

        public void Clear()
        {
            Processed = false;
            Exception = null;
            Output = default;
        }
    }
}