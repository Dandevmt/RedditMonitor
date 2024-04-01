

namespace RedditMonitor.RedditApi
{
    public class RedditApiOptions
    {
        public string ClientId { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
        public string GrantType { get; set; } = string.Empty;
        public string TokenUrl { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}