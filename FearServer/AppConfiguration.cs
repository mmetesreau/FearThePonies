using System;

namespace FearServer
{
    public class AppConfiguration
    {
        public readonly DateTime EndOfTheWorld;

        public AppConfiguration(DateTime endOfTheWorld)
        {
            EndOfTheWorld = endOfTheWorld;
        }
    }
}
