using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using BusinessDayApi.Models;
using Newtonsoft.Json;

namespace BusinessDayApi.Helper
{
    public class PublicHolidayProvider:IPublicHolidayProvider
    {
        private string fixedHolidayJsonFile = "fixedHolidays.json";

        /// <summary>
        /// Returns list of public holidays for a year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<PublicHoliday> GetPublicHolidays(int year)
        {
            List<PublicHoliday> publicHolidays = new List<PublicHoliday>();
            publicHolidays = GetDayBasedHolidays(year);

            DateTime easterSunday = GetEasterSunday(year);
            DateTime labourDay = DayFinder.GetDate(DayOfWeek.Monday, 10, year, 1);
            DateTime queenBirthday = DayFinder.GetDate(DayOfWeek.Monday, 6, year,2);

            publicHolidays.Add(new PublicHoliday(easterSunday, "Easter Sunday"));
            publicHolidays.Add(new PublicHoliday(easterSunday.AddDays(1), "Easter Monday"));

            return publicHolidays.OrderBy(t=>t.HolidayDate).ToList();
        }

        /// <summary>
        /// Filter public holidays based on date range.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<PublicHoliday> GetPublicHolidays(DateTime startDate, DateTime endDate)
        {
            List<PublicHoliday> publicHolidays = new List<PublicHoliday>();
           
            publicHolidays = GetPublicHolidays(startDate.Year);

           if(endDate.Year != startDate.Year)
            {
                publicHolidays.AddRange(GetPublicHolidays(endDate.Year));
            }

           if(publicHolidays.Count()>0)
            {

                return publicHolidays.Where(t => t.HolidayDate != null && t.HolidayDate > startDate && t.HolidayDate < endDate).ToList();
            }
            return publicHolidays;
        }

        /// <summary>
        /// Returns the list of fixed holidays based on date for a year
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<PublicHoliday> GetFixedHolidays(int Year)
        {
            List<PublicHoliday> fixedHolidays = new List<PublicHoliday>();
            var data = Properties.Resources.fixedHolidays;
            string json = Encoding.UTF8.GetString(data, 0, data.Length);
            fixedHolidays = JsonConvert.DeserializeObject<List<PublicHoliday>>(json);
            fixedHolidays.ForEach(t =>
            {
                t.Year = Year;
                t.HolidayDate = new DateTime((int)t.Year, (int)t.Month, (int)t.Day);
            });
            return fixedHolidays;
        }

        /// <summary>
        /// If the public holiday falls on weekend identify the next weekday as holiday.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<PublicHoliday> GetDayBasedHolidays(int year)
        {
            List<PublicHoliday> publicHolidays = GetFixedHolidays(year);
            for (int i=0; i< publicHolidays.Count; i++)
            {
                PublicHoliday holiday = publicHolidays[i];
                if ((holiday.HolidayDate == new DateTime(year, 1, 1)
                    || holiday.HolidayDate == new DateTime(year, 1, 26)
                    || holiday.HolidayDate == new DateTime(year, 4, 25)
                    || holiday.HolidayDate == new DateTime(year, 12, 25)
                    || holiday.HolidayDate == new DateTime(year, 12, 26)
                    ) && holiday.IsWeekend())
                {
                    int addDays = 1;
                    while (holiday.HolidayDate.AddDays(addDays).DayOfWeek < DayOfWeek.Monday || publicHolidays.Exists(t=>t.HolidayDate == holiday.HolidayDate.AddDays(addDays) && !holiday.IsWeekend()))
                    {
                        addDays++;
                    }
                    string holidayName = String.Concat(holiday.Name, " ", holiday.HolidayDate.AddDays(addDays).DayOfWeek.ToString());
                  
                    publicHolidays.Add(new PublicHoliday(holiday.HolidayDate.AddDays(addDays), holidayName) );

                }
            }
            return publicHolidays;

        }
        /// <summary>
        /// Returns easter sunday for a current year
        /// Reference Oskar Wieland's algorithm
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DateTime GetEasterSunday(int year)
        {
            int g = year % 19;
            int c = year / 100;
            int h = h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25)
                                                + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) *
                        (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            var day = i - ((year + (int)(year / 4) +
                          i + 2 - c + (int)(c / 4)) % 7) + 28;
            var month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }
            return new DateTime(year, month, day);
        }
    }
}