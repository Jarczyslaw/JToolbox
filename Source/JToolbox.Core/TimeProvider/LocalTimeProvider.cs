using System;

namespace JToolbox.Core.TimeProvider
{
    public class LocalTimeProvider : ITimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}