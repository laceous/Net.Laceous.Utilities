namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Options that control how to unescape chars
    /// </summary>
    public class CharUnescapeOptions
    {
        /// <summary>
        /// C# / F# / PowerShell
        /// </summary>
        public CharEscapeLanguage EscapeLanguage { get; set; }

        /// <summary>
        /// Treat unrecognized escape sequences as verbatim if it would otherwise throw an exception
        /// </summary>
        public bool IsUnrecognizedEscapeVerbatim { get; set; }

        /// <summary>
        /// Initialize new instance of CharUnescapeOptions with selected options
        /// </summary>
        /// <param name="escapeLanguage">C# / F# / PowerShell</param>
        /// <param name="isUnrecognizedEscapeVerbatim">Treat unrecognized escape sequences as verbatim if it would otherwise throw an exception</param>
        public CharUnescapeOptions(CharEscapeLanguage escapeLanguage = CharEscapeLanguage.CSharp, bool isUnrecognizedEscapeVerbatim = false)
        {
            EscapeLanguage = escapeLanguage;
            IsUnrecognizedEscapeVerbatim = isUnrecognizedEscapeVerbatim;
        }
    }
}
