using Autofac;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using SchedulerApp.MessageHandlers;
using SchedulerApp.Messages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchedulerApp.App_Start
{
    public static class RebusConfig
    {
        public static void ConfigureRebus(ContainerBuilder autofacContainerBuilder)
        {

            string connString = ConfigurationManager.ConnectionStrings["hangfire-db"].ConnectionString;

            autofacContainerBuilder.RegisterRebus((config, context) => config
                .Logging(l => l.ColoredConsole(minLevel: Rebus.Logging.LogLevel.Warn))
                .Transport(t => t.UseSqlServer(connString, "consumer.input"))      // where to fetch
                .Routing(r => r.TypeBased().MapAssemblyOf<NotificationMessage>("notifications"))
                .Options(o => {
                    //o.SetNumberOfWorkers(1);
                    //o.SetMaxParallelism(30);
                }));

            // register message handlers
            //autofacContainerBuilder.RegisterHandler<HelloMessageHandler>();
            autofacContainerBuilder.RegisterHandlersFromAssemblyOf<HelloMessageHandler>();

            // bus itself will be started when container will be built
            // https://github.com/rebus-org/Rebus.Autofac

            // rebus integratin with Nlog is also possible
            // https://github.com/rebus-org/Rebus.NLog
        }
    }
}
