using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace _09_TaskRelationships
{
    class Program
    {
        static void Main(string[] args)
        {
            //WhenAny();
            WhenAll();
        }

        private static void WhenAny()
        {
            Console.WriteLine(DownloadWebPageAsync(new string[]
            {
                "http://www.rocksolidknowledge2.com",
                "http://www.bbc.co.uk2",
                "http://www.apress.com"
            }).Result);
        }

        private static void WhenAll()
        {
            Task[] importTasks =
                (from file in new DirectoryInfo(@"..\..\..\data").GetFiles("*.xml")
                 select Task.Run(() => ProcessFile(file.FullName))).ToArray();

            Task.Factory.ContinueWhenAll(importTasks, _ => Console.WriteLine("All done")).Wait();
        }

        private static void ProcessFile(string fullName)
        {
            throw new NotImplementedException();
        }

        private static Task<string> DownloadWebPageAsync(string[] urls)
        {
            List<Task<WebResponse>> requestsOutStanding = (from url in urls select WebRequest.Create(url).GetResponseAsync()).ToList();

            return Task.Factory.ContinueWhenAny(requestsOutStanding.ToArray(), completedRequest =>
            {
                requestsOutStanding.Remove(completedRequest);

                while ((completedRequest.Status != TaskStatus.RanToCompletion) && (requestsOutStanding.Count > 0))
                {
                    completedRequest = requestsOutStanding[Task.WaitAny(requestsOutStanding.ToArray())];
                    requestsOutStanding.Remove(completedRequest);
                }

                if (completedRequest.Status != TaskStatus.RanToCompletion) return "";

                using (var reader = new StreamReader(completedRequest.Result.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            });
        }
    }
}
