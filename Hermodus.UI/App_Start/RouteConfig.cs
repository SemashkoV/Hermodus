﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hermodus.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(null, "{controller}/{action}/{postId}",
             
              new { postId = @"\d+" }

              );
            routes.MapRoute(null, "{controller}/{action}/{category}/{page}",
               new { controller = "Post", action = "PostByCategory" },
               new { category = @"\d+" , page = @"\d+" }
              
               );

        }
    }
}
