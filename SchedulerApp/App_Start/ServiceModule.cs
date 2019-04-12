using Autofac;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.App_Start
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder b)
        {
            base.Load(b);
            b.RegisterAssemblyTypes(GetType().Assembly)
                .Where(IsRebusHandler)
                .AsImplementedInterfaces();
        }
            

        private bool IsRebusHandler(Type t)
        {
            return typeof(IHandleMessages).IsAssignableFrom(t);
        }
    }
}
