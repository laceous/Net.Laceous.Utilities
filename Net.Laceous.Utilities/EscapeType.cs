namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Which chars to escape in the string
    /// </summary>
    public enum EscapeType
    {
        /// <summary>
        /// Escape all chars
        /// </summary>
        EscapeAll,

        /// <summary>
        /// Escape all chars except for ascii print chars (which includes space)
        /// </summary>
        EscapeNonAscii
    }
}
