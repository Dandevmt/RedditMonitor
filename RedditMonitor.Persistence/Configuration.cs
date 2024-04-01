using Microsoft.Extensions.DependencyInjection;
using RedditMonitor.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RedditMonitor.Persistence
{
    public static class Configuration
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IStore, Store>();
        }
    }
}
