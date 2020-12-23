namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Options that control how to escape chars
    /// </summary>
    public class CharEscapeOptions
    {
        /// <summary>
        /// C# or F#
        /// </summary>
        public EscapeLanguage EscapeLanguage { get; set; }

        /// <summary>
        /// Choose which unicode escape letter to use
        /// </summary>
        public EscapeLetter EscapeLetter { get; set; }

        /// <summary>
        /// Use lower case hex instead of upper case hex
        /// </summary>
        public bool UseLowerCaseHex { get; set; }

        /// <summary>
        /// Use \', \", \\, \0, \a, \b, \f, \n, \r, \t, \v instead of \uHHHH
        /// </summary>
        public bool UseShortEscape { get; set; }

        /// <summary>
        /// Initialize new instance of CharEscapeOptions with selected options
        /// </summary>
        /// <param name="escapeLanguage">C# or F#</param>
        /// <param name="escapeLetter">Choose which unicode escape letter to use</param>
        /// <param name="useLowerCaseHex">Use lower case hex instead of upper case hex</param>
        /// <param name="useShortEscape">Use built-in short sequences instead of \uHHHH</param>
        public CharEscapeOptions(EscapeLanguage escapeLanguage = EscapeLanguage.CSharp, EscapeLetter escapeLetter = EscapeLetter.LowerCaseU4, bool useLowerCaseHex = false, bool useShortEscape = false)
        {
            EscapeLanguage = escapeLanguage;
            EscapeLetter = escapeLetter;
            UseLowerCaseHex = useLowerCaseHex;
            UseShortEscape = useShortEscape;
        }
    }
}
