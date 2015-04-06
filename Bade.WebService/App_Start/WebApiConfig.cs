using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Bade.Admin.Model.Mapper;

namespace Bade.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "CustomApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            FormUrlEncodedMediaTypeFormatter f = new FormUrlEncodedMediaTypeFormatter();
            f.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            f.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-www-form-urlencoded"));


            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.Insert(0, new JsonMediaTypeFormatter());
            config.Formatters.Add(f);


            AutoMapperConfiguration.LoadConfig();

        }
    }
}
