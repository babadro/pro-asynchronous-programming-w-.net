using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace _07_ErrorHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            UnobserveredTaskException();
            //ErrorHandling();
        }

        private static void ErrorHandling()
        {
            Task task = Task.Factory.StartNew(() => Import(@"..\..\data\2.xml"));

            try
            {
                task.Wait();
            }
            catch (AggregateException errors)
            {
                foreach (Exception error in errors.Flatten().InnerExceptions)
                {
                    Console.WriteLine("{0} : {1}", error.GetType().Name, error.Message);
                }
                errors.Handle(IgnoreXmlErrors);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
        }

        private static void UnobserveredTaskException()
        {
            NonObserveredExceptionsTerminateOnError();
        }

        private static void NonObserveredExceptionsTerminateOnError()
        {
            Task t = Task.Factory.StartNew(() => TerminateOnUnHandledException(() => { throw new Exception("Boom!"); }));
            t = null;
            object[] garbage = new object[10000];
            Random rnd = new Random();
            while (true)
            {
                if (rnd.Next(garbage.Length) == 1) Thread.Sleep(1);
                garbage[rnd.Next(garbage.Length)] = new object();
            }
        }

        private static void TerminateOnUnHandledException(Action body)
        {
            try
            {
                body();
            }
            catch (Exception error)
            {
                Environment.FailFast(error.Message);
            }
        }

        private static bool IgnoreXmlErrors(Exception arg)
        {
            return (arg is XmlException);
        }

        private static void Import(string fullName)
        {
            XElement doc = XElement.Load(fullName);
        }
    }
}
