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
using BLL.Services;

namespace Grizzly.Server
{
    public class Program
    {
        private static string _apiUrl = $"{Manager.Url}{Manager.Port}/";

        static async Task Main(string[] args)
        {
            Program program = new Program();
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(_apiUrl);
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
            JObject json;
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
                json = JObject.Parse(buf);
                Console.WriteLine(json.ToString());
            }

            switch(method)
            {
                case "GET":
                    break;
            }
        }
    }
}
