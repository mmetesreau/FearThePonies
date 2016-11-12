using System;
using Microsoft.Owin.Hosting;

namespace FearServer
{
    public class Program
    {

        public static void Main(string[] _)
        {
            using (WebApp.Start<Startup>("http://*:8000"))
            {
                Console.WriteLine("Server started. Press any key to quit.");
                Console.ReadKey();
            }
        }
    }
}
