using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace _06_IOBasedTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //string download = DownloadWebPage("http://slowwly.robertomurray.co.uk/delay/3000/url/http://www.google.co.uk");
            //Console.WriteLine(download);

            //Task<string> downloadTask = DownloadWebPageAsync("http://slowwly.robertomurray.co.uk/delay/3000/url/http://www.google.co.uk");

            Task<string> downloadTask = BetterDownoadWebPageAsync45("http://slowwly.robertomurray.co.uk/delay/3000/url/http://www.google.co.uk");

            while (!downloadTask.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(250);
            }

            Console.WriteLine(downloadTask.Result);




        }

        private static string DownloadWebPage(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            {
                return reader.ReadToEnd();
            }
        }

        private static Task<string> DownloadWebPageAsync(string url)
        {
            return Task.Factory.StartNew(() => DownloadWebPage(url));
        }

        private static Task<string> BetterDownloadWebPageAsync(string url)
        {
            WebRequest request = WebRequest.Create(url);
            IAsyncResult ar = request.BeginGetResponse(null, null);

            Task<string> downloadTask =
                Task.Factory
                    .FromAsync<string>(ar, iar =>
                        { using (WebResponse response = request.EndGetResponse(iar))
                            {
                                using (var reader = new StreamReader(response.GetResponseStream()))
                                {
                                    return reader.ReadToEnd();
                                }
                            }
                        });

            return downloadTask;
        }

        private static async Task<string> BetterDownoadWebPageAsync45(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = await request.GetResponseAsync();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string result = await reader.ReadToEndAsync();

                return result;
            }
        }
    }
}
