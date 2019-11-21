using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessDayApi.Models;
namespace BusinessDayApi.Helper
{
    public interface IPublicHolidayProvider
    {
        List<PublicHoliday> GetPublicHolidays(int year);
        List<PublicHoliday> GetPublicHolidays(DateTime startDate, DateTime endDate);
    }
}
