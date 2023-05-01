using Grizzly.Server.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Grizzly.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new NinjectResolver();

            // Конфигурация и службы Web API

            // Маршруты Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
