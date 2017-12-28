using System;

namespace StepApp.CommonExtensions.Date
{
   public static class DateTimeHelper
    {
        public static TimeSpan ToTimeSpanHoursMinutesFromZero(this DateTime dateTime)
        {
            return new TimeSpan(dateTime.Hour, dateTime.Minute, 0);
        }
    }
}
