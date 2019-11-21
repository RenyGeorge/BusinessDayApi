using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessDayApi.Helper;
using BusinessDayApi.Models;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace BusinessDayApi.Controllers
{
  
    public class PublicHolidayController : System.Web.Http.ApiController
    {
        private readonly IPublicHolidayProvider _publicHolidayProvider;

        public PublicHolidayController(IPublicHolidayProvider publicHolidayProvider)
        {
            this._publicHolidayProvider = publicHolidayProvider;
        }

        [HttpGet]
       [Route("api/publicHolidays")]
        public object Get()
        {
            List<PublicHoliday> publicHolidays = _publicHolidayProvider.GetPublicHolidays(DateTime.Now.Year);
            return new
            {
                Message = "List of public holidays",
                PublicHolidays = publicHolidays
            };
        }

        [HttpGet]
        [Route("api/publicHolidaysByYear/{year}")]
        public object GetByYear(int year)
        {
            List<PublicHoliday> publicHolidays = _publicHolidayProvider.GetPublicHolidays(year);
            return new
            {
                Message = "List of public holidays",
                PublicHolidays = publicHolidays
            };
        }
        [HttpGet]
        [Route("api/publicholidaysbydate/{startDate=}/{endDate=}")]
        
        public object GetByDate(string startDate, string endDate)
        {
            DateTime from = DateTime.ParseExact(startDate.Replace("\"", "").Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            DateTime to = DateTime.ParseExact(endDate.Replace("\"", "").Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            List<PublicHoliday> publicHolidays = _publicHolidayProvider.GetPublicHolidays(from, to);
            return new
            {
                Message = "List of public holidays",
                PublicHolidays = publicHolidays
            };
        }
    }
}
