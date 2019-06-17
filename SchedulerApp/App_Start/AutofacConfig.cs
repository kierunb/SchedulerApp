using Autofac;
using Autofac.Integration.WebApi;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using SchedulerApp.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchedulerApp.App_Start
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            // CUSTOM modules
            //builder.RegisterType<ToInject>().As<IToInject>();

            RebusConfig.ConfigureRebus(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build(); 
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
