using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessDayApi.Models
{
    public class PublicHoliday
    {
        public DateTime HolidayDate { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }

        public PublicHoliday(DateTime holidayDate,string name)
        {
            this.HolidayDate = holidayDate;
            this.Name = name;
            this.Year = holidayDate.Year;
            this.Month = holidayDate.Month;
            this.Day = holidayDate.Day;
        }

        public PublicHoliday(int day,int month, int year, string name)
        {
            this.HolidayDate = new DateTime(year,month,day);
            this.Name = name;
        }

        
        [JsonConstructor]
        public PublicHoliday()
        {
            if (this.Year != null)
            {
                HolidayDate = new DateTime((int)this.Year, (int)this.Month, (int)this.Day);
            }
        }

        public bool IsWeekend()
        {
            if(HolidayDate!=null)
            {
                if(HolidayDate.DayOfWeek == DayOfWeek.Saturday || HolidayDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    return true;
                }                
            }
            return false;
        }
    }
}