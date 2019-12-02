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
            name: "AdmiTag",
            url: "admin/tag/{action}/{id}",
            defaults: new { controller = "Tag", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
            name: "AdminCategory",
            url: "admin/category/{action}/{id}",
            defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("Account", "admin/{action}",
            new { controller = "Account", action = "Login", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
