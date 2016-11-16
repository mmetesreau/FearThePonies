using System;
using FearServer.Repositories;
using Microsoft.AspNet.SignalR;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace FearServer
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var duration = TimeSpan.FromHours(1);

            container.Register(new Configuration(DateTime.Now.Add(duration)));
            container.Register(new InMemoryUserRepository());
            container.Register(GlobalHost.ConnectionManager.GetHubContext<NotificationHub>());

            base.ApplicationStartup(container, pipelines);
        }
    }
}
