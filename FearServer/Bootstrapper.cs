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
            var duration = TimeSpan.FromHours(1);

            container.Register(new AppConfiguration(DateTime.Now.Add(duration)));
            container.Register(new InMemoryUserRepository());
            container.Register(Startup.ConnectionManager.GetHubContext<NotificationHub>());

            base.ApplicationStartup(container, pipelines);
        }
    }
}
