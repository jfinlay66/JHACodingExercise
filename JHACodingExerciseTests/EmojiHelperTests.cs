using JHACodingExercise.Domain;
using Xunit;

namespace JHACodingExerciseTests
{
    public class EmojiHelperTests
    {
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData("The quick brown fox jumped over the lazy dog.", 0)]
        [InlineData("I want a 🍩", 1)]
        [InlineData("I want many 🍩🍩🍩", 1)]
        [InlineData("I want a 🍰 or 🍩 under the 🌘", 3)]
        public void EmojiHelper_FindEmojis_Success(string tweet, int expectedEmojiCount)
        {
            // arrange
            var emojiHelper = new EmojiHelper();

            // act
            var results = emojiHelper.FindEmojis(tweet);

            // assert
            Assert.Equal(expectedEmojiCount, results.Count);
        }
    }
}
