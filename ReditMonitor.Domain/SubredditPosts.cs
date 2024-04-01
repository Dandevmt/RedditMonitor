using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditMonitor.Domain.RedditModels;

namespace RedditMonitor.ConsoleApp
{
    public record SubredditPosts(IEnumerable<SubredditPost> Posts, RateLimitInfo RateLimitInfo);
}
