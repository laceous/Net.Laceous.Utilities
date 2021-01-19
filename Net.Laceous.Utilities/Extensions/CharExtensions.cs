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
        internal static bool IsLeftBrace(this char c)
        {
            return c == '{';
        }

        /// <summary>
        /// Checks if the char is }
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if }, otherwise false</returns>
        internal static bool IsRightBrace(this char c)
        {
            return c == '}';
        }

        /// <summary>
        /// Checks if the char is "
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if ", otherwise false</returns>
        internal static bool IsDoubleQuote(this char c)
        {
            return c == '\"';
        }

        /// <summary>
        /// Checks if the char is '
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if ', otherwise false</returns>
        internal static bool IsSingleQuote(this char c)
        {
            return c == '\'';
        }

        /// <summary>
        /// Checks if the char is \
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if \, otherwise false</returns>
        internal static bool IsBackslash(this char c)
        {
            return c == '\\';
        }

        /// <summary>
        /// Checks if the char is `
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if `, otherwise false</returns>
        internal static bool IsBacktick(this char c)
        {
            return c == '`';
        }

        /// <summary>
        /// Checks if the char is \r
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if \r, otherwise false</returns>
        internal static bool IsCarriageReturn(this char c)
        {
            return c == '\r';
        }

        /// <summary>
        /// Checks if the char is \n
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if \n, otherwise false</returns>
        internal static bool IsLineFeed(this char c)
        {
            return c == '\n';
        }

        /// <summary>
        /// Checks if the char is NEL
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if NEL, otherwise false</returns>
        internal static bool IsNextLine(this char c)
        {
            return c == '\x85';
        }

        /// <summary>
        /// Checks if the char is LS
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if LS, otherwise false</returns>
        internal static bool IsLineSeparator(this char c)
        {
            return c == '\u2028';
        }

        /// <summary>
        /// Checks if the char is PS
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if PS, otherwise false</returns>
        internal static bool IsParagraphSeparator(this char c)
        {
            return c == '\u2029';
        }

        /// <summary>
        /// Checks if the char is $
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if $, otherwise false</returns>
        internal static bool IsDollarSign(this char c)
        {
            return c == '$';
        }

        /// <summary>
        /// Checks if the char is _
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if _, otherwise false</returns>
        internal static bool IsUnderscore(this char c)
        {
            return c == '_';
        }

        /// <summary>
        /// Checks if the char is ?
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if ?, otherwise false</returns>
        internal static bool IsQuestionMark(this char c)
        {
            return c == '?';
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns></returns>
        internal static bool IsCaret(this char c)
        {
            return c == '^';
        }

        /// <summary>
        /// Checks if the char is (
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if (, otherwise false</returns>
        internal static bool IsLeftParenthesis(this char c)
        {
            return c == '(';
        }

        /// <summary>
        /// Checks if the char is 1 of 4 double-quote types that PowerShell allows
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if 1 of 4 double-quote types, otherwise false</returns>
        internal static bool IsPowerShellDoubleQuote(this char c)
        {
            // found by testing...
            // U+0022 Quotation Mark
            // U+201E Double Low-9 Quotation Mark
            // U+201C Left Double Quotation Mark
            // U+201D Right Double Quotation Mark
            return c == '\"' || c == '\u201E' || c == '\u201C' || c == '\u201D';
        }

        /// <summary>
        /// Checks if the char is an ascii print char (including space)
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if in the range of 32 to 126, otherwise false</returns>
        internal static bool IsPrintAscii(this char c)
        {
            return c >= 32 && c <= 126; // space - tilde (~)
        }
    }
}
