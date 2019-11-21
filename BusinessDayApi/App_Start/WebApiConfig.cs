﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BusinessDayApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


    config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }

        );
            config.Routes.MapHttpRoute(
           name: "ParamApi",
           routeTemplate: "api/{controller}/{startDate}/{endDate}",
           defaults: new { startDate = RouteParameter.Optional, endDate = RouteParameter.Optional }
       );

        }
    }
}
