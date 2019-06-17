using Rebus.Activation;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Logging;
using SchedulerApp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageConsumer
{

    /// <summary>
    /// Purpose of this console app is to demostrate message consumer code
    /// Messages will be published by Web API
    /// </summary>

    class Program
    {

        private static string connString = "server=(localdb)\\mssqllocaldb; database=HangfireDB; integrated security=true";

        static void Main(string[] args)
        {
            Console.WriteLine("Consumer app\n");

            using (var activator = new BuiltinHandlerActivator())
            {

                activator.Register( () => new NotificationHandler() );

                Configure.With(activator)
                    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Info))
                    .Transport(t => t.UseSqlServer(connString, "notifications"))
                    .Start();

                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
            }
        }
        
    }

    class NotificationHandler : IHandleMessages<NotificationMessage>
    {
        public async Task Handle(NotificationMessage msg)
        {
            Console.WriteLine($"Got it: {nameof(NotificationMessage)} from process {msg.ProcessName}");
        }
    }
}
