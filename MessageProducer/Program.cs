using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;
using Rebus.Routing.TypeBased;
using SchedulerApp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProducer
{
    class Program
    {
        private static string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HangfireDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                //adapter.Handle<HelloMessage>(async reply =>
                //{
                //    await Console.Out.WriteLineAsync($"Got reply '{reply.Message}' (from OS process {reply.Originator})");
                //});

                Configure.With(activator)
                    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Info))
                    .Transport(t => t.UseSqlServer(connString, inputQueueName: "producer.input"))
                    .Routing(r => r.TypeBased().MapAssemblyOf<HelloMessage>("consumer.input")) // destination queue
                    //.Subscriptions(s => s.StoreInSqlServer(connString, "subscriptions"))
                    .Start();

                Console.WriteLine("Press Q to quit or any other key to publish a job");
                while (true)
                {
                    var keyChar = char.ToLower(Console.ReadKey(true).KeyChar);

                    switch (keyChar)
                    {
                        case 'q':
                            goto quit;

                        default:
                            activator.Bus
                                .Send(new HelloMessage
                                {
                                    Message = "Ho ho",
                                    Originator = Process.GetCurrentProcess().ProcessName
                                })
                                .Wait();
                            Console.WriteLine($"Message {nameof(HelloMessage)} sent");

                            activator.Bus
                                .Send(new HelloMessage2
                                {
                                    Message = "Ha ha",
                                    Originator = Process.GetCurrentProcess().ProcessName
                                })
                                .Wait();
                            Console.WriteLine($"Message {nameof(HelloMessage2)} sent");
                            break;
                    }
                }

                quit:
                Console.WriteLine("Quitting...");
            }
        }
    }
}
