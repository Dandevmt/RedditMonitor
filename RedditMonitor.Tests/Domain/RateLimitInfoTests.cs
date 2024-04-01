using RedditMonitor.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditMonitor.Tests.Domain
{
    [TestClass]
    public class RateLimitInfoTests
    {
        [TestMethod]
        public void CalculateMsDelay_WhenRateLimitRemainingIsZero_ReturnsMsDelayDefault()
        {
            // Arrange
            var rateLimitInfo = new RateLimitInfo(10, 0, 60); 

            // Act
            var result = rateLimitInfo.CalculateMsDelay();

            // Assert
            Assert.AreEqual(RateLimitInfo.MsDelayDefault, result);
        }

        [TestMethod]
        public void CalculateMsDelay_WhenRateLimitRemainingIsGreaterThanZero_ReturnsCalculatedMsDelay()
        {
            // Arrange
            var rateLimitInfo = new RateLimitInfo(5, 10, 60); 

            // Act
            var result = rateLimitInfo.CalculateMsDelay();

            // Assert
            Assert.AreEqual(6000, result);
        }

        [TestMethod]
        public void CalculateMsDelay_WhenRateLimitResetIsZero_ReturnsZero()
        {
            // Arrange
            var rateLimitInfo = new RateLimitInfo(5, 10, 0); 

            // Act
            var result = rateLimitInfo.CalculateMsDelay();

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
