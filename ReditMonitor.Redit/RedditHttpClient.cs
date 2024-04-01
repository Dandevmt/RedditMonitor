using RedditMonitor.ConsoleApp;
using RedditMonitor.Domain.RedditModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RedditMonitor.RedditApi
{
    public class RedditHttpClient : IRedditApi
    {
        private readonly HttpClient httpClient;
        public RedditHttpClient(HttpClient httpClient) 
        {
            this.httpClient = httpClient;
        }

        public async Task<SubredditPosts> GetSubredditPostsAsync(string subreddit)
        {
            var response = await httpClient.GetAsync($"/r/{subreddit}/new.json");

             response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RedditResult>();

            if (result == null)
            {
                throw new NullReferenceException("Error deserializing response from reddit");
            }

            return new SubredditPosts(
                result.Data.Children.Select(x => x.Data),
                GetRateLimitInfo(response));
        }

        public RateLimitInfo GetRateLimitInfo(HttpResponseMessage response)
        {

            return new RateLimitInfo(
                GetRateLimitFromHeader(response.Headers, "x-ratelimit-used"),
                GetRateLimitFromHeader(response.Headers, "x-ratelimit-remaining"),
                GetRateLimitFromHeader(response.Headers, "x-ratelimit-reset"));

        }

        private double GetRateLimitFromHeader(HttpResponseHeaders headers, string key)
        {
            if (headers.TryGetValues(key, out IEnumerable<string>? values))
            {
                if (values != null && values.Count() > 0 && double.TryParse(values.First(), out double result))
                {
                    return result;
                }
            }

            return 0;
        }
    }
}
