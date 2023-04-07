using Grizzly.ConnectionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Grizzly.Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add($"{Manager.Url}{Manager.Port}/");
            listener.Start();

            while (listener.IsListening)
            {
                await Task.Run(async () => await GetRequest(await listener.GetContextAsync()));
            }

            


            listener.Stop();
            listener.Close();
        }

        static async Task GetRequest(HttpListenerContext context)
        {
            Console.WriteLine("Request");
            var response = context.Response;
            
        }
    }
}
