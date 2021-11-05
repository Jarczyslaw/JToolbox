using JToolbox.Core.Abstraction;
using System;

namespace JToolbox.Misc.TimeProviders
{
    public class LocalTimeProvider : ITimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}