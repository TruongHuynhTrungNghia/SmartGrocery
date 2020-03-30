using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartGrocery.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: typeof(Startup).Assembly
                    .GetExportedTypes()
                    .Where(type => typeof(IController).IsAssignableFrom(type))
                    .Select(type => type.Namespace)
                    .Distinct()
                    .ToArray()
            ); ;
        }
    }
}