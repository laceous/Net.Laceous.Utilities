namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Options that control how to unescape strings
    /// </summary>
    public class StringUnescapeOptions
    {
        /// <summary>
        /// C# or F#
        /// </summary>
        public EscapeLanguage EscapeLanguage { get; set; }

        /// <summary>
        /// Treat unrecognized escape sequences as verbatim, otherwise throw an exception
        /// </summary>
        public bool IsUnrecognizedEscapeVerbatim { get; set; }

        /// <summary>
        /// Initialize new instance of StringUnescapeOptions with selected options
        /// </summary>
        /// <param name="escapeLanguage">C# or F#</param>
        /// <param name="isUnrecognizedEscapeVerbatim">Treat unrecognized escape sequences as verbatim, otherwise throw an exception</param>
        public StringUnescapeOptions(EscapeLanguage escapeLanguage = EscapeLanguage.CSharp, bool? isUnrecognizedEscapeVerbatim = null)
        {
            EscapeLanguage = escapeLanguage;
            if (isUnrecognizedEscapeVerbatim.HasValue)
            {
                IsUnrecognizedEscapeVerbatim = isUnrecognizedEscapeVerbatim.Value;
            }
            else
            {
                // C# generally complains about unrecognized escape sequences while F# is generally pretty lenient
                switch (escapeLanguage)
                {
                    case EscapeLanguage.FSharp:
                        IsUnrecognizedEscapeVerbatim = true;
                        break;
                    default: // EscapeLanguage.CSharp
                        IsUnrecognizedEscapeVerbatim = false;
                        break;
                }
            }
        }
    }
}
