namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Options that control how to unescape chars
    /// </summary>
    public class CharUnescapeOptions
    {
        /// <summary>
        /// C# or F#
        /// </summary>
        public EscapeLanguage EscapeLanguage { get; set; }

        /// <summary>
        /// Initialize new instance of CharUnescapeOptions with selected options
        /// </summary>
        /// <param name="escapeLanguage">C# or F#</param>
        public CharUnescapeOptions(EscapeLanguage escapeLanguage = EscapeLanguage.CSharp)
        {
            EscapeLanguage = escapeLanguage;
        }
    }
}
