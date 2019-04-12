using Hangfire;
using System;
using System.Collections.Generic;
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
        [Route("job")]
        [HttpGet]
        public async Task<IHttpActionResult> Job()
        {
            BackgroundJob.Enqueue( () => Jobs.TestJob.Execute() );


            await Task.Run( () => "Dummy await" );
            return Ok();
        }

    }
}
