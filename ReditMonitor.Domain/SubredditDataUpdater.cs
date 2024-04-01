using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditMonitor.ConsoleApp
{
    public class SubredditDataUpdater
    {
        private readonly IStore store;
        private readonly IRedditApi redditApi;
        private readonly ILogger<SubredditDataUpdater> logger;

        public SubredditDataUpdater(IStore store, IRedditApi redditApi, ILogger<SubredditDataUpdater> logger)
        {
            this.store = store;
            this.redditApi = redditApi;
            this.logger = logger;
        }

        public async Task Update()
        {
            var subredditToMonitor = store.GetSubredditToMonitor();

            if (string.IsNullOrWhiteSpace(subredditToMonitor))
            {
                logger.LogWarning("No subreddit to monitor");
            } else
            {
                await UpdateData(subredditToMonitor);
            }
        }

        private async Task UpdateData(string subreddit)
        {
            var data = await redditApi.GetSubredditPostsAsync(subreddit);

            store.AddPosts(data.Posts);

            store.SaveRateLimitInfo(data.RateLimitInfo);
        }
    }
}
