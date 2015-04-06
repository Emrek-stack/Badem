using System.Web;
using System.Web.Http;

namespace Bade.WebService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutofacBootsrapper.Register();
            GlobalConfiguration.Configure(WebApiConfig.Register);
           
        }
    }
}
