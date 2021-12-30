using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ictshop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 "Default",
                 "{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Ictshop.Controllers" }

            );
                routes.MapRoute(
           name: "Search",
           url: "search",
           defaults: new { controller = "Sanpham", action = "Search", id = UrlParameter.Optional },
           namespaces: new[] { "Ictshop.Controllers" }
       );
        }
    }
}
