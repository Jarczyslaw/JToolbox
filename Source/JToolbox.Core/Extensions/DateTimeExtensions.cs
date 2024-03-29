﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace JToolbox.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static List<DateTime> GetDatesTo(this DateTime @this, DateTime endDate, TimeSpan interval)
        {
            var result = new List<DateTime>();
            var currentDate = @this;
            while (currentDate <= endDate)
            {
                result.Add(currentDate);
                currentDate += interval;
            }
            return result;
        }

        public static int GetIsoWeekNumber(this DateTime date)
        {
            //https://docs.microsoft.com/pl-pl/archive/blogs/shawnste/iso-8601-week-of-year-format-in-microsoft-net
            DayOfWeek day = CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }

            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static bool IsDateEarlierThan(this DateTime dt1, DateTime dt2)
        {
            return DateTime.Compare(dt1.Date, dt2.Date) <= 0;
        }

        public static bool IsDateInRange(this DateTime dateTime, DateTime start, DateTime end)
        {
            return dateTime.IsLaterThan(start.Date) && dateTime.IsEarlierThan(end.Date);
        }

        public static bool IsDateLaterThan(this DateTime dt1, DateTime dt2)
        {
            return DateTime.Compare(dt1.Date, dt2.Date) >= 0;
        }

        public static bool IsDateNotInRange(this DateTime dateTime, DateTime start, DateTime end)
        {
            return DateTime.Compare(dateTime.Date, start.Date) < 0 || DateTime.Compare(dateTime.Date, end.Date) > 0;
        }

        public static bool IsEarlierThan(this DateTime dt1, DateTime dt2)
        {
            return DateTime.Compare(dt1, dt2) <= 0;
        }

        public static bool IsInRange(this DateTime dateTime, DateTime start, DateTime end)
        {
            return dateTime.IsLaterThan(start) && dateTime.IsEarlierThan(end);
        }

        public static bool IsLaterThan(this DateTime dt1, DateTime dt2)
        {
            return DateTime.Compare(dt1, dt2) >= 0;
        }

        public static bool IsNotInRange(this DateTime dateTime, DateTime start, DateTime end)
        {
            return DateTime.Compare(dateTime, start) < 0 || DateTime.Compare(dateTime, end) > 0;
        }

        public static bool IsWeekend(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsWorkday(this DateTime @this)
        {
            return !IsWeekend(@this);
        }

        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static DateTime NextWorkday(this DateTime @this)
        {
            var current = @this.AddDays(1);
            while (!current.IsWorkday()) { current = current.AddDays(1); }
            return current;
        }
    }
}