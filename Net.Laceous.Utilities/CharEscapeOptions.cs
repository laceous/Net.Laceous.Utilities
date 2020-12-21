namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Options that control how to escape chars
    /// </summary>
    public class CharEscapeOptions
    {
        /// <summary>
        /// Choose which unicode escape letter to use
        /// </summary>
        public CharEscapeLetter EscapeLetter { get; set; }

        /// <summary>
        /// Use lower case hex instead of upper case hex
        /// </summary>
        public bool UseLowerCaseHex { get; set; }

        /// <summary>
        /// Use \", \', \", \\, \0, \a, \b, \f, \n, \r, \t, \v instead of \unnnn
        /// </summary>
        public bool UseShortEscape { get; set; }

        /// <summary>
        /// Initialize new instance of CharEscapeOptions with selected options
        /// </summary>
        /// <param name="escapeLetter">Choose which unicode escape letter to use</param>
        /// <param name="alwaysUseUnicodeEscape">Always use unicode escape sequence</param>
        /// <param name="useLowerCaseHex">Use lower case hex instead of upper case hex</param>
        public CharEscapeOptions(CharEscapeLetter escapeLetter = CharEscapeLetter.LowerCaseU, bool useLowerCaseHex = false, bool useShortEscape = false)
        {
            EscapeLetter = escapeLetter;
            UseLowerCaseHex = useLowerCaseHex;
            UseShortEscape = UseShortEscape;
        }
    }
}
