namespace Net.Laceous.Utilities
{
    /// <summary>
    /// String quote types
    /// </summary>
    public enum StringQuoteKind
    {
        /// <summary>
        /// "string" (C#, F#, PowerShell, Python)
        /// </summary>
        DoubleQuote,

        /// <summary>
        /// 'string' (Python)
        /// </summary>
        SingleQuote,

        /// <summary>
        /// """string""" (Python)
        /// </summary>
        TripleDoubleQuote,

        /// <summary>
        /// '''string''' (Python)
        /// </summary>
        TripleSingleQuote
    }
}
