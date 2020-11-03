using JHACodingExercise.Domain;
using System;
using Xunit;

namespace JHACodingExerciseTests
{
    public class AveragesTrackerTests
    {
        /// <summary>
        /// This test could probably be set up better - calculating elapsed time with private variables is causing testing problems
        /// I'm not fond of exposing variables or adding method overrides just to satisfy testing
        /// </summary>
        /// <param name="tweetsReceived">number of tweets to run in test</param>
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void AveragesTracker_ComputesAverages_Success(int tweetsReceived)
        {
            // arrange
            var averagesTracker = new AveragesTracker();

            // act
            for (int i=0; i < tweetsReceived; i++)
            {
                averagesTracker.Recompute();
            }

            var elapsedSeconds = averagesTracker.TotalTweets / averagesTracker.AveragePerSecond; // find the elapsed time used in the computation

            var expectedAveragePerSecond = tweetsReceived / elapsedSeconds;
            var expectedAveragePerMinute = Math.Round(averagesTracker.AveragePerSecond * 60, 5);
            var expectedAveragePerHour = Math.Round(averagesTracker.AveragePerMinute * 60, 5);

            var actualAveragePerSecond = averagesTracker.AveragePerSecond;
            var actualAveragePerMinute = Math.Round(averagesTracker.AveragePerMinute, 5);
            var actualAveragePerHour = Math.Round(averagesTracker.AveragePerHour, 5);

            // assert
            Assert.Equal(tweetsReceived, averagesTracker.TotalTweets);
            Assert.Equal(expectedAveragePerSecond, actualAveragePerSecond);
            Assert.Equal(expectedAveragePerMinute, actualAveragePerMinute);
            Assert.Equal(expectedAveragePerHour, actualAveragePerHour);
        }
    }
}
