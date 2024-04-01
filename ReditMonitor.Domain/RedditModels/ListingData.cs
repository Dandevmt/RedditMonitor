namespace RedditMonitor.Domain.RedditModels
{
    public class ListingData
    {
        public string After { get; set; } = string.Empty;
        public IEnumerable<Listing> Children { get; set; } = Enumerable.Empty<Listing>();
    }
}