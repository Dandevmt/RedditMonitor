using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RedditMonitor.ConsoleApp;
using RedditMonitor.Domain.RedditModels;
using System.Collections.Generic;
using System.Linq;

namespace RedditMonitor.Tests.Domain
{
    [TestClass]
    public class SubredditStatisticsTests
    {
        [TestMethod]
        public void GetTopTenAuthors_ReturnsTopTenAuthors()
        {
            // Arrange
            var subredditPosts = new List<SubredditPost>
            {
                new() { Author_Fullname = "Author1" },
                new() { Author_Fullname = "Author2" },
                new() { Author_Fullname = "Author3" },
                new() { Author_Fullname = "Author4" },
                new() { Author_Fullname = "Author5" },
                new() { Author_Fullname = "Author6" },
                new() { Author_Fullname = "Author7" },
                new() { Author_Fullname = "Author8" },
                new() { Author_Fullname = "Author9" },
                new() { Author_Fullname = "Author10" },
                new() { Author_Fullname = "Author11" },
                new() { Author_Fullname = "Author12" },
                new() { Author_Fullname = "Author13" },
                new() { Author_Fullname = "Author13" },
                new() { Author_Fullname = "Author12" },
                new() { Author_Fullname = "Author11" },
                new() { Author_Fullname = "Author10" },
                new() { Author_Fullname = "Author9" },
                new() { Author_Fullname = "Author8" },
                new() { Author_Fullname = "Author7" },
                new() { Author_Fullname = "Author6" },
                new() { Author_Fullname = "Author5" },
                new() { Author_Fullname = "Author4" },
            }.AsQueryable();

            var statistics = new SubredditStatistics(subredditPosts);

            // Act
            var topAuthors = statistics.GetTopTenAuthors();

            // Assert
            Assert.AreEqual(10, topAuthors.Count());
            Assert.IsFalse(topAuthors.Any(x => x.Author == "Author3"));
        }

        [TestMethod]
        public void GetTopTenPosts_ReturnsTopTenPosts()
        {
            // Arrange
            var subredditPosts = new int[] { 10, 20, 30, 40, 50, 60, 80, 90, 100, 200, 300, 400, 500 }
                .Select(x => new SubredditPost()
                {
                    Ups = x
                }).AsQueryable();


            var statistics = new SubredditStatistics(subredditPosts);

            // Act
            var topPosts = statistics.GetTopTenPosts();

            // Assert
            Assert.AreEqual(10, topPosts.Count());
            Assert.IsFalse(topPosts.Any(x => x.Ups < 40));
        }

        [TestMethod]
        public void GetTotalPosts_ReturnsCountOfPosts()
        {
            // Arrange
            var subredditPosts = new int[10].Select(x => new SubredditPost()).AsQueryable();

            var statistics = new SubredditStatistics(subredditPosts);

            // Act
            var totalPosts = statistics.GetTotalPosts();

            // Assert
            Assert.AreEqual(10, totalPosts);
        }

        [TestMethod]
        public void GetTotalUpVotes_ReturnsCountOfUpVotes()
        {
            // Arrange
            var subredditPosts = new int[10].Select(x => new SubredditPost() 
            {
                Ups = 2
            }).AsQueryable();

            var statistics = new SubredditStatistics(subredditPosts);

            // Act
            var upVotes = statistics.GetTotalUpVotes();

            // Assert
            Assert.AreEqual(20, upVotes);
        }
    }
}
