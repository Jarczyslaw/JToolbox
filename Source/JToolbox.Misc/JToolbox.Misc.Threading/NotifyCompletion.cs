using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace JToolbox.Misc.Threading
{
    public struct NotifyCompletion : INotifyCompletion
    {
        private static readonly SendOrPostCallback postCallback = state => ((Action)state)();

        private readonly SynchronizationContext context;

        public NotifyCompletion(SynchronizationContext context)
        {
            this.context = context;
        }

        public bool IsCompleted => context == SynchronizationContext.Current;

        public void GetResult()
        {
        }

        public void OnCompleted(Action continuation) => context.Post(postCallback, continuation);
    }
}