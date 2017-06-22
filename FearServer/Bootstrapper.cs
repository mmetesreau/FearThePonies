using System;
using FearServer.Repositories;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Nancy.Configuration;

namespace FearServer
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: true, displayErrorTraces: true);
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {

            container.Register(GetAppConfiguration());
            container.Register(new InMemoryUserRepository());
            container.Register(new InMemoryScoreRepository());
            container.Register(Startup.ConnectionManager.GetHubContext<NotificationHub>());

            base.ApplicationStartup(container, pipelines);
        }

        private AppConfiguration GetAppConfiguration()
        {
            DateTime endOfTheWorld;

            if (!DateTime.TryParse(Environment.GetEnvironmentVariable("END"), out endOfTheWorld))
            {
                var commandLineArgs = Environment.GetCommandLineArgs();
                if (commandLineArgs?.Length != 3 || commandLineArgs[1] != "--end" || !DateTime.TryParse(commandLineArgs[2], out endOfTheWorld))
                {
                    endOfTheWorld = DateTime.Now.Add(TimeSpan.FromMinutes(60));
                }
            }
            return new AppConfiguration(endOfTheWorld);
        }
    }
}
