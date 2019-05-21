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
        /// Use \unnnn or \xnnnn instead of \", \', \", \\, \0, \a, \b, \f, \n, \r, \t, \v
        /// </summary>
        public bool AlwaysUseUnicodeEscape { get; set; }

        /// <summary>
        /// Use \xnnnn instead of \unnnn
        /// </summary>
        public bool UseLowerCaseX { get; set; }

        /// <summary>
        /// Use upper case hex instead of lower case hex
        /// </summary>
        public bool UseUpperCaseHex { get; set; }

        /// <summary>
        /// Initialize new instance of CharEscapeOptions with default options
        /// </summary>
        public CharEscapeOptions()
        {
            EscapeType = CharEscapeType.Default;
            AlwaysUseUnicodeEscape = false;
            UseLowerCaseX = false;
            UseUpperCaseHex = false;
        }

        /// <summary>
        /// Initialize new instance of CharEscapeOptions with selected options
        /// </summary>
        /// <param name="escapeType">Escape type</param>
        /// <param name="alwaysUseUnicodeEscape">Always use unicode escape sequence</param>
        /// <param name="useLowerCaseX">Use lower case x instead of lower case u</param>
        /// <param name="useUpperCaseHex">Use upper case hex instead of lower case hex</param>
        public CharEscapeOptions(CharEscapeType escapeType, bool alwaysUseUnicodeEscape, bool useLowerCaseX, bool useUpperCaseHex)
        {
            EscapeType = escapeType;
            AlwaysUseUnicodeEscape = alwaysUseUnicodeEscape;
            UseLowerCaseX = useLowerCaseX;
            UseUpperCaseHex = useUpperCaseHex;
        }
    }
}
