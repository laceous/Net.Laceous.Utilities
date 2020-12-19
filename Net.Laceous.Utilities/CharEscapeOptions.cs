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
        /// Use \unnnn instead of \", \', \", \\, \0, \a, \b, \f, \n, \r, \t, \v
        /// </summary>
        public bool AlwaysUseUnicodeEscape { get; set; }

        /// <summary>
        /// Use lower case hex instead of upper case hex
        /// </summary>
        public bool UseLowerCaseHex { get; set; }

        /// <summary>
        /// Initialize new instance of CharEscapeOptions with selected options
        /// </summary>
        /// <param name="escapeLetter">Choose which unicode escape letter to use</param>
        /// <param name="alwaysUseUnicodeEscape">Always use unicode escape sequence</param>
        /// <param name="useLowerCaseHex">Use lower case hex instead of upper case hex</param>
        public CharEscapeOptions(CharEscapeLetter escapeLetter = CharEscapeLetter.LowerCaseU, bool alwaysUseUnicodeEscape = true, bool useLowerCaseHex = false)
        {
            EscapeLetter = escapeLetter;
            AlwaysUseUnicodeEscape = alwaysUseUnicodeEscape;
            UseLowerCaseHex = useLowerCaseHex;
        }
    }
}
