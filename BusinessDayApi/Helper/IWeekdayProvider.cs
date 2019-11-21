using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDayApi.Helper
{
    public interface IWeekdayProvider
    {
        int GetWeekDays(DateTime from, DateTime to);
        int GetWeekDays();
    }
}
