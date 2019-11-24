using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;

namespace GreenNub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
          //  HttpConfiguration configuration = new HttpConfiguration();
            // Web API configuration and services
          //////////////////  config.Filters.Add(new ApplicationAuthenticationHandler());
           // config.Services.Replace(typeof(IHttpControllerSelector), new HttpNotFoundAwareDefaultHttpControllerSelector(config));
            //   config.Services.Replace(typeof(IHttpActionSelector), new HttpNotFoundAwareControllerActionSelector());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
    
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
          
            config.EnableCors(cors);

            
        }

      
    }
}
