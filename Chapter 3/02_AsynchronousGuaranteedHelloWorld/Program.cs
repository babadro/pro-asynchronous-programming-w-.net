using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_AsynchronousGuaranteedHelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = new Task(Speak);
            t.Start();
            Console.WriteLine("Waiting for completion");
            t.Wait();
            Console.WriteLine("All Done");
        }

        private static void Speak()
        {
            Console.WriteLine("Hello World");
        }
    }
}
