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
        static void Main(string[] args)
        {
            using (var adapter = new BuiltinHandlerActivator())
            {
                adapter.Handle<HelloMessage>(async reply =>
                {
                    await Console.Out.WriteLineAsync($"Got reply '{reply.Message}' (from OS process {reply.Originator})");
                });

                Configure.With(adapter)
                    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Info))
                    //.Transport(t => t.UseSqlServer("server=.; database=rebus; trusted_connection=true", "Messages", "producer.input"))
                    .Transport(t => t.UseMsmq("producer"))
                    .Routing(r => r.TypeBased().MapAssemblyOf<HelloMessage>("jobs-queue"))
                    .Start();

                Console.WriteLine("Press Q to quit or any other key to produce a job");
                while (true)
                {
                    var keyChar = char.ToLower(Console.ReadKey(true).KeyChar);

                    switch (keyChar)
                    {
                        case 'q':
                            goto quit;

                        default:
                            adapter.Bus
                                .Send(new HelloMessage {
                                    Message = "Ho ho",
                                    Originator = Process.GetCurrentProcess().ProcessName })
                                .Wait();
                            Console.WriteLine("Message sent");
                            break;
                    }
                }

                quit:
                Console.WriteLine("Quitting...");
            }
        }
    }
}
