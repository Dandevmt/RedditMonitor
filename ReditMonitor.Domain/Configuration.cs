using Microsoft.Extensions.DependencyInjection;
using RedditMonitor.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditMonitor.Domain
{
    public static class Configuration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddTransient<SubredditDataUpdater>();

            return services;
        }
    }
}
