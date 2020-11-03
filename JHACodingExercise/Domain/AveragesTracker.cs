using System;

namespace JHACodingExercise.Domain
{
    /// <summary>
    /// Class to track time-based averages
    /// </summary>
    public class AveragesTracker
    {
        private DateTime _startTime = DateTime.MinValue;

        /// <summary>
        /// Total number of tweets received  
        /// </summary>
        public int TotalTweets { get; private set; } = 0;

        /// <summary>
        /// Average number of tweets received per hour
        /// </summary>
        public double AveragePerHour { get; private set; } = 0;

        /// <summary>
        /// Average number of tweets received per minute
        /// </summary>
        public double AveragePerMinute { get; private set; } = 0;

        /// <summary>
        /// Average number of tweets received per second
        /// </summary>
        public double AveragePerSecond { get; private set; } = 0;

        /// <summary>
        /// A tweet has been received, increase total tweets by 1 and recompute averages based on current time
        /// </summary>
        public void Recompute()
        {
            if (_startTime == DateTime.MinValue)
                _startTime = DateTime.Now;

            var currentTime = DateTime.Now;

            TotalTweets++;

            // compute averages
            // timespan should never be exactly 0 (at least a millisecond or two), but protect against divide by zero just in case
            var timeSpan = currentTime - _startTime;
            AveragePerSecond = timeSpan.TotalSeconds > 0 ? TotalTweets / timeSpan.TotalSeconds : TotalTweets;
            AveragePerMinute = timeSpan.TotalMinutes > 0 ? TotalTweets / timeSpan.TotalMinutes : TotalTweets;
            AveragePerHour = timeSpan.TotalHours > 0 ? TotalTweets / timeSpan.TotalHours : TotalTweets;
        }
    }
}
