using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using SchedulerApp.App_Start;

[assembly: OwinStartup(typeof(SchedulerApp.Startup))]

namespace SchedulerApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HangfireConfig.Configure(app);
        }
    }
}
