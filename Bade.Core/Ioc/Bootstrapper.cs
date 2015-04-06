using System.Configuration;
using Autofac;
using Bade.Constants.Helper;
using Bade.Data.Dapper;
using Bade.Infrastructure;
using Bade.Infrastructure.Configuration;
using Bade.Lib.Configuration;
using Bade.Manager.Impl;

namespace Bade.Core.Ioc
{
    public class Bootstrapper
    {
        public static ContainerBuilder Run()
        {
            return SetAutofacContainer();
        }

        private static ContainerBuilder SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConfigReader>()
                .As<IConfigReader>()
                .AsImplementedInterfaces()
                .SingleInstance();


            builder.RegisterType<MsSqlConnectionFactory>()
            .As<IConnectionFactory>()
            .WithParameter("connectionStringName", ConfigurationManager.AppSettings["DefaultConnectionString"])
            .SingleInstance();
            builder.RegisterType<BadeApi>().As<IBadeClient>().SingleInstance();


            builder.RegisterAssemblyTypes(typeof(MemberRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().SingleInstance();


            builder.RegisterAssemblyTypes(typeof(ApplicationManager).Assembly)
           .Where(t => t.Name.EndsWith("Manager"))
           .AsImplementedInterfaces().SingleInstance();



            return builder;
        }
    }
}
