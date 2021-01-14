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
        /// Remove quotes before parsing
        /// </summary>
        public bool RemoveQuotes { get; set; }

        /// <summary>
        /// What type of quotes were encasing s: "s", 's', """s""", '''s'''
        /// Helps find illegal sequences
        /// </summary>
        public StringQuoteKind QuoteKind { get; set; }

        /// <summary>
        /// Initialize new instance of StringUnescapeOptions with selected options
        /// </summary>
        /// <param name="isUnrecognizedEscapeVerbatim">Treat unrecognized escape sequences as verbatim if it would otherwise throw an exception</param>
        /// <param name="removeQuotes">Remove quotes before parsing</param>
        /// <param name="quoteKind">What type of quotes were encasing the string</param>
        public StringUnescapeOptions(bool isUnrecognizedEscapeVerbatim = false, bool removeQuotes = false, StringQuoteKind quoteKind = StringQuoteKind.DoubleQuote)
        {
            IsUnrecognizedEscapeVerbatim = isUnrecognizedEscapeVerbatim;
            RemoveQuotes = removeQuotes;
            QuoteKind = quoteKind;
        }
    }
}
