using Newtonsoft.Json;
using System.Linq;

namespace JHACodingExercise.Domain
{
    /// <summary>
    /// Class to gather and compute twitter statistics (meant to be injected as a singleton and accessed across threads)
    /// </summary>
    public class TwitterStatistics
    {
        private readonly object padlock = new object();
        private readonly EmojiHelper _emojiHelper;

        /// <summary>
        /// Public constructor with DI
        /// </summary>
        /// <param name="averagesTracker">Track overall averages</param>
        /// <param name="hashtagTracker">Track hashtags</param>
        /// <param name="emojiTracker">Track emojis</param>
        /// <param name="urlTracker">Track urls</param>
        /// <param name="emojiHelper">Help with emojis</param>
        public TwitterStatistics(AveragesTracker averagesTracker, TopPropertyTracker hashtagTracker, TopPropertyWithAverageTracker emojiTracker, UrlTracker urlTracker, EmojiHelper emojiHelper)
        {
            Averages = averagesTracker;
            Hashtags = hashtagTracker;
            Emojis = emojiTracker;
            Urls = urlTracker;

            _emojiHelper = emojiHelper;
        }

        /// <summary>
        /// Overall average statistics for tweets
        /// </summary>
        public AveragesTracker Averages { get; private set; }

        /// <summary>
        /// Emoji statistics
        /// </summary>
        public TopPropertyWithAverageTracker Emojis { get; private set; }

        /// <summary>
        /// Hashtag statistics
        /// </summary>
        public TopPropertyTracker Hashtags { get; private set; }

        /// <summary>
        /// Url statistics
        /// </summary>
        public UrlTracker Urls { get; set; }

        /// <summary>
        /// Update statistics with a new tweet that has been received from the Twitter api
        /// </summary>
        /// <param name="jsonString"></param>
        public void ProcessTweet(string jsonString)
        {
            var tweet = JsonConvert.DeserializeObject<Tweet>(jsonString);

            // not sure why this is happening, but receiving empty tweets from the API occasionally - throw them away
            if (tweet == null)
                return;

            // this is compute intensive, perform outside of the lock
            var foundEmojis = _emojiHelper.FindEmojis(tweet.data.text);

            // update statistics with information about this tweet - lock to be threadsafe
            lock (padlock)
            {
                Averages.Recompute();

                Emojis.AddProperties(foundEmojis);

                Hashtags.AddProperties(tweet.data.entities?.hashtags?.Select(x => x.tag));

                Urls.AddProperties(tweet.data.entities?.urls);
            }
        }
    }
}
