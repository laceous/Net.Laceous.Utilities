namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Options that control how to escape strings
    /// </summary>
    public class StringEscapeOptions
    {
        /// <summary>
        /// Choose which char types in the string to escape
        /// </summary>
        public StringEscapeKind EscapeKind { get; set; }

        /// <summary>
        /// Escape surrogate pairs together with \U instead of as two separate chars
        /// </summary>
        public bool EscapeSurrogatePairs { get; set; }

        /// <summary>
        /// Add quotes after escaping
        /// </summary>
        public bool AddQuotes { get; set; }

        /// <summary>
        /// What type of quotes were encasing s: "s", 's', """s""", '''s'''
        /// </summary>
        public StringQuoteKind QuoteKind { get; set; }

        /// <summary>
        /// Initialize new instance of StringEscapeOptions with selected options
        /// </summary>
        /// <param name="escapeKind">Choose which char types in the string to escape</param>
        /// <param name="escapeSurrogatePairs">Escape surrogate pairs together with \U instead of as two separate chars</param>
        /// <param name="quoteKind">What type of quotes to encase the string with</param>
        /// <param name="addQuotes">Add quotes after escaping</param>
        public StringEscapeOptions(StringEscapeKind escapeKind = StringEscapeKind.EscapeAll, bool escapeSurrogatePairs = false, bool addQuotes = false, StringQuoteKind quoteKind = StringQuoteKind.DoubleQuote)
        {
            EscapeKind = escapeKind;
            EscapeSurrogatePairs = escapeSurrogatePairs;
            AddQuotes = addQuotes;
            QuoteKind = quoteKind;
        }
    }
}
