using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using BusinessDayApi.Helper;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace BusinessDayApi.Controllers
{
    [System.Web.Mvc.RoutePrefix("api/businessdays")]
    public class WeekDayController : ApiController
    {
        private readonly IWeekdayProvider _weekDayProvider;
        public WeekDayController(IWeekdayProvider weekdayProvider)
        {
            this._weekDayProvider = weekdayProvider;
        }

        [HttpGet]
        [Route("")]
        public object Get()
        {
            int totalWeekDays = _weekDayProvider.GetWeekDays();
            return new
            {
                Message = "Total business days",
                Total = totalWeekDays
            };
        }
        [HttpGet]        
        [Route("GetByDate/{startDate=}/{endDate=}")]
        // GET api/values/5
        public object Get(string startDate, string endDate)
        {
            
            DateTime from = DateTime.ParseExact(startDate.Replace("\"", "").Trim(), "dd-MM-yyyy",CultureInfo.InvariantCulture);
            
            DateTime to = DateTime.ParseExact(endDate.Replace("\"", "").Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            int totalWeekDays = _weekDayProvider.GetWeekDays(from,to);
            return new
            {
                Message = "Total business days",
                Total = totalWeekDays
            };
        }


    }
}
