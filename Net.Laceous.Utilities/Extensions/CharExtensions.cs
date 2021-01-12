﻿using System;

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
        /// Checks if the char is an octal digit (0-7)
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if octal digit, otherwise false</returns>
        internal static bool IsOctal(this char c)
        {
            return c >= '0' && c <= '7';
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
        /// Checks if the char is an ascii print char (including space)
        /// Exclude escape char + string quote chars
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <param name="escapeLanguage">Escape language</param>
        /// <returns>True if in the range of 32 to 126, otherwise false</returns>
        internal static bool IsQuotableAscii(this char c, CharEscapeLanguage escapeLanguage)
        {
            char[] chars;
            switch (escapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                case CharEscapeLanguage.FSharp:
                    chars = new char[] { '\\', '\"' };
                    break;
                case CharEscapeLanguage.PowerShell:
                    chars = new char[] { '`', '\"' };
                    break;
                case CharEscapeLanguage.Python:
                    chars = new char[] { '\\', '\"', '\'' };
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", escapeLanguage, nameof(escapeLanguage)), nameof(escapeLanguage));
            }

            return Array.IndexOf(chars, c) == -1 && c >= 32 && c <= 126;
        }
    }
}
