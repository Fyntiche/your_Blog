using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace your_Blog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           

            routes.MapRoute(
            name: "Admin",
            url: "admin/tagmodels/{action}/{id}",
            defaults: new { controller = "TagModels", action = "Index", id = UrlParameter.Optional }) ;

            //routes.MapRoute(
            //name: "Admin",
            //url: "admin/tagmodels/{action}/{id}",
            //defaults: new { controller = "TagModels", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("Account", "admin/{action}",
            new { controller = "Account", action = "Login", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } 
            );
        }
    }
}
