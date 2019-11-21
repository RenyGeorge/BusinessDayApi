using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessDayApi.Helper
{
    public static class DayFinder
    {
        /// <summary>
        /// Get the next occurence of a day of week in a month based on the frequency
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static DateTime GetDate(DayOfWeek day,int month, int year, int frequency)
        {
            if (frequency == 0 || frequency > 5)
            {
                throw new Exception("Exceeds the number of weeks in a month");
            }

            var firstDayOfMonth = new DateTime(year, month, 1);
            
            var daysNeeded = (int)day - (int)firstDayOfMonth.DayOfWeek;
                        
            if (daysNeeded < 0)
            {
                daysNeeded = daysNeeded + 7;
            }

            
            var resultedDay = (daysNeeded + 1) + (7 * (frequency - 1));

            if (resultedDay > DateTime.DaysInMonth(year, month))
            {
                return DateTime.MinValue;
            }

            return new DateTime(year, month, resultedDay);
        }
    }
    
}