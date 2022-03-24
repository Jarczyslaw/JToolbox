using System;

namespace JToolbox.Core.TimeProvider
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}