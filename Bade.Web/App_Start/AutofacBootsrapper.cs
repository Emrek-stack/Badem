using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Bade.Core.Ioc;

namespace Bade.Web
{
    public class AutofacBootsrapper
    {
        public static void Register()
        {
            ContainerBuilder builder = Bootstrapper.Run();
            //For MVC
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterFilterProvider();
            IContainer container = builder.Build();           
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}