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
            //BackgroundJob.Enqueue(() => Jobs.TestJob.Execute());
            Debug.WriteLine($"Got It! {nameof(HelloMessage)}: {message.Message}, originator: {message.Originator}");
        }
    }

    public class HelloMessageHandler2 : IHandleMessages<HelloMessage2>//, IHandleMessages<NotificationMessage>
    {
        public async Task Handle(HelloMessage2 message)
        {
            await Task.Run(() => { });
            //BackgroundJob.Enqueue(() => Jobs.TestJob.Execute());
            Debug.WriteLine($"Got It! {nameof(HelloMessage2)}: {message.Message}, originator: {message.Originator}");
        }

        public async Task Handle(NotificationMessage message)
        {
            await Task.Run(() => { });
            //BackgroundJob.Enqueue(() => Jobs.TestJob.Execute());
            Debug.WriteLine($"Got It! {nameof(NotificationMessage)}: {message.Notification}, originator: {message.ProcessName}");
        }
    }
}