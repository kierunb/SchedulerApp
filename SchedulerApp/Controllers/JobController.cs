using Hangfire;
using Rebus.Bus;
using SchedulerApp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchedulerApp.Controllers
{
    [RoutePrefix("api")]
    public class JobController : ApiController
    {
        private readonly IBus bus;

        public JobController(IBus bus)
        {
            this.bus = bus;
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Job()
        {
            BackgroundJob.Enqueue( () => Jobs.TestJob.Execute() );

            await Task.Run( () => "Dummy await" );
            return Ok();
        }

        [Route("test")]
        [HttpGet]
        public async Task<IHttpActionResult> Test()
        {
            await bus.Send(new NotificationMessage {
                Notification ="raport ready",
                ProcessName = Process.GetCurrentProcess().ProcessName });
            return Ok();
        }
    }
}
