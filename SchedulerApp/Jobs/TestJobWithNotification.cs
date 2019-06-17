using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Jobs
{
    public static class TestJobWithNotification
    {
        public static void Execute()
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("TestJobWithNotification job done");
        }
    }
}
