using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bade.Admin.Model.Mapper;
using Bade.Infrastructure.Helper;

namespace Bade.Admin
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacBootsrapper.Register();
            AutoMapperConfiguration.LoadConfig();
            GlobalConfiguration.LoadConfiguration();
        }
    }
}
