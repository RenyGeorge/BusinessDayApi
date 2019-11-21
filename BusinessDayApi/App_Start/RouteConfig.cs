using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BusinessDayApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "publicHolidays",
            //    url: "api/{controller}/{id}",
            //    defaults: new { controller = "PublicHoliday",  id = UrlParameter.Optional }
            //);
        }
    }
}
