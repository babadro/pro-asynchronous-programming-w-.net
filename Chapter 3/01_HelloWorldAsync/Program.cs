using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_HelloWorldAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = new Task(Speak);
            t.Start();
        }

        private static void Speak()
        {
            Console.WriteLine("Hello World");
        }
    }
}
