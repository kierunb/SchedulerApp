using Autofac;
using Autofac.Integration.WebApi;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using SchedulerApp.MessageHandlers;
using System;
using System.Collections.Generic;
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

            //register rebus
            //builder.RegisterRebus((cnfg, context) => cnfg
            //    .Logging(l => l.None())
            //    .Transport(t => t.UseMsmq("jobs-queue"))
            //    .Routing(r => r.TypeBased().MapAssemblyOf<Messages.HelloMessage>("producer"))
            //    .Options(o => {
            //        o.SetNumberOfWorkers(2);
            //        o.SetMaxParallelism(30);
            //    }));

            //builder.RegisterHandler<HelloMessageHandler>();
            //builder.RegisterHandlersFromAssemblyOf<HelloMessageHandler>();


            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
