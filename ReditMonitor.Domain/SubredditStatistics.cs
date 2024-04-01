using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditMonitor.Domain.RedditModels;

namespace RedditMonitor.ConsoleApp
{
    public class SubredditStatistics
    {
        private readonly IQueryable<SubredditPost> subredditPosts;

        public SubredditStatistics(IQueryable<SubredditPost> subredditPosts)
        {
            this.subredditPosts = subredditPosts ?? Array.Empty<SubredditPost>().AsQueryable();
        }

        public IEnumerable<TopTenAuthor> GetTopTenAuthors()
        {
            return subredditPosts
                .GroupBy(x => x.Author_Fullname)
                .OrderByDescending(x => x.Count())
                .Take(10)
                .Select(x => new TopTenAuthor(x.Key, x.Count()));
        }

        public IEnumerable<SubredditPost> GetTopTenPosts()
        {
            return subredditPosts
                .OrderByDescending(x => x.Ups)
                .Take(10);                
        }

        public double GetTotalPosts()
        {
            return subredditPosts
                .Count();
        }

        public double GetTotalUpVotes()
        {
            return subredditPosts
                .Sum(x => x.Ups);
        }
    }
}
