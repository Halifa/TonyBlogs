using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TonyBlogs.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Blog",
                url: "Blog",
                defaults: new { controller = "Home", action = "Blog", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Essay",
                url: "Essay",
                defaults: new { controller = "Home", action = "Essay", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BlogDetail",
                url: "b/{blogID}",
                defaults: new { controller = "BlogDetail", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}