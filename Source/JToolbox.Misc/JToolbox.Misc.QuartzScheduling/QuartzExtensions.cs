using Quartz;
using System;

namespace JToolbox.Misc.QuartzScheduling
{
    public static class QuartzExtensions
    {
        public static bool IsMisfired(this IJobExecutionContext context, int offsetMilliseconds = 1000)
        {
            DateTimeOffset fireTimeUtc = context.FireTimeUtc;

            if (!context.ScheduledFireTimeUtc.HasValue || fireTimeUtc == default) { return false; }

            DateTimeOffset scheduledFireTimeUtc = context.ScheduledFireTimeUtc.Value;

            return fireTimeUtc.Subtract(scheduledFireTimeUtc).TotalMilliseconds > offsetMilliseconds;
        }
    }
}