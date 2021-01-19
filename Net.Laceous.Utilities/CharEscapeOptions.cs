namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Options that control how to escape chars
    /// </summary>
    public class CharEscapeOptions
    {
        /// <summary>
        /// C# / F# / PowerShell
        /// </summary>
        public CharEscapeLanguage EscapeLanguage { get; set; }

        /// <summary>
        /// Choose which unicode escape letter to use
        /// </summary>
        public CharEscapeLetter EscapeLetter { get; set; }

        /// <summary>
        /// Choose which fallback unicode escape letter to use
        /// </summary>
        public CharEscapeLetter EscapeLetterFallback { get; set; }

        /// <summary>
        /// Choose which surrogate pair escape letter to use
        /// </summary>
        public CharEscapeLetter SurrogatePairEscapeLetter { get; set; }

        /// <summary>
        /// Use lower case hex instead of upper case hex
        /// </summary>
        public bool UseLowerCaseHex { get; set; }

        /// <summary>
        /// Use defined short escape (e.g. \n, \r, \t) instead of \uHHHH
        /// </summary>
        public bool UseShortEscape { get; set; }

        /// <summary>
        /// Initialize new instance of CharEscapeOptions with selected options
        /// </summary>
        /// <param name="escapeLanguage">C# / F# / PowerShell</param>
        /// <param name="escapeLetter">Choose which unicode escape letter to use</param>
        /// <param name="escapeLetterFallback">Choose which fallback unicode escape letter to use</param>
        /// <param name="surrogatePairEscapeLetter">Choose which surrogate pair escape letter to use</param>
        /// <param name="useLowerCaseHex">Use lower case hex instead of upper case hex</param>
        /// <param name="useShortEscape">Use built-in short sequences instead of \uHHHH</param>
        public CharEscapeOptions(CharEscapeLanguage escapeLanguage = CharEscapeLanguage.CSharp, CharEscapeLetter escapeLetter = CharEscapeLetter.LowerCaseU4,
            CharEscapeLetter escapeLetterFallback = CharEscapeLetter.LowerCaseU4, CharEscapeLetter surrogatePairEscapeLetter = CharEscapeLetter.UpperCaseU8,
            bool useLowerCaseHex = false, bool useShortEscape = false)
        {
            EscapeLanguage = escapeLanguage;
            EscapeLetter = escapeLetter;
            EscapeLetterFallback = escapeLetterFallback;
            SurrogatePairEscapeLetter = surrogatePairEscapeLetter;
            UseLowerCaseHex = useLowerCaseHex;
            UseShortEscape = useShortEscape;
        }
    }
}
