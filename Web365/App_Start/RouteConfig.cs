using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web365
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Ajax",
                "ajax/{controller}/{action}",
                new[] { "Web365.Controllers" }
            );

            routes.MapRoute(
                name: "ContactUs",
                url: "lien-he",
                defaults: new { controller = "ContactUs", action = "Index", prodId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "News",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Promotion",
                url: "khuyen-mai",
                defaults: new { controller = "News", action = "Promotion" }
            );

            routes.MapRoute(
                name: "PromotionDetail",
                url: "khuyen-mai/{nameascii}",
                defaults: new { controller = "News", action = "PromotionDetail" }
            );

            routes.MapRoute(
                name: "NewsDetail",
                url: "tin-tuc/{nameascii}",
                defaults: new { controller = "News", action = "NewsDetail", nameascii = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "Introduce",
                url: "gioi-thieu",
                defaults: new { controller = "Home", action = "About" }
            );
            
            routes.MapRoute(
                name: "Service",
                url: "dich-vu",
                defaults: new { controller = "Service", action = "Index" }
            );
            
            routes.MapRoute(
                name: "ServiceDetail",
                url: "dich-vu/{nameAscii}",
                defaults: new { controller = "Service", action = "ServiceDetail", nameAscii = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "Product",
                url: "san-pham",
                defaults: new { controller = "Product", action = "Index", page = UrlParameter.Optional, orderBy = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "ProductDetail",
                url: "san-pham/{nameAscii}",
                defaults: new { controller = "Product", action = "ProductDetail", nameAscii = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
