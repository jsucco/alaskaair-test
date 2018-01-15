using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AlaskaAir_CodeTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FileConfig.CacheAppData(); 
        }

        protected void Application_BeginRequest()
        {
            VarifyCacheObjects(); 
        }

        #region helpers
        private void VarifyCacheObjects()
        {
            if (HttpRuntime.Cache["flights-data"] == null || HttpRuntime.Cache["airports-data"] == null)
                FileConfig.CacheAppData();
        }
        #endregion
    }

}
