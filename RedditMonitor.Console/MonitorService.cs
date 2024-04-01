using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditMonitor.ConsoleApp
{
    internal class MonitorService : BackgroundService
    {
        private readonly SubredditDataUpdater subredditDataUpdater;
        private readonly IStore store;
        private readonly ILogger<MonitorService> logger;

        public MonitorService(SubredditDataUpdater subredditDataUpdater, IStore store, ILogger<MonitorService> logger)
        {
            this.subredditDataUpdater = subredditDataUpdater;
            this.store = store;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await subredditDataUpdater.Update();

                    var msDelay = store.GetRateLimitInfo().CalculateMsDelay();

                    await Task.Delay(TimeSpan.FromMilliseconds(msDelay), stoppingToken);
                } catch (Exception ex)
                {
                    logger.LogError("Error updating reddit data\n{message}\n {exception}", ex.Message, ex);
                    logger.LogInformation("Trying again in 5 seconds");
                } finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                               
            }           
        }
    }
}
