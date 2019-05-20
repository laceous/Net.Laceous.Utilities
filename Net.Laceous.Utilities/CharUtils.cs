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
            
            string xu = escapeOptions.UseLowerCaseXInsteadOfLowerCaseU ? "x" : "u";
            string hex = escapeOptions.UseLowerCaseHexInsteadOfUpperCaseHex ? "x4" : "X4";
            
            switch (c)
            {
                case '\'':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\'";
                case '\"':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\\"";
                case '\\':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\\\";
                case '\0':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\0";
                case '\a':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\a";
                case '\b':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\b";
                case '\f':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\f";
                case '\n':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\n";
                case '\r':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\r";
                case '\t':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\t";
                case '\v':
                    return escapeOptions.AlwaysUseUnicodeEscapeSequence ? "\\" + xu + ((int)c).ToString(hex) : "\\v";
                default:
                    switch (escapeOptions.EscapeType)
                    {
                        case CharEscapeType.EscapeAllChars:
                            return "\\" + xu + ((int)c).ToString(hex);
                        case CharEscapeType.EscapeAllCharsExceptAscii:
                            if (c >= 32 && c <= 126) // ascii print chars + space
                            {
                                return c.ToString();
                            }
                            else
                            {
                                return "\\" + xu + ((int)c).ToString(hex);
                            }
                        default:
                            if (c == ' ' || (!char.IsControl(c) && !char.IsWhiteSpace(c)))
                            {
                                return c.ToString();
                            }
                            else
                            {
                                return "\\" + xu + ((int)c).ToString(hex);
                            }
                    }
            }
        }

        /// <summary>
        /// Escape surrogate pair with \\Unnnnnnnn
        /// </summary>
        /// <param name="highSurrogate">High surrogate</param>
        /// <param name="lowSurrogate">Low surrogate</param>
        /// <param name="useLowerCaseHexInsteadOfUpperCaseHex">Use lower case hex instead of upper case hex</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
        public static string EscapeSurrogatePair(char highSurrogate, char lowSurrogate, bool useLowerCaseHexInsteadOfUpperCaseHex = false)
        {
            string hex = useLowerCaseHexInsteadOfUpperCaseHex ? "x8" : "X8";
            return "\\U" + char.ConvertToUtf32(highSurrogate, lowSurrogate).ToString(hex);
        }

        /// <summary>
        /// Escape surrogate pair with \\Unnnnnnnn
        /// </summary>
        /// <param name="s">String containing the surrogate pair</param>
        /// <param name="index">Index position of the surrogate pair</param>
        /// <param name="useLowerCaseHexInsteadOfUpperCaseHex">Use lower case hex instead of upper case hex</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
        public static string EscapeSurrogatePair(string s, int index = 0, bool useLowerCaseHexInsteadOfUpperCaseHex = false)
        {
            if (s == null)
            {
                return null;
            }

            string hex = useLowerCaseHexInsteadOfUpperCaseHex ? "x8" : "X8";
            return "\\U" + char.ConvertToUtf32(s, index).ToString(hex);
        }

        /// <summary>
        /// Unescape backslash sequence to char (e.g. \\n -> \n)
        /// </summary>
        /// <param name="s">String containing the escaped char</param>
        /// <param name="index">Index position of the escaped char</param>
        /// <returns>Char that's been unescaped</returns>
        public static char Unescape(string s, int index = 0)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            string ss = index == 0 ? s : s.Substring(index);

            // longest escaped string: \Unnnnnnnn
            if (ss.Length >= 1 && ss.Length <= 10)
            {
                string unescaped = StringUtils.Unescape(ss);
                if (unescaped.Length == 1)
                {
                    return unescaped[0];
                }
            }
            throw new ArgumentException("String did not contain exactly one escaped char.", "s");
        }

        /// <summary>
        /// Unescape backslash sequence to surrogate pair
        /// </summary>
        /// <param name="s">String containing the escaped surrogate pair</param>
        /// <param name="highSurrogate">Return high surrogate</param>
        /// <param name="lowSurrogate">Return low surrogate</param>
        public static void UnescapeSurrogatePair(string s, out char highSurrogate, out char lowSurrogate) => UnescapeSurrogatePair(s, 0, out highSurrogate, out lowSurrogate);

        /// <summary>
        /// Unescape backslash sequence to surrogate pair
        /// </summary>
        /// <param name="s">String containing the escaped surrogate pair</param>
        /// <param name="index">Index position of the escaped surrogate pair</param>
        /// <param name="highSurrogate">Return high surrogate</param>
        /// <param name="lowSurrogate">Return low surrogate</param>
        public static void UnescapeSurrogatePair(string s, int index, out char highSurrogate, out char lowSurrogate)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            string ss = index == 0 ? s : s.Substring(index);

            // longest escaped string: \unnnn\unnnn
            if (ss.Length >= 2 && ss.Length <= 12)
            {
                string unescaped = StringUtils.Unescape(ss);
                if (unescaped.Length == 2 && char.IsSurrogatePair(unescaped[0], unescaped[1]))
                {
                    highSurrogate = unescaped[0];
                    lowSurrogate = unescaped[1];
                    return;
                }
            }
            throw new ArgumentException("String did not contain exactly one escaped surrogate pair.", "s");
        }

        /// <summary>
        /// Unescape backslash sequence to surrogate pair
        /// </summary>
        /// <param name="s">String containing the escaped surrogate pair</param>
        /// <param name="index">Index position of the escaped surrogate pair</param>
        /// <returns>String containing the high surrogate + low surrogate</returns>
        public static string UnescapeSurrogatePair(string s, int index = 0)
        {
            if (s == null)
            {
                return null;
            }

            UnescapeSurrogatePair(s, index, out char highSurrogate, out char lowSurrogate);
            return new string(new char[] { highSurrogate, lowSurrogate });
        }
    }
}
