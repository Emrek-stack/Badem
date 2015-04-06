using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Bade.Core.Ioc;

namespace Bade.WebService
{
    public class AutofacBootsrapper
    {
        public static void Register()
        {
            ContainerBuilder builder = Bootstrapper.Run();

            //Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(config);

            //Build Container
            IContainer container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

    }
}