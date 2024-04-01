using Microsoft.Extensions.DependencyInjection;
using RedditMonitor.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditMonitor.RedditApi
{
    public static class Configuration
    {
        public static IServiceCollection AddRedditApi(this IServiceCollection services, Action<RedditApiOptions> configureOptions)
        {
            var options = new RedditApiOptions();
            configureOptions(options);
            return services.AddRedditApi(options);         
        }

        public static IServiceCollection AddRedditApi(this IServiceCollection services, RedditApiOptions options)
        {
            services
                .AddSingleton<ITokenCache, TokenCache>()
                .AddSingleton(options)
                .AddSingleton<AuthHandler>()                
                .AddHttpClient<IRedditApi, RedditHttpClient>(o =>
                {
                    o.BaseAddress = new Uri(options.BaseUrl);
                })
                .AddHttpMessageHandler<AuthHandler>();

            return services;
        }
    }
}
