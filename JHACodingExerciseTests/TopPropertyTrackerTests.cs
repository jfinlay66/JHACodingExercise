using JHACodingExercise.Domain;
using System.Linq;
using Xunit;

namespace JHACodingExerciseTests
{
    public class TopPropertyTrackerTests
    {
        [Theory]
        [InlineData(null, 0, 0)]
        [InlineData(new string[] { "ElectionDay" }, 1, 1)]
        [InlineData(new string[] { "ElectionDay", "ElectionDay" }, 1, 2)]
        public void TopPropertyTracker_AddProperties_Success(string[] propertyList, int expectedLength, int expectedCount)
        {
            // arrange
            var topPropertyTracker = new TopPropertyTracker();

            // act
            topPropertyTracker.AddProperties(propertyList);

            // assert
            Assert.Equal(expectedLength, topPropertyTracker.TopProperties.Count());

            if (expectedLength > 0)
            {
                var expectedString = $"Hashtag:ElectionDay Count:{expectedCount}";
                Assert.Equal(expectedString, topPropertyTracker.TopProperties.First());
            }
        }
    }
}
