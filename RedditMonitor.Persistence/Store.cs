using RedditMonitor.ConsoleApp;
using RedditMonitor.Domain.RedditModels;

namespace RedditMonitor.Persistence
{
    public class Store : IStore
    {
        private readonly IDictionary<string, SubredditPost> posts = new Dictionary<string, SubredditPost>();
        private string subredditToMonitor = string.Empty;
        private RateLimitInfo rateLimitInfo = RateLimitInfo.Default;

        public void AddPosts(IEnumerable<SubredditPost> updatedData)
        {
            foreach (var post in updatedData)
            {
                if (posts.ContainsKey(post.Id))
                {
                    posts[post.Id] = post;
                } else
                {
                    posts.Add(post.Id, post);
                }
                
            }
        }

        public string GetSubredditToMonitor()
        {
            return subredditToMonitor;
        }

        public void SetSubredditToMonitor(string subreddit)
        {
            subredditToMonitor = subreddit;
        }

        public RateLimitInfo GetRateLimitInfo()
        {
            return this.rateLimitInfo ?? RateLimitInfo.Default;
        }

        public void SaveRateLimitInfo(RateLimitInfo rateLimitInfo)
        {
            this.rateLimitInfo = rateLimitInfo;
        }

        public IQueryable<SubredditPost> GetAllSubredditPosts(string subreddit)
        {
            return this.posts.Values.Where(x => x.Subreddit.Equals(subreddit)).AsQueryable();
        }
    }
}
