namespace RedditMonitor.ConsoleApp
{
    public interface IRedditApi
    {
        public Task<SubredditPosts> GetSubredditPostsAsync(string subreddit);
    }
}
