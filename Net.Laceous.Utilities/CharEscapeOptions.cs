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
        /// 
        /// </summary>
        public CharEscapeLetter EscapeSurrogatePairLetter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CharEscapeLetter EscapeSurrogatePairLetterFallback { get; set; }

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
        /// <param name="escapeLetterFallback">Choose which fallback unicode escape letter to use</param>
        /// <param name="escapeSurrogatePairLetter"></param>
        /// <param name="escapeSurrogatePairLetterFallback"></param>
        /// <param name="useLowerCaseHex">Use lower case hex instead of upper case hex</param>
        /// <param name="useShortEscape">Use built-in short sequences instead of \uHHHH</param>
        public CharEscapeOptions(CharEscapeLanguage escapeLanguage = CharEscapeLanguage.CSharp, CharEscapeLetter escapeLetter = CharEscapeLetter.LowerCaseU4,
            CharEscapeLetter escapeLetterFallback = CharEscapeLetter.LowerCaseU4, CharEscapeLetter escapeSurrogatePairLetter = CharEscapeLetter.UpperCaseU8, 
            CharEscapeLetter escapeSurrogatePairLetterFallback = CharEscapeLetter.UpperCaseU8, bool useLowerCaseHex = false, bool useShortEscape = false)
        {
            EscapeLanguage = escapeLanguage;
            EscapeLetter = escapeLetter;
            EscapeLetterFallback = escapeLetterFallback;
            EscapeSurrogatePairLetter = escapeSurrogatePairLetter;
            EscapeSurrogatePairLetterFallback = escapeSurrogatePairLetterFallback;
            UseLowerCaseHex = useLowerCaseHex;
            UseShortEscape = useShortEscape;
        }
    }
}
