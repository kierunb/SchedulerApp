using Hangfire;
using Rebus.Handlers;
using SchedulerApp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.MessageHandlers
{
    public class HelloMessageHandler : IHandleMessages<HelloMessage>
    {
        public async Task Handle(HelloMessage message)
        {
            await Task.Run(() => { });
            BackgroundJob.Enqueue(() => Jobs.TestJob.Execute());
            Debug.WriteLine($"message: {message.Message}, originator: {message.Originator}");
        }
    }
}