namespace JHACodingExercise.Domain
{
    /// <summary>
    /// Class to use to deserialize the emoji.json file into strongly typed objects
    /// </summary>
    public class Emoji
    {
        /// <summary>
        /// Name of the emoji
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Unified codepoints of the emoji
        /// </summary>
        public string unified { get; set; }

        /// <summary>
        /// Computed string from the unified codepoints
        /// </summary>
        public string UnicodeString { get; set; }
    }
}
