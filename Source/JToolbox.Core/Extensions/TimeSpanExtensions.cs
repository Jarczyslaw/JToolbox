using JToolbox.Core.Enums;
using System;

namespace JToolbox.Core.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpanFormat GetTimeSpanFormat(this TimeSpan timeSpan)
        {
            if (timeSpan.TotalMilliseconds < 1000) { return TimeSpanFormat.Millis; }
            if (timeSpan.TotalSeconds < 60) { return TimeSpanFormat.Seconds; }
            if (timeSpan.TotalMinutes < 60) { return TimeSpanFormat.Minutes; }
            return TimeSpanFormat.Hours;
        }

        public static string GetTimeSpanFormattedString(this TimeSpan timeSpan, string stringFormat = "N2")
        {
            var format = GetTimeSpanFormat(timeSpan);
            return GetTimeSpanFormattedString(timeSpan, format, stringFormat);
        }

        public static string GetTimeSpanFormattedString(this TimeSpan timeSpan, TimeSpanFormat format, string stringFormat = "N2")
        {
            if (format == TimeSpanFormat.Millis) { return $"{timeSpan.TotalMilliseconds.ToString(stringFormat)} ms"; }
            if (format == TimeSpanFormat.Seconds) { return $"{timeSpan.TotalSeconds.ToString(stringFormat)} sec"; }
            if (format == TimeSpanFormat.Minutes) { return $"{timeSpan.TotalMinutes.ToString(stringFormat)} min"; }
            return $"{timeSpan.TotalHours.ToString(stringFormat)} h";
        }
    }
}