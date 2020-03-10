using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(SmartGrocery.WebApi.Startup))]

namespace SmartGrocery.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            var configuration = new HttpConfiguration();

            CompositionRoot(builder, configuration);
            ConfigureWebApi(configuration);
            ConfigurateSwagger(builder, configuration);
        }
    }
}