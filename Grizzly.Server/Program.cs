using Grizzly.ConnectionManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var request = context.Request;
            var stream = request.InputStream;
            string method = request.HttpMethod;
            Console.WriteLine("Got request");
            Console.WriteLine($"[HTTP METHOD] -> {method}");
            Console.WriteLine($"[DATE] -> {DateTime.Now}");
            Console.WriteLine($"[LOCAL] -> {request.IsLocal}");
            Console.WriteLine($"Remote end point -> {request.RemoteEndPoint}");
            Console.WriteLine($"Request length -> {request.ContentLength64}");
            Console.WriteLine($"Requested Url -> {request.Url}");
            using (StreamReader reader = new StreamReader(stream))
            {
                string buf = reader.ReadToEnd();
                var json = JObject.Parse(buf);
                Console.WriteLine(json.ToString());
            }
        }
    }
}
