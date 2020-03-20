using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SmartGrocery.WebUI.Startup))]

namespace SmartGrocery.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            ConfigurationCompositionRoot(builder);
        }
    }
}