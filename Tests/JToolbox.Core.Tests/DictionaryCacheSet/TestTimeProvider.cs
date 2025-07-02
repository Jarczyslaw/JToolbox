using JToolbox.Core.TimeProvider;
using System;

namespace JToolbox.Core.Tests.DictionaryCacheSet
{
    internal class TestTimeProvider : ITimeProvider
    {
        public DateTime CurrentTime { get; set; } = DateTime.Now;

        public DateTime Now
        {
            get
            {
                CurrentTime += Step;
                return CurrentTime;
            }
        }

        public TimeSpan Step { get; set; } = TimeSpan.FromSeconds(1);
    }
}