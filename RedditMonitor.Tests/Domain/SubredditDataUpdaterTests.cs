using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using RedditMonitor.ConsoleApp;
using RedditMonitor.Domain.RedditModels;

namespace RedditMonitor.Tests.Domain
{
    [TestClass]
    public class SubredditDataUpdaterTests
    {
        [TestMethod]
        public async Task Update_WithValidSubreddit_CallsUpdateData()
        {
            // Arrange
            var storeMock = new Mock<IStore>();
            storeMock.Setup(s => s.GetSubredditToMonitor()).Returns("testsubreddit");

            var redditApiMock = new Mock<IRedditApi>();
            redditApiMock.Setup(s => s.GetSubredditPostsAsync(It.IsAny<string>()))
                .ReturnsAsync(new SubredditPosts(new List<SubredditPost>(), RateLimitInfo.Default));

            var logger = NullLogger<SubredditDataUpdater>.Instance;

            var updater = new SubredditDataUpdater(storeMock.Object, redditApiMock.Object, logger);

            // Act
            await updater.Update();

            // Assert
            redditApiMock.Verify(api => api.GetSubredditPostsAsync("testsubreddit"), Times.Once);
        }

        [TestMethod]
        public async Task Update_WithEmptySubreddit_DoesNotCallUpdateData()
        {
            // Arrange
            var storeMock = new Mock<IStore>();
            storeMock.Setup(s => s.GetSubredditToMonitor()).Returns("");

            var redditApiMock = new Mock<IRedditApi>();
            var logger = NullLogger<SubredditDataUpdater>.Instance;

            var updater = new SubredditDataUpdater(storeMock.Object, redditApiMock.Object, logger);

            // Act
            await updater.Update();

            // Assert
            redditApiMock.Verify(api => api.GetSubredditPostsAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task UpdateData_CallsRedditApiAndStoresData()
        {
            // Arrange
            var storeMock = new Mock<IStore>();
            storeMock.Setup(x => x.GetSubredditToMonitor()).Returns("testsubreddit");

            var redditApiMock = new Mock<IRedditApi>();
            var testData = new SubredditPosts(new List<SubredditPost>(), RateLimitInfo.Default);
            redditApiMock.Setup(s => s.GetSubredditPostsAsync(It.IsAny<string>()))
                .ReturnsAsync(testData);

            var logger = NullLogger<SubredditDataUpdater>.Instance;

            var updater = new SubredditDataUpdater(storeMock.Object, redditApiMock.Object, logger);

            // Act
            await updater.Update();

            // Assert
            storeMock.Verify(store => store.AddPosts(testData.Posts), Times.Once);
            storeMock.Verify(store => store.SaveRateLimitInfo(testData.RateLimitInfo), Times.Once);
        }
    }
}