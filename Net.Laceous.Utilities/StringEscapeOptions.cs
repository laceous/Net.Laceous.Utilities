namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Options that control how to escape strings
    /// </summary>
    public class StringEscapeOptions
    {
        /// <summary>
        /// Choose which char types in the string to escape
        /// </summary>
        public StringEscapeType EscapeType { get; set; }

        /// <summary>
        /// Escape surrogate pairs together with \U instead of as two separate chars
        /// </summary>
        public bool EscapeSurrogatePairs { get; set; }

        /// <summary>
        /// Initialize new instance of StringEscapeOptions with selected options
        /// </summary>
        /// <param name="escapeType">Choose which char types in the string to escape</param>
        /// <param name="escapeSurrogatePairs">Escape surrogate pairs together with \U instead of as two separate chars</param>
        public StringEscapeOptions(StringEscapeType escapeType = StringEscapeType.EscapeAll, bool escapeSurrogatePairs = false)
        {
            EscapeType = escapeType;
            EscapeSurrogatePairs = escapeSurrogatePairs;
        }
    }
}
