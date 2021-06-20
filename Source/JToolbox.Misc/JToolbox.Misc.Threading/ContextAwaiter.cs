using System.Threading;

namespace JToolbox.Misc.Threading
{
    public class ContextAwaiter
    {
        private readonly SynchronizationContext context;

        public ContextAwaiter(SynchronizationContext context = null)
        {
            if (context == null)
            {
                context = SynchronizationContext.Current;
            }
            this.context = context;
        }

        public NotifyCompletion GetAwaiter()
        {
            return new NotifyCompletion(context);
        }
    }
}