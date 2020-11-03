using System.Collections.Generic;
using System.Linq;

namespace JHACodingExercise.Domain
{
    /// <summary>
    /// Class to track property names encountered, and how many times
    /// This class is used for Hashtags, as well as a base class for other property types that need expanded tracking
    /// </summary>
    public class TopPropertyTracker
    {
        private const int DISPLAY_TOP_COUNT = 10;

        protected string _propertyName = "Hashtag";
        private Dictionary<string, int> _allProperties = new Dictionary<string, int>();

        /// <summary>
        /// Get the top properties encountered, along with their counts, at this point in time
        /// </summary>
        public IEnumerable<string> TopProperties { get { return _allProperties.OrderByDescending(x => x.Value).Take(DISPLAY_TOP_COUNT).Select(x => $"{_propertyName}:{x.Key} Count:{x.Value}"); } }

        /// <summary>
        /// A list of properties has been encountered, add to list or update count of times seen
        /// </summary>
        /// <param name="propertyNames">List of property names</param>
        public void AddProperties(IEnumerable<string> propertyNames)
        {
            if (propertyNames == null)
                return;

            foreach (var name in propertyNames)
            {
                if (_allProperties.ContainsKey(name))
                {
                    _allProperties[name]++;
                }
                else
                {
                    _allProperties.Add(name, 1);
                }
            }
        }
    }
}
