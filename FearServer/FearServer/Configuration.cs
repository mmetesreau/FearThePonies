using System;

namespace FearServer
{
    public class Configuration
    {
        public readonly DateTime EndOfTheWorld;

        public Configuration(DateTime endOfTheWorld)
        {
            EndOfTheWorld = endOfTheWorld;
        }
    }
}
