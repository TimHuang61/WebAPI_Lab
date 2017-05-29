﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Binding_CSVMediaFormatter.Formatters;

namespace Binding_CSVMediaFormatter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Add(new ProductCsvFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
