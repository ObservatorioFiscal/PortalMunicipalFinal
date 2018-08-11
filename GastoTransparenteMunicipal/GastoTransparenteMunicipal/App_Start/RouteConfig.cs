using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GastoTransparenteMunicipal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{municipality}/{controller}/{action}/{id}",
                //defaults: new { municipality = "RECOLETA", controller = "Home", action = "Index", id = UrlParameter.Optional }
                defaults: new { municipality = "list" ,controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
