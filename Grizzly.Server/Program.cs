using Grizzly.ConnectionManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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
            Console.WriteLine("Server started listeting port: " + Manager.Port);

            while (listener.IsListening)
            {
                await Task.Run(async () => await GetRequest(await listener.GetContextAsync()));
            }

            


            listener.Stop();
            listener.Close();
        }

        static async Task GetRequest(HttpListenerContext context)
        {
            Console.WriteLine("Got request");

            var str = context.Request.InputStream;
            using (StreamReader ms = new StreamReader(str))
            {
                string str1 = ms.ReadToEnd();
                Console.WriteLine(str1);
            }

            var response = context.Response;
            
        }
    }
}
