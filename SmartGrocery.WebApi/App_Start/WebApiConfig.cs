
using Microsoft.Web.Http.Routing;
using System.Web.Http;
using System.Web.Http.Routing;

namespace SmartGrocery.WebApi
{
    public static class WebApiConfig
    {
        private static void ConfigureWebApi(HttpConfiguration configuration)
        {
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof( ApiVersionRouteConstraint)
                }
            };

            configuration.AddApiVersioning(o => o.ReportApiVersions = true);

            configuration.MapHttpAttributeRoutes(constraintResolver);

            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}