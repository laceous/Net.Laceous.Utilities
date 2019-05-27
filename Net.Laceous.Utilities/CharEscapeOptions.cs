namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Options that control how to escape chars
    /// </summary>
    public class CharEscapeOptions
    {
        /// <summary>
        /// Choose which char types to escape
        /// </summary>
        public CharEscapeType EscapeType { get; set; }

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
        /// Initialize new instance of CharEscapeOptions with default options
        /// </summary>
        public CharEscapeOptions()
        {
            EscapeType = CharEscapeType.Default;
            EscapeLetter = CharEscapeLetter.LowerCaseU;
            AlwaysUseUnicodeEscape = false;
            UseLowerCaseHex = false;
        }

        /// <summary>
        /// Initialize new instance of CharEscapeOptions with selected options
        /// </summary>
        /// <param name="escapeType">Choose which char types to escape</param>
        /// <param name="escapeLetter">Choose which unicode escape letter to use</param>
        /// <param name="alwaysUseUnicodeEscape">Always use unicode escape sequence</param>
        /// <param name="useLowerCaseHex">Use lower case hex instead of upper case hex</param>
        public CharEscapeOptions(CharEscapeType escapeType, CharEscapeLetter escapeLetter, bool alwaysUseUnicodeEscape, bool useLowerCaseHex)
        {
            EscapeType = escapeType;
            EscapeLetter = escapeLetter;
            AlwaysUseUnicodeEscape = alwaysUseUnicodeEscape;
            UseLowerCaseHex = useLowerCaseHex;
        }
    }
}
