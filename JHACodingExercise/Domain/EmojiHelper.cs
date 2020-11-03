using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JHACodingExercise.Domain
{
    /// <summary>
    /// Helper class to find emojis in a string/tweet
    /// </summary>
    public class EmojiHelper
    {
        private const string JSON_FILE = "Resources/emoji.json";

        private IEnumerable<Emoji> _emojis = new List<Emoji>();

        /// <summary>
        /// Public constructor - loads the defined emojis from the json file
        /// </summary>
        public EmojiHelper()
        {
            // read in json file definitions and deserialize to strongly typed class
            var emojis = new List<Emoji>();
            using (StreamReader r = new StreamReader(JSON_FILE))
            {
                var jsonString = r.ReadToEnd();
                emojis = JsonConvert.DeserializeObject<List<Emoji>>(jsonString);
            }

            // probably overthinking this - got lost down a google rabbit hole
            // I'm not sure I understand what is contained in the unified entry here, especially when '-' characters are present
            // I suspect this has to do with high and low surrogates, but this field often contains 3 or more hex digits
            // To get this working for now, just looking at the single hex digit entries, which is about 2/3 of the emoji definitions
            var filteredList = emojis.Where(x => !x.unified.Contains('-'));

            _emojis = filteredList.Select(x =>
            {
                int code = int.Parse(x.unified, System.Globalization.NumberStyles.HexNumber);
                string unicodeString = char.ConvertFromUtf32(code);
                return new Emoji() { name = x.name, unified = x.unified, UnicodeString = unicodeString };
            });
        }

        /// <summary>
        /// Given a string, find a list of all emojis found within the string
        /// Not happy with this implementation - stepping through the emoji definitions and looking for each one in the string
        /// Would like to find candidate characters in the string and look for them in the list instead, but couldn't determine a way to locate candidates in the given string
        /// </summary>
        /// <param name="tweet">string to evaluate</param>
        /// <returns>List of emoji names found, empty list if none</returns>
        public List<string> FindEmojis(string tweet)
        {
            List<string> foundEmojis = new List<string>();

            if (string.IsNullOrEmpty(tweet))
                return foundEmojis;

            foundEmojis = _emojis.Where(x => tweet.Contains(x.UnicodeString)).Select(x => x.name).ToList();

            return foundEmojis;
        }
    }
}
