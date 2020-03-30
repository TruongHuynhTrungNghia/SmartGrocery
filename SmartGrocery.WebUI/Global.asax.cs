﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SmartGrocery.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}