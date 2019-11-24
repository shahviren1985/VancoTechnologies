using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BhaariAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{country}/{city}",
                defaults: new { controller = "Home", action = "Index", country = UrlParameter.Optional, city = UrlParameter.Optional }
            ).RouteHandler = new HyphenatedRouteHandler();
        }

        public class HyphenatedRouteHandler : MvcRouteHandler
        {
            protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                if (requestContext.RouteData.Values["controller"] != null)
                {
                    requestContext.RouteData.Values["controller"] = requestContext.RouteData.Values["controller"].ToString().Replace("-", "");
                }

                if (requestContext.RouteData.Values["action"] != null)
                {
                    requestContext.RouteData.Values["action"] = requestContext.RouteData.Values["action"].ToString().Replace("-", "");
                }

                return base.GetHttpHandler(requestContext);
            }
        }
    }
}
