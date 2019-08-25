using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IQCare.AIR.Infrastructure.Extensions
{
    public static class DateTimeExtension
    {
        public static string GetMonthAndYearFromDate(this DateTime date)
        {
            return $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month)}/{date.Year}";
        }
    }
}
