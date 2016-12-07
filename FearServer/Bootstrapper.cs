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
            container.Register(Startup.ConnectionManager.GetHubContext<NotificationHub>());

            base.ApplicationStartup(container, pipelines);
        }

        private AppConfiguration GetAppConfiguration()
        {
            var duration = TimeSpan.FromHours(1);

            var commandLineArgs = Environment.GetCommandLineArgs();

            if (commandLineArgs?.Length == 3 && commandLineArgs[1] == "--duration")
            {
                int minutes;

                if (int.TryParse(commandLineArgs[2], out minutes))
                {
                    duration = TimeSpan.FromMinutes(minutes);
                }
            }

            return new AppConfiguration(DateTime.Now.Add(duration));
        }
    }
}
