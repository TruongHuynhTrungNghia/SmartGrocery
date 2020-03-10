using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(SmartGrocery.WebUI.Startup))]

namespace SmartGrocery.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            var configuration = new HttpConfiguration();

            ConfigurationCompositionRoot(builder);

        }
    }
}