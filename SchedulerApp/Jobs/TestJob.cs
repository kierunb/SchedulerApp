using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Jobs
{
    public static class TestJob
    {
        public static void Execute()
        {
            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("Done");
        }
    }
}
