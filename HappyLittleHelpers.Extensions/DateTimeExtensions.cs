using System;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class DateTimeExtensions
    {
        public static DateTime ToEndOfYear(this DateTime dateTime, bool keepTime = false,
            CultureInfo cultureInfo = null)
        {
            return ToStartOfYear(dateTime, keepTime).AddDays(dateTime.GetDaysInYear(cultureInfo));
        }

        public static DateTime ToStartOfYear(this DateTime dateTime, bool keepTime = false)
        {
            var hour = keepTime ? dateTime.Hour : 0;
            var minute = keepTime ? dateTime.Minute : 0;
            var seconds = keepTime ? dateTime.Second : 0;

            return new DateTime(dateTime.Year, 1, 1, hour, minute, seconds);
        }

        public static DateTime ToStartOfMonth(this DateTime dateTime, bool keepTime = false) =>
            ToStartOfMonthInternal(dateTime, 0, keepTime);

        public static DateTime ToStartOfPreviousMonth(this DateTime dateTime, bool keepTime = false) =>
            ToStartOfMonthInternal(dateTime, -1, keepTime);

        public static DateTime ToEndOfMonth(this DateTime dateTime, bool keepTime = false,
            CultureInfo cultureInfo = null) => ToEndOfMonthInternal(dateTime, 0, keepTime, cultureInfo);
        
        public static DateTime ToEndOfPreviousMonth(this DateTime dateTime, bool keepTime = false) =>
            dateTime.ToStartOfMonth(keepTime).AddMonths(-1);

        public static int GetDaysInMonth(this DateTime dateTime, CultureInfo cultureInfo)
        {
            return (cultureInfo ?? CultureInfo.CurrentCulture).Calendar.GetDaysInMonth(dateTime.Year, dateTime.Month);
        }

        public static int GetDaysInYear(this DateTime dateTime, CultureInfo cultureInfo)
        {
            return (cultureInfo ?? CultureInfo.CurrentCulture).Calendar.GetDaysInYear(dateTime.Year);
        }
        
        private static DateTime ToEndOfMonthInternal(this DateTime dateTime, int offset = 0, bool keepTime = false,
            CultureInfo cultureInfo = null)
        {
            cultureInfo ??= CultureInfo.CurrentCulture;
            return dateTime.ToStartOfMonth(keepTime).AddDays(dateTime.GetDaysInMonth(cultureInfo)).AddMonths(offset);
        }
    
        private static DateTime ToStartOfMonthInternal(DateTime dateTime, int offset = 0, bool keepTime = false)
        {
            var hour = keepTime ? dateTime.Hour : 0;
            var minute = keepTime ? dateTime.Minute : 0;
            var seconds = keepTime ? dateTime.Second : 0;

            return new DateTime(dateTime.Year, dateTime.Month, 1, hour, minute, seconds).AddMonths(offset);
        }
    }
}