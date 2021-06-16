using System;
using System.Globalization;

namespace happy_little_helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime ToEndOfYear(this DateTime dateTime, bool keepTime = false, CultureInfo cultureInfo = null)
        {

            var culture = cultureInfo ?? CultureInfo.CurrentCulture;
            var month = culture.Calendar.GetMonthsInYear(dateTime.Year);
            var day = culture.Calendar.GetDaysInMonth(dateTime.Year,month);
            var hour = keepTime ? dateTime.Hour : 0;
            var minute = keepTime ? dateTime.Minute : 0;
            var seconds = keepTime ? dateTime.Second : 0;
 
            return new DateTime(dateTime.Year, month, day, hour, minute, seconds);
        }
    }
}