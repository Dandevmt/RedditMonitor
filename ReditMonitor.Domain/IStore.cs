
using RedditMonitor.Domain.RedditModels;

namespace RedditMonitor.ConsoleApp
{
    public interface IStore
    {
        void AddPosts(IEnumerable<SubredditPost> posts);
        IQueryable<SubredditPost> GetAllSubredditPosts(string subreddit);
        RateLimitInfo GetRateLimitInfo();
        string GetSubredditToMonitor();
        void SaveRateLimitInfo(RateLimitInfo rateLimitInfo);
        void SetSubredditToMonitor(string subreddit);
    }
}