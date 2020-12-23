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

            switch (escapeOptions.EscapeLanguage)
            {
                case EscapeLanguage.FSharp:
                    return EscapeFSharp(c, escapeOptions);
                default: // EscapeLanguage.CSharp
                    return EscapeCSharp(c, escapeOptions);
            }
        }

        /// <summary>
        /// Escape char with backslash sequence (e.g. \n -> \\n)
        /// </summary>
        /// <param name="c">Char to escape</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <returns>String with escape sequence for char</returns>
        private static string EscapeCSharp(char c, CharEscapeOptions escapeOptions)
        {
            if (escapeOptions.EscapeLetter == EscapeLetter.Decimal3)
            {
                throw new ArgumentException("Decimal3 is not valid for CSharp.", nameof(escapeOptions));
            }

            string xu;
            string hex;
            switch (escapeOptions.EscapeLetter)
            {
                case EscapeLetter.LowerCaseX1:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x1" : "X1";
                    break;
                case EscapeLetter.LowerCaseX2:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x2" : "X2";
                    break;
                case EscapeLetter.LowerCaseX3:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x3" : "X3";
                    break;
                case EscapeLetter.LowerCaseX4:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
                    break;
                case EscapeLetter.UpperCaseU8:
                    xu = "U";
                    hex = escapeOptions.UseLowerCaseHex ? "x8" : "X8";
                    break;
                default: // EscapeLetter.LowerCaseU4
                    xu = "u";
                    hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
                    break;
            }

            if (escapeOptions.UseShortEscape)
            {
                switch (c)
                {
                    case '\'':
                        return "\\'";
                    case '\"':
                        return "\\\"";
                    case '\\':
                        return "\\\\";
                    case '\0':
                        return "\\0";
                    case '\a':
                        return "\\a";
                    case '\b':
                        return "\\b";
                    case '\f':
                        return "\\f";
                    case '\n':
                        return "\\n";
                    case '\r':
                        return "\\r";
                    case '\t':
                        return "\\t";
                    case '\v':
                        return "\\v";
                }
            }

            return "\\" + xu + ((int)c).ToString(hex);
        }

        /// <summary>
        /// Escape char with backslash sequence (e.g. \n -> \\n)
        /// </summary>
        /// <param name="c">Char to escape</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <returns>String with escape sequence for char</returns>
        private static string EscapeFSharp(char c, CharEscapeOptions escapeOptions)
        {
            if (escapeOptions.EscapeLetter == EscapeLetter.LowerCaseX1)
            {
                throw new ArgumentException("LowerCaseX1 is not valid for FSharp.", nameof(escapeOptions));
            }
            if (escapeOptions.EscapeLetter == EscapeLetter.LowerCaseX3)
            {
                throw new ArgumentException("LowerCaseX3 is not valid for FSharp.", nameof(escapeOptions));
            }
            if (escapeOptions.EscapeLetter == EscapeLetter.LowerCaseX4)
            {
                throw new ArgumentException("LowerCaseX4 is not valid for FSharp.", nameof(escapeOptions));
            }

            string xu;
            string hex;
            switch (escapeOptions.EscapeLetter)
            {
                case EscapeLetter.Decimal3:
                    xu = "";
                    hex = "D3";
                    break;
                case EscapeLetter.LowerCaseX2:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x2" : "X2";
                    break;
                case EscapeLetter.UpperCaseU8:
                    xu = "U";
                    hex = escapeOptions.UseLowerCaseHex ? "x8" : "X8";
                    break;
                default: // EscapeLetter.LowerCaseU4
                    xu = "u";
                    hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
                    break;
            }

            if (escapeOptions.UseShortEscape)
            {
                // FSharp doesn't define \0
                switch (c)
                {
                    case '\a':
                        return "\\a";
                    case '\b':
                        return "\\b";
                    case '\f':
                        return "\\f";
                    case '\n':
                        return "\\n";
                    case '\r':
                        return "\\r";
                    case '\t':
                        return "\\t";
                    case '\v':
                        return "\\v";
                    case '\\':
                        return "\\\\";
                    case '\'':
                        return "\\'";
                    case '\"':
                        return "\\\"";
                }
            }

            if (c > 255 && (escapeOptions.EscapeLetter == EscapeLetter.Decimal3 || escapeOptions.EscapeLetter == EscapeLetter.LowerCaseX2))
            {
                // switch to \\uHHHH because we can only support 0-255 here for \\DDD and \\xHH
                return "\\u" + ((int)c).ToString(escapeOptions.UseLowerCaseHex ? "x4" : "X4");
            }
            else
            {
                return "\\" + xu + ((int)c).ToString(hex);
            }
        }

        /// <summary>
        /// Escape surrogate pair with \\UHHHHHHHH
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
        /// Escape surrogate pair with \\UHHHHHHHH
        /// </summary>
        /// <param name="s">String containing the surrogate pair</param>
        /// <param name="useLowerCaseHex">Use lower case hex instead of upper case hex</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
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
        /// <param name="escapeLanguage">C# or F#</param>
        /// <returns>Char that's been unescaped</returns>
        public static char Unescape(string s, CharUnescapeOptions unescapeOptions = null)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (unescapeOptions == null)
            {
                unescapeOptions = new CharUnescapeOptions();
            }

            // escaped char will have more than 1 char
            // longest escaped string: \UHHHHHHHH
            if (s.Length > 1 && s.Length <= 10 && s.StartsWith("\\", StringComparison.Ordinal))
            {
                string unescaped = StringUtils.Unescape(s, new StringUnescapeOptions(isUnrecognizedEscapeVerbatim: true), unescapeOptions);
                if (unescaped.Length == 1)
                {
                    return unescaped[0];
                }
            }
            throw new ArgumentException("String did not contain exactly one escaped char.", nameof(s));
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
                throw new ArgumentNullException(nameof(s));
            }

            // escaped surrogate pairs look like this: \UHHHHHHHH
            // you could techincally have an escaped surrogate pair look like this: \uHHHH\uHHHH
            // however, this method is for reversing CharUtils.EscapeSurrogatePair which always uses \U
            if (s.Length == 10 && s.StartsWith("\\U", StringComparison.Ordinal))
            {
                // \\U is handled the same between C# and F#
                string unescaped = StringUtils.Unescape(s, new StringUnescapeOptions(isUnrecognizedEscapeVerbatim: true));

                if (IsSurrogatePair(unescaped))
                {
                    highSurrogate = unescaped[0];
                    lowSurrogate = unescaped[1];
                    return;
                }
            }
            throw new ArgumentException("String did not contain exactly one escaped surrogate pair.", nameof(s));
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
