using System;
using System.Threading;

namespace JToolbox.Core.Helpers
{
    public class SingleAppInstanceHelper
    {
        private readonly Mutex _mutex;

        public SingleAppInstanceHelper(string appKey)
        {
            _mutex = new Mutex(true, appKey);
        }

        public bool IsAlreadyRunning()
        {
            if (_mutex.WaitOne(TimeSpan.Zero, true))
            {
                _mutex.ReleaseMutex();
                return false;
            }

            return true;
        }
    }
}