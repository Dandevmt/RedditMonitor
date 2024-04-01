namespace RedditMonitor.Domain.RedditModels
{
    public class Listing
    {
        public string Kind { get; set; } = string.Empty;
        public SubredditPost Data { get; set; } = new SubredditPost();
    }
}