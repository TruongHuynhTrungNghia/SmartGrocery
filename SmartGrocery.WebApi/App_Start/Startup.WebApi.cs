using Microsoft.Web.Http.Routing;
using System.Web.Http;
using System.Web.Http.Routing;

namespace SmartGrocery.WebApi
{
    public partial class Startup
    {
        private void ConfigureWebApi(HttpConfiguration configuration)
        {
            ConfigurateRouting();

            void ConfigurateRouting()
            {
                var contraintResolver = ConfigureApiVersion(configuration);

                configuration.MapHttpAttributeRoutes(contraintResolver);

                //configuration.Routes.MapHttpRoute(
                //    name: "DefaultApi",
                //    routeTemplate: "api/v{version:apiVersion}/{controller}/{id}",
                //    defaults: new { id = RouteParameter.Optional });
            }
        }

        private IInlineConstraintResolver ConfigureApiVersion(HttpConfiguration configuration)
        {
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof(ApiVersionRouteConstraint)
                }
            };

            configuration.AddApiVersioning(o => o.ReportApiVersions = true);

            return constraintResolver;
        }
    }
}