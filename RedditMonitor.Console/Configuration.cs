using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditMonitor.RedditApi;
using RedditMonitor.ConsoleApp;
using RedditMonitor.Persistence;
using RedditMonitor.Domain;

namespace RedditMonitor.ConsoleApp
{
    public static class Configuration
    {
        internal static IHost BuildHost(string[]? args)
        {
            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            var hostBuilder = Host.CreateApplicationBuilder(args);

            // Setup Reddit API
            var redditOptions = config.GetSection(nameof(RedditApiOptions)).Get<RedditApiOptions>()!;
            hostBuilder.Services.AddRedditApi(redditOptions);

            // Setup Input Loop & Monitoring Background Service
            hostBuilder.Services.AddSingleton<InputLoopService>();
            hostBuilder.Services.AddHostedService<MonitorService>();

            // Setup Persistence
            hostBuilder.Services.AddPersistence();

            // Setup Domain
            hostBuilder.Services.AddDomain();

            return hostBuilder.Build();
        }
    }
}
