using BusinessDayApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessDayApi.Helper
{
    public class WeekdayProvider:IWeekdayProvider
    {
        private IPublicHolidayProvider publicHolidayProvider;

        public WeekdayProvider(IPublicHolidayProvider holidayProvider)
        {
            this.publicHolidayProvider = holidayProvider; 
        }
        /// <summary>
        /// Gets the number of weekdays between 2 date excluding public holidays.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int GetWeekDays(DateTime from, DateTime to)
        {
            var dayDifference = (int)to.Subtract(from).TotalDays - 1;
            List<PublicHoliday> publicHolidays = publicHolidayProvider.GetPublicHolidays(from, to);
            return Enumerable
                .Range(1, dayDifference)
                .Select(x => from.AddDays(x))
                .Count(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday 
                && publicHolidays!=null && !publicHolidays.Exists(p=> p.HolidayDate == x.Date));
        }


        /// <summary>
        /// Gets the number of weekdays in current Year.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int GetWeekDays()
        {
            DateTime _toDate = new DateTime(DateTime.Now.Year, 12, 31);
            DateTime _fromDate = new DateTime(DateTime.Now.Year, 1, 1);
            var dayDifference = (int)_toDate.Subtract(_fromDate).TotalDays - 1;
            List<PublicHoliday> publicHolidays = publicHolidayProvider.GetPublicHolidays(_fromDate, _toDate);
            return Enumerable
                .Range(1, dayDifference)
                .Select(x => _fromDate.AddDays(x))
                .Count(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday
                && publicHolidays != null && !publicHolidays.Exists(p => p.HolidayDate == x.Date));
        }
    }
}