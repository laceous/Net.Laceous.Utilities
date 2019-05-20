namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Which char types to escape
    /// </summary>
    public enum CharEscapeType
    {
        /// <summary>
        /// Escape control and white space chars (except for space)
        /// </summary>
        Default,

        /// <summary>
        /// Escape all chars
        /// </summary>
        EscapeAllChars,

        /// <summary>
        /// Escape all chars except for ascii print chars and space
        /// </summary>
        EscapeAllCharsExceptAscii
    }
}
