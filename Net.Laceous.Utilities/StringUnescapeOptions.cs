namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Options that control how to unescape strings
    /// </summary>
    public class StringUnescapeOptions
    {
        /// <summary>
        /// Treat unrecognized escape sequences as verbatim if it would otherwise throw an exception
        /// </summary>
        public bool IsUnrecognizedEscapeVerbatim { get; set; }

        /// <summary>
        /// Initialize new instance of StringUnescapeOptions with selected options
        /// </summary>
        /// <param name="isUnrecognizedEscapeVerbatim">Treat unrecognized escape sequences as verbatim if it would otherwise throw an exception</param>
        public StringUnescapeOptions(bool isUnrecognizedEscapeVerbatim = false)
        {
            IsUnrecognizedEscapeVerbatim = isUnrecognizedEscapeVerbatim;
        }
    }
}
