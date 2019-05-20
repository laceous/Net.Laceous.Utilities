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
        public bool AlwaysUseUnicodeEscapeSequence { get; set; }

        /// <summary>
        /// Use lower case hex instead of upper case hex
        /// </summary>
        public bool UseLowerCaseHexInsteadOfUpperCaseHex { get; set; }

        /// <summary>
        /// Use \xnnnn instead of \unnnn
        /// </summary>
        public bool UseLowerCaseXInsteadOfLowerCaseU { get; set; }

        /// <summary>
        /// Initialize new instance of CharEscapeOptions with default options
        /// </summary>
        public CharEscapeOptions()
        {
            EscapeType = CharEscapeType.Default;
            AlwaysUseUnicodeEscapeSequence = false;
            UseLowerCaseHexInsteadOfUpperCaseHex = false;
            UseLowerCaseXInsteadOfLowerCaseU = false;
        }

        public CharEscapeOptions(CharEscapeType escapeType, bool alwaysUseUnicodeEscapeSequence, bool useLowerCaseHexInsteadOfUpperCaseHex, bool useLowerCaseXInsteadOfLowerCaseU)
        {
            EscapeType = escapeType;
            AlwaysUseUnicodeEscapeSequence = alwaysUseUnicodeEscapeSequence;
            UseLowerCaseHexInsteadOfUpperCaseHex = useLowerCaseHexInsteadOfUpperCaseHex;
            UseLowerCaseXInsteadOfLowerCaseU = useLowerCaseXInsteadOfLowerCaseU;
        }
    }
}
