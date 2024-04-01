namespace RedditMonitor.Domain.RedditModels
{
    public class SubredditPost
    {
        public string Subreddit { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Author_Fullname { get; set; } = string.Empty;
        public string Selftext { get; set; } = string.Empty;
        public double Ups { get; set; }
    }
}