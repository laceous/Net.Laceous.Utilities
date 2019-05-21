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
            
            string xu = escapeOptions.UseLowerCaseX ? "x" : "u";
            string hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
            
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
        /// <param name="useLowerCaseHex">Use lower case hex instead of upper case hex</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
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
        public static string EscapeSurrogatePair(string s, bool useLowerCaseHex = false)
        {
            if (s == null)
            {
                return null;
            }

            if (IsSurrogatePair(s))
            {
                return EscapeSurrogatePair(s[0], s[1], useLowerCaseHex);
            }
            throw new ArgumentException("String did not contain exactly one surrogate pair.", "s");
        }

        /// <summary>
        /// Unescape backslash sequence to char (e.g. \\n -> \n)
        /// </summary>
        /// <param name="s">String containing the escaped char</param>
        /// <returns>Char that's been unescaped</returns>
        public static char Unescape(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            // longest escaped string: \Unnnnnnnn
            if (s.Length >= 1 && s.Length <= 10)
            {
                string unescaped = StringUtils.Unescape(s);
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
        public static void UnescapeSurrogatePair(string s, out char highSurrogate, out char lowSurrogate)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            // longest escaped string: \unnnn\unnnn
            if (s.Length >= 2 && s.Length <= 12)
            {
                string unescaped = StringUtils.Unescape(s);
                if (IsSurrogatePair(unescaped))
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
        /// <returns>String containing the high surrogate + low surrogate</returns>
        public static string UnescapeSurrogatePair(string s)
        {
            if (s == null)
            {
                return null;
            }

            UnescapeSurrogatePair(s, out char highSurrogate, out char lowSurrogate);
            return new string(new char[] { highSurrogate, lowSurrogate });
        }

        /// <summary>
        /// Checks if the string contains exactly one surrogate pair
        /// </summary>
        /// <param name="s">String to check</param>
        /// <returns>True if surrogate pair, otherwise false</returns>
        public static bool IsSurrogatePair(string s)
        {
            if (s == null)
            {
                return false;
            }
            
            return s.Length == 2 && char.IsSurrogatePair(s[0], s[1]);
        }
    }
}
