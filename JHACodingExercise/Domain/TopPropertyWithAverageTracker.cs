using System.Collections.Generic;
using System.Linq;

namespace JHACodingExercise.Domain
{
    /// <summary>
    /// Class to track a property, along with the percentage
    /// </summary>
    public class TopPropertyWithAverageTracker : TopPropertyTracker
    {
        private int _tweetsWithProperty = 0;
        protected int _totalTweets = 0;

        /// <summary>
        /// Compute percentage of tweets encountered with this property
        /// </summary>
        public double PercentWithProperty { get { return _totalTweets != 0 ? ((double)_tweetsWithProperty / (double)_totalTweets) * 100 : 0; } }

        /// <summary>
        /// Public constructor - override name of property printed in strings
        /// </summary>
        public TopPropertyWithAverageTracker()
        {
            _propertyName = "Emoji";
        }

        /// <summary>
        /// Update counts and call base to add/update the property name counts
        /// </summary>
        /// <param name="propertyNames">List of property names encountered, if any</param>
        public new void AddProperties(IEnumerable<string> propertyNames)
        {
            _totalTweets++;

            if (propertyNames.Any())
            {
                _tweetsWithProperty++;
            }

            base.AddProperties(propertyNames);
        }
    }
}
