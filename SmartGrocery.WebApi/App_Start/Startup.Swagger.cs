using Microsoft.Web.Http.Routing;
using Owin;
using Swashbuckle.Application;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;

namespace SmartGrocery.WebApi
{
    partial class Startup
    {
        private void ConfigurateSwagger(IAppBuilder builder, HttpConfiguration configuration)
        {
            var httpServer = new HttpServer(configuration);

            configuration.AddApiVersioning(x => x.ReportApiVersions = true);

            configuration.EnableSwagger(
                "{apiVersion}/swagger",
                swagger =>
                {
                    swagger.SchemaId(x => x.FullName);

                    swagger.SingleApiVersion("v1", "SmartGroceryApi");

                    swagger.OperationFilter<ImplicitApiVersionParameter>();

                    swagger.IncludeXmlComments(GetXmlCommentsFilePath());
                })
                .EnableSwaggerUi(swagger => swagger.EnableDiscoveryUrlSelector());

            builder.UseWebApi(httpServer);

            string GetXmlCommentsFilePath()
            {
                var baseClass = System.AppDomain.CurrentDomain.RelativeSearchPath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(baseClass, fileName);
            }
        }
    }
}