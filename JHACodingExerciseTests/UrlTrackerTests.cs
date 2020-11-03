using JHACodingExercise.Domain;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JHACodingExerciseTests
{
    public class UrlTrackerTests
    {
        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(0, 0, 1, 0)]
        [InlineData(0, 1, 0, 0)]
        [InlineData(0, 1, 1, 0)]
        [InlineData(1, 0, 0, 100)]
        [InlineData(1, 0, 1, 50)]
        [InlineData(1, 1, 0, 50)]
        [InlineData(1, 4, 5, 10)]
        public void UrlTracker_AddProperties_Success(int numWithPicDomain, int numWithoutPicDomain, int numWithoutUrl, double expectedPercent)
        {
            // arrange
            var urlListWithPicDomain = new List<URL>() { new URL() { display_url = "pic.twitter.com/subfolder" } };
            var urlListWithoutPicDomain = new List<URL>() { new URL() { display_url = "google.com" } };

            var urlTracker = new UrlTracker();

            // act
            for (int i = 0; i < numWithPicDomain; i++)
            {
                urlTracker.AddProperties(urlListWithPicDomain);
            }

            for (int i = 0; i < numWithoutPicDomain; i++)
            {
                urlTracker.AddProperties(urlListWithoutPicDomain);
            }

            for (int i = 0; i < numWithoutUrl; i++)
            {
                urlTracker.AddProperties((List<URL>)null);
            }

            // assert
            Assert.Equal(expectedPercent, urlTracker.PercentWithPhotoUrl);
        }
    }
}
