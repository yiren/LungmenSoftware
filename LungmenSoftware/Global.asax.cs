using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LungmenSoftware.Helper;
using LungmenSoftware.Models.CodeFirst;
using Microsoft.Ajax.Utilities;
using WebGrease.Configuration;
using System.Web.Http;

namespace LungmenSoftware
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ChangeProcessDbContext, LungmenSoftware.MigrationForChangeRequestData.Configuration>());
            //ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            //ValueProviderFactories.Factories.Add(new JsonDotNetValueProviderFactory());
            
        }
    }
}
