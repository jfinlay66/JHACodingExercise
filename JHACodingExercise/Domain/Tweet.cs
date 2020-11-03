namespace JHACodingExercise.Domain
{
    /// <summary>
    /// Classes to use to deserialize the tweets information sent by the Twitter api into strongly typed objects
    /// (forgive the multiple classes in one file - I didn't want to muddy up the exercise solution with these tiny classes separated out)
    /// </summary>
    public class Tweet
    {
        /// <summary>
        /// Tweet data
        /// </summary>
        public Data data { get; set; }
    }

    /// <summary>
    /// Class containing tweet data
    /// </summary>
    public class Data
    { 
        /// <summary>
        /// Tweet text
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Entities associated with the tweet
        /// </summary>
        public Entities entities { get; set; }
    }

    /// <summary>
    /// Class containing the tweet entities
    /// </summary>
    public class Entities
    {
        /// <summary>
        /// Array of hashtags
        /// </summary>
        public HashTag[] hashtags { get; set; }

        /// <summary>
        /// Array of urls
        /// </summary>
        public URL[] urls { get; set; }
    }

    /// <summary>
    /// Class containing hashtag information
    /// </summary>
    public class HashTag
    {
        /// <summary>
        /// Tag
        /// </summary>
        public string tag { get; set; }
    }

    /// <summary>
    /// Class containing url information
    /// </summary>
    public class URL
    {
        /// <summary>
        /// Display url
        /// </summary>
        public string display_url { get; set; }
    }
}
