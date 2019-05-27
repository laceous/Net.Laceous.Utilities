namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Which unicode escape letter to use
    /// </summary>
    public enum CharEscapeLetter
    {
        /// <summary>
        /// \\unnnn
        /// </summary>
        LowerCaseU,

        /// <summary>
        /// \\Unnnnnnnn
        /// </summary>
        UpperCaseU,

        /// <summary>
        /// \\xnnnn
        /// </summary>
        LowerCaseXFixedLength,

        /// <summary>
        /// \\xn or \\xnn or \\xnnn or \\xnnnn
        /// </summary>
        LowerCaseXVariableLength
    }
}
