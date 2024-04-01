using System.Linq.Expressions;

namespace RedditMonitor.Domain.RedditModels
{
    public class RedditResult
    {
        public string Kind { get; set; } = string.Empty;
        public ListingData Data { get; set; } = new ListingData();
    }
}