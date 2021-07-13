using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TReEDeCor
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PhroDuct",
                url: "san-pham",
                defaults: new { controller = "PhroDuct", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "trangchu",
                url: "",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TinTuc",
                url: "tin-tuc",
                defaults: new { controller = "TinTuc", action = "TinTucNew", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "SuKien",
                url: "su-kien",
                defaults: new { controller = "SuKien", action = "SuKienNew", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "BaoHanh-HauMai",
              url: "bao-hanh-hau-mai",
              defaults: new { controller = "BHHM", action = "BaoHanh_HauMai", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DangNhap",
              url: "dang-nhap",
              defaults: new { controller = "Dangnhap", action = "Nguoidung", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Lien He",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Contact", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
