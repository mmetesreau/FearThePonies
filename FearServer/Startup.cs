using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Nancy.Owin;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FearServer
{
    public class Startup
    {
        public static IConnectionManager ConnectionManager;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            app.UseSignalR();

            ConnectionManager = serviceProvider.GetService<IConnectionManager>();

            app.UseOwin(x => x.UseNancy());
        }
    }
}
