using JHACodingExercise.Domain;
using System.Collections.Generic;
using Xunit;

namespace JHACodingExerciseTests
{
    public class TopPropertyWithAverageTrackerTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 100)]
        [InlineData(1, 1, 50)]
        [InlineData(0, 1, 0)]
        public void TopPropertyWithAverageTracker_AddProperties_Success(int numWithProperties, int numWithoutProperties, double expectedPercent)
        {
            // arrange
            var propertyList = new List<string>() { "Heart", "Donut" };

            var topPropertyWithAverageTracker = new TopPropertyWithAverageTracker();

            // act
            for (int i=0; i<numWithProperties; i++)
            {
                topPropertyWithAverageTracker.AddProperties(propertyList);
            }

            for (int i = 0; i < numWithoutProperties; i++)
            {
                topPropertyWithAverageTracker.AddProperties(new string[] { });
            }

            // assert
            Assert.Equal(expectedPercent, topPropertyWithAverageTracker.PercentWithProperty);
        }
    }
}
