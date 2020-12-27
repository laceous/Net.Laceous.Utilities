namespace Net.Laceous.Utilities.Extensions
{
    /// <summary>
    /// Adds extension methods to chars
    /// </summary>
    internal static class CharExtensions
    {
        /// <summary>
        /// Checks if the char is a hex char (0-9, A-F, a-f)
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if hex, false if not hex</returns>
        internal static bool IsHex(this char c)
        {
            return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');
        }

        /// <summary>
        /// Checks if the char is a decimal digit (0-9)
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if decimal digit, otherwise false</returns>
        internal static bool IsDecimal(this char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// Checks if the char is zero
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if zero, false if not zero</returns>
        internal static bool IsZero(this char c)
        {
            return c == '0';
        }

        /// <summary>
        /// Checks if the char is {
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if {, otherwise false</returns>
        internal static bool IsOpeningCurlyBrace(this char c)
        {
            return c == '{';
        }

        /// <summary>
        /// Checks if the char is }
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if }, otherwise false</returns>
        internal static bool IsClosingCurlyBrace(this char c)
        {
            return c == '}';
        }

        /// <summary>
        /// Checks if the char is an ascii print char (including space)
        /// Exclude quotes and escape char
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if in the range of 32 to 126 (and quotable), otherwise false</returns>
        internal static bool IsQuotableAscii(this char c, CharEscapeLanguage escapeLanguage)
        {
            char escape = escapeLanguage == CharEscapeLanguage.PowerShell ? '`' : '\\';
            return c != '\'' && c != '\"' && c != escape && c >= 32 && c <= 126;
        }
    }
}
