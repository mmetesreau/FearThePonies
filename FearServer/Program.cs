using System;
using Microsoft.AspNetCore.Hosting;

namespace FearServer
{
    public class Program
    {
        public const string APP_URL = "APP_URL";

        public static void Main(string[] args)
        {
            var url = Environment.GetEnvironmentVariable(APP_URL);

            var host = new WebHostBuilder()
                .UseUrls("http://*:8080")
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
