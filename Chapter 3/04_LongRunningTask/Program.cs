using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _04_LongRunningTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory
                .StartNew(WhatTypeOfThreadAmI, TaskCreationOptions.LongRunning)
                .Wait();
        }

        private static void WhatTypeOfThreadAmI()
        {
            Console.WriteLine("I'm a {0} thread", Thread.CurrentThread.IsThreadPoolThread ? "Thread Pool" : "Custom");
        }
    }
}
