using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace JHACodingExercise.Domain
{
    /// <summary>
    /// Class to track a property, along with the photo domain percentage
    /// </summary>
    public class UrlTracker : TopPropertyWithAverageTracker
    {
        private IEnumerable<string> _photoDomains = new List<string>() { "pic.twitter.com", "instagram.com" };
        private int _tweetsWithPhotoUrl = 0;

        /// <summary>
        /// Compute percentage of tweets encountered with photo domains
        /// </summary>
        public double PercentWithPhotoUrl { get { return _totalTweets != 0 ? ((double)_tweetsWithPhotoUrl / (double)_totalTweets) * 100 : 0; } }

        /// <summary>
        /// Public constructor - override name of property printed in strings
        /// </summary>
        public UrlTracker()
        {
            _propertyName = "Domain";
        }

        /// <summary>
        /// Update counts and call base to add/update the property name counts
        /// </summary>
        /// <param name="urls">List of urls encountered, if any</param>
        public void AddProperties(IEnumerable<URL> urls)
        {
            var domains = new List<string>();

            // strip off beginning domain name from the url display name
            if (urls != null)
            {
                domains = urls.Select(x => { var tokens = x.display_url.Split('/'); return tokens[0]; }).ToList();
            }

            if (domains.Any(x => _photoDomains.Contains(x, StringComparer.OrdinalIgnoreCase)))
            {
                _tweetsWithPhotoUrl++;                                                                               
            }

            base.AddProperties(domains);
        }
    }
}
