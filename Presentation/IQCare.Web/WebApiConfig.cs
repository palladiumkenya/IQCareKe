using System.Web.Http;

namespace IQCare.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
           // config.Routes.MapHttpRoute(
           //    name: "DefaultApi",
           //    routeTemplate: "api/{controller}/{id}",
           //    defaults: new { id = RouteParameter.Optional }
           //);
            // config.Routes.MapHttpRoute(
            //     name: "HTSAPI",
            //     routeTemplate: "myhts/api/{controller}/{id}",
            //     defaults: new { id = RouteParameter.Optional }
            // );
            config.Routes.MapHttpRoute(
                name: "InteroptApi",
                routeTemplate: "api/interop/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "PsmartApi",
                routeTemplate: "api/psmart/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
              name: "ConfigApi",
              routeTemplate: "api/config/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
            config.Routes.MapHttpRoute(
                name: "HTSAPI",
                routeTemplate: "myhts/api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}