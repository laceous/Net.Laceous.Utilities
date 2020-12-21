using System;

namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Static methods related to chars
    /// </summary>
    public static class CharUtils
    {
        /// <summary>
        /// Escape char with backslash sequence (e.g. \n -> \\n)
        /// </summary>
        /// <param name="c">Char to escape</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <returns>String with escape sequence for char</returns>
        public static string Escape(char c, CharEscapeOptions escapeOptions = null)
        {
            if (escapeOptions == null)
            {
                escapeOptions = new CharEscapeOptions();
            }

            string xu;
            string hex;
            switch (escapeOptions.EscapeLetter)
            {
                case CharEscapeLetter.LowerCaseXFixedLength:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
                    break;
                case CharEscapeLetter.LowerCaseXVariableLength:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x" : "X";
                    break;
                case CharEscapeLetter.UpperCaseU:
                    xu = "U";
                    hex = escapeOptions.UseLowerCaseHex ? "x8" : "X8";
                    break;
                default: // CharEscapeLetter.LowerCaseU
                    xu = "u";
                    hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
                    break;
            }

            switch (c)
            {
                case '\'':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\'";
                case '\"':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\\"";
                case '\\':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\\\";
                case '\0':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\0";
                case '\a':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\a";
                case '\b':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\b";
                case '\f':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\f";
                case '\n':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\n";
                case '\r':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\r";
                case '\t':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\t";
                case '\v':
                    return escapeOptions.AlwaysUseUnicodeEscape ? "\\" + xu + ((int)c).ToString(hex) : "\\v";
                default:
                    return "\\" + xu + ((int)c).ToString(hex);
            }
        }

        /// <summary>
        /// Escape surrogate pair with \\Unnnnnnnn
        /// </summary>
        /// <param name="highSurrogate">High surrogate</param>
        /// <param name="lowSurrogate">Low surrogate</param>
        /// <param name="useLowerCaseHex">Use lower case hex instead of upper case hex</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string EscapeSurrogatePair(char highSurrogate, char lowSurrogate, bool useLowerCaseHex = false)
        {
            string hex = useLowerCaseHex ? "x8" : "X8";
            return "\\U" + char.ConvertToUtf32(highSurrogate, lowSurrogate).ToString(hex);
        }

        /// <summary>
        /// Escape surrogate pair with \\Unnnnnnnn
        /// </summary>
        /// <param name="s">String containing the surrogate pair</param>
        /// <param name="useLowerCaseHex">Use lower case hex instead of upper case hex</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string EscapeSurrogatePair(string s, bool useLowerCaseHex = false)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (IsSurrogatePair(s))
            {
                return EscapeSurrogatePair(s[0], s[1], useLowerCaseHex);
            }
            throw new ArgumentException("String did not contain exactly one surrogate pair.", nameof(s));
        }

        /// <summary>
        /// Unescape backslash sequence to char (e.g. \\n -> \n)
        /// </summary>
        /// <param name="s">String containing the escaped char</param>
        /// <returns>Char that's been unescaped</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static char Unescape(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (s.Length == 1)
            {
                return s[0];
            }
            else if (s.Length > 1 && s.Length <= 10) // longest escaped string: \Unnnnnnnn
            {
                string unescaped = StringUtils.Unescape(s, unrecognizedEscapeIsVerbatim: true);
                if (unescaped.Length == 1)
                {
                    return unescaped[0];
                }
            }
            throw new ArgumentException("String did not contain exactly one char (escaped or not).", nameof(s));
        }

        /// <summary>
        /// Unescape backslash sequence to surrogate pair
        /// </summary>
        /// <param name="s">String containing the escaped surrogate pair</param>
        /// <param name="highSurrogate">Return high surrogate</param>
        /// <param name="lowSurrogate">Return low surrogate</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UnescapeSurrogatePair(string s, out char highSurrogate, out char lowSurrogate)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            // longest escaped string: \unnnn\unnnn
            if (s.Length >= 2 && s.Length <= 12)
            {
                string unescaped;
                if (s.Length == 2)
                {
                    unescaped = s;
                }
                else
                {
                    unescaped = StringUtils.Unescape(s, unrecognizedEscapeIsVerbatim: true);
                }

                if (IsSurrogatePair(unescaped))
                {
                    highSurrogate = unescaped[0];
                    lowSurrogate = unescaped[1];
                    return;
                }
            }
            throw new ArgumentException("String did not contain exactly one surrogate pair (escaped or not).", nameof(s));
        }

        /// <summary>
        /// Unescape backslash sequence to surrogate pair
        /// </summary>
        /// <param name="s">String containing the escaped surrogate pair</param>
        /// <returns>String containing the high surrogate + low surrogate</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string UnescapeSurrogatePair(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            UnescapeSurrogatePair(s, out char highSurrogate, out char lowSurrogate);
            return new string(new char[] { highSurrogate, lowSurrogate });
        }

        /// <summary>
        /// Passthrough for char.IsSurrogatePair
        /// </summary>
        /// <param name="highSurrogate">High surrogate</param>
        /// <param name="lowSurrogate">Low surrogate</param>
        /// <returns>True if surrogate pair, otherwise false</returns>
        public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
        {
            return char.IsSurrogatePair(highSurrogate, lowSurrogate);
        }

        /// <summary>
        /// Checks if the string contains exactly one surrogate pair
        /// </summary>
        /// <param name="s">String to check</param>
        /// <returns>True if surrogate pair, otherwise false</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsSurrogatePair(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return s.Length == 2 && IsSurrogatePair(s[0], s[1]);
        }
    }
}
