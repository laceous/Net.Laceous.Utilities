namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Which chars to escape in the string
    /// </summary>
    public enum StringEscapeKind
    {
        /// <summary>
        /// Escape all chars
        /// </summary>
        EscapeAll,

        /// <summary>
        /// Escape all chars except for ascii print chars (which includes space)
        /// Escape char + relevant quote chars will still be escaped
        /// </summary>
        EscapeNonAscii
    }
}
