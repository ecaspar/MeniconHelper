using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MeniconHelper
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            /*
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
            */
            routes.MapRoute(
                name: "LocalizedDefault",
                url: "{lang}/{controller}/{action}",
                defaults: new { controller = "Login", action = "Index" },
                constraints: new { lang = "fr-FR|en-US" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Login", action = "Index", lang = "fr-FR|en-US" }
            );

        }
    }
}
