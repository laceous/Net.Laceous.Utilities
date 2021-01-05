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
        /// <exception cref="ArgumentException"></exception>
        public static string Escape(char c, CharEscapeOptions escapeOptions = null)
        {
            if (escapeOptions == null)
            {
                escapeOptions = new CharEscapeOptions();
            }

            switch (escapeOptions.EscapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                    return EscapeCSharp(c, escapeOptions);
                case CharEscapeLanguage.FSharp:
                    return EscapeFSharp(c, escapeOptions);
                case CharEscapeLanguage.PowerShell:
                    return EscapePowerShell(c, escapeOptions);
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid EscapeLanguage.", escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }
        }

        /// <summary>
        /// Escape char with backslash sequence (e.g. \n -> \\n)
        /// </summary>
        /// <param name="c">Char to escape</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <returns>String with escape sequence for char</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string EscapeCSharp(char c, CharEscapeOptions escapeOptions)
        {
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

            string xu;
            string hex;
            switch (escapeOptions.EscapeLetter)
            {
                case CharEscapeLetter.LowerCaseX1:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x1" : "X1";
                    break;
                case CharEscapeLetter.LowerCaseX2:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x2" : "X2";
                    break;
                case CharEscapeLetter.LowerCaseX3:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x3" : "X3";
                    break;
                case CharEscapeLetter.LowerCaseX4:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
                    break;
                case CharEscapeLetter.LowerCaseU4:
                    xu = "u";
                    hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
                    break;
                case CharEscapeLetter.UpperCaseU8:
                    xu = "U";
                    hex = escapeOptions.UseLowerCaseHex ? "x8" : "X8";
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid EscapeLetter for {1}.", escapeOptions.EscapeLetter, escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            return "\\" + xu + ((int)c).ToString(hex);
        }

        /// <summary>
        /// Escape char with backslash sequence (e.g. \n -> \\n)
        /// </summary>
        /// <param name="c">Char to escape</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <returns>String with escape sequence for char</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string EscapeFSharp(char c, CharEscapeOptions escapeOptions)
        {
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

            string xu;
            string hex;
            switch (escapeOptions.EscapeLetter)
            {
                case CharEscapeLetter.Decimal3:
                    xu = "";
                    hex = "D3";
                    break;
                case CharEscapeLetter.LowerCaseX2:
                    xu = "x";
                    hex = escapeOptions.UseLowerCaseHex ? "x2" : "X2";
                    break;
                case CharEscapeLetter.LowerCaseU4:
                    xu = "u";
                    hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
                    break;
                case CharEscapeLetter.UpperCaseU8:
                    xu = "U";
                    hex = escapeOptions.UseLowerCaseHex ? "x8" : "X8";
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid EscapeLetter for {1}.", escapeOptions.EscapeLetter, escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            // we can only support 0-255 here for \\DDD and \\xHH
            if (c > 255 && (escapeOptions.EscapeLetter == CharEscapeLetter.Decimal3 || escapeOptions.EscapeLetter == CharEscapeLetter.LowerCaseX2))
            {
                switch (escapeOptions.EscapeLetterFallback)
                {
                    case CharEscapeLetter.LowerCaseU4:
                        xu = "u";
                        hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
                        break;
                    case CharEscapeLetter.UpperCaseU8:
                        xu = "U";
                        hex = escapeOptions.UseLowerCaseHex ? "x8" : "X8";
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid EscapeLetterFallback for {1}.", escapeOptions.EscapeLetter, escapeOptions.EscapeLanguage), nameof(escapeOptions));
                }
            }

            return "\\" + xu + ((int)c).ToString(hex);
        }

        /// <summary>
        /// Escape char with backslash sequence (e.g. \n -> `n)
        /// </summary>
        /// <param name="c">Char to escape</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <returns>String with escape sequence for char</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string EscapePowerShell(char c, CharEscapeOptions escapeOptions)
        {
            if (escapeOptions.UseShortEscape)
            {
                switch (c)
                {
                    case '\0':
                        return "`0";
                    case '\a':
                        return "`a";
                    case '\b':
                        return "`b";
                    case '\x1B':     // escape
                        return "`e"; // this is supported in PowerShell v7, but not Windows PowerShell v5
                    case '\f':
                        return "`f";
                    case '\n':
                        return "`n";
                    case '\r':
                        return "`r";
                    case '\t':
                        return "`t";
                    case '\v':
                        return "`v";
                }
            }

            string hex;
            switch (escapeOptions.EscapeLetter)
            {
                case CharEscapeLetter.LowerCaseU1:
                    hex = escapeOptions.UseLowerCaseHex ? "x1" : "X1";
                    break;
                case CharEscapeLetter.LowerCaseU2:
                    hex = escapeOptions.UseLowerCaseHex ? "x2" : "X2";
                    break;
                case CharEscapeLetter.LowerCaseU3:
                    hex = escapeOptions.UseLowerCaseHex ? "x3" : "X3";
                    break;
                case CharEscapeLetter.LowerCaseU4:
                    hex = escapeOptions.UseLowerCaseHex ? "x4" : "X4";
                    break;
                case CharEscapeLetter.LowerCaseU5:
                    hex = escapeOptions.UseLowerCaseHex ? "x5" : "X5";
                    break;
                case CharEscapeLetter.LowerCaseU6:
                    hex = escapeOptions.UseLowerCaseHex ? "x6" : "X6";
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid EscapeLetter for {1}.", escapeOptions.EscapeLetter, escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            return "`u{" + ((int)c).ToString(hex) + "}"; // this is supported in PowerShell v7, but not Windows PowerShell v5
        }

        /// <summary>
        /// Escape surrogate pair with \\UHHHHHHHH or `u{HHHHH}
        /// </summary>
        /// <param name="highSurrogate">High surrogate</param>
        /// <param name="lowSurrogate">Low surrogate</param>
        /// <param name="escapeOptions">Char escape options</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string EscapeSurrogatePair(char highSurrogate, char lowSurrogate, CharEscapeOptions escapeOptions = null)
        {
            if (escapeOptions == null)
            {
                escapeOptions = new CharEscapeOptions();
            }

            string hex;
            switch (escapeOptions.EscapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                case CharEscapeLanguage.FSharp:
                    // automatically use UpperCaseU8
                    hex = escapeOptions.UseLowerCaseHex ? "x8" : "X8";
                    return "\\U" + char.ConvertToUtf32(highSurrogate, lowSurrogate).ToString(hex);
                case CharEscapeLanguage.PowerShell:
                    switch (escapeOptions.EscapeLetter)
                    {
                        // 1/2/3/4 will output as 5 so we can just fall through
                        case CharEscapeLetter.LowerCaseU1:
                        case CharEscapeLetter.LowerCaseU2:
                        case CharEscapeLetter.LowerCaseU3:
                        case CharEscapeLetter.LowerCaseU4:
                        case CharEscapeLetter.LowerCaseU5:
                            hex = escapeOptions.UseLowerCaseHex ? "x5" : "X5";
                            break;
                        case CharEscapeLetter.LowerCaseU6:
                            hex = escapeOptions.UseLowerCaseHex ? "x6" : "X6";
                            break;
                        default:
                            throw new ArgumentException(string.Format("{0} is not a valid EscapeLetter for {1}.", escapeOptions.EscapeLetter, escapeOptions.EscapeLanguage), nameof(escapeOptions));
                    }
                    return "`u{" + char.ConvertToUtf32(highSurrogate, lowSurrogate).ToString(hex) + "}";
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid EscapeLanguage.", escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }
        }

        /// <summary>
        /// Escape surrogate pair with \\UHHHHHHHH or `u{HHHHH}
        /// </summary>
        /// <param name="s">String containing the surrogate pair</param>
        /// <param name="escapeOptions">Char escape options</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string EscapeSurrogatePair(string s, CharEscapeOptions escapeOptions = null)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (s.Length == 2 && char.IsSurrogatePair(s[0], s[1]))
            {
                return EscapeSurrogatePair(s[0], s[1], escapeOptions);
            }
            throw new ArgumentException("String did not contain exactly one surrogate pair.", nameof(s));
        }

        /// <summary>
        /// Unescape backslash sequence to char (e.g. \\n -> \n)
        /// </summary>
        /// <param name="s">String containing the escaped char</param>
        /// <param name="unescapeOptions">Unescape options</param>
        /// <returns>Char that's been unescaped</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
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

            string unescaped = String.Empty;
            switch (unescapeOptions.EscapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                case CharEscapeLanguage.FSharp:
                    // escaped char will have more than 1 char
                    // longest escaped string: \UHHHHHHHH
                    if (s.Length > 1 && s.Length <= 10 && s.StartsWith("\\", StringComparison.Ordinal))
                    {
                        unescaped = StringUtils.Unescape(s, new StringUnescapeOptions(isUnrecognizedEscapeVerbatim: true), unescapeOptions);
                    }
                    break;
                case CharEscapeLanguage.PowerShell:
                    // escaped char will have more than 1 char
                    // longest escaped string: `u{HHHHHH}
                    if (s.Length > 1 && s.Length <= 10 && s.StartsWith("`", StringComparison.Ordinal))
                    {
                        unescaped = StringUtils.Unescape(s, new StringUnescapeOptions(isUnrecognizedEscapeVerbatim: true), unescapeOptions);
                    }
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid EscapeLanguage.", unescapeOptions.EscapeLanguage), nameof(unescapeOptions));
            }

            if (unescaped.Length == 1)
            {
                return unescaped[0];
            }
            throw new ArgumentException("String did not contain exactly one escaped char.", nameof(s));
        }

        /// <summary>
        /// Unescape backslash sequence to surrogate pair
        /// </summary>
        /// <param name="s">String containing the escaped surrogate pair</param>
        /// <param name="highSurrogate">Return high surrogate</param>
        /// <param name="lowSurrogate">Return low surrogate</param>
        /// <param name="unescapeOptions">Char unescape options</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void UnescapeSurrogatePair(string s, out char highSurrogate, out char lowSurrogate, CharUnescapeOptions unescapeOptions = null)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (unescapeOptions == null)
            {
                unescapeOptions = new CharUnescapeOptions();
            }

            string unescaped = String.Empty;
            switch (unescapeOptions.EscapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                case CharEscapeLanguage.FSharp:
                    // escaped surrogate pairs look like this: \UHHHHHHHH
                    // you could techincally have an escaped surrogate pair look like this: \uHHHH\uHHHH
                    // however, this method is for reversing CharUtils.EscapeSurrogatePair which always uses \U
                    if (s.Length == 10 && s.StartsWith("\\U", StringComparison.Ordinal))
                    {
                        unescaped = StringUtils.Unescape(s, new StringUnescapeOptions(isUnrecognizedEscapeVerbatim: true), unescapeOptions);
                    }
                    break;
                case CharEscapeLanguage.PowerShell:
                    // escaped surrogate pairs look like this: `u{HHHHH}, `u{HHHHHH}
                    if (s.Length >= 9 && s.Length <= 10 && s.StartsWith("`u{", StringComparison.Ordinal))
                    {
                        unescaped = StringUtils.Unescape(s, new StringUnescapeOptions(isUnrecognizedEscapeVerbatim: true), unescapeOptions);
                    }
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid EscapeLanguage.", unescapeOptions.EscapeLanguage), nameof(unescapeOptions));
            }

            if (unescaped.Length == 2 && char.IsSurrogatePair(unescaped[0], unescaped[1]))
            {
                highSurrogate = unescaped[0];
                lowSurrogate = unescaped[1];
                return;
            }
            throw new ArgumentException("String did not contain exactly one escaped surrogate pair.", nameof(s));
        }

        /// <summary>
        /// Unescape backslash sequence to surrogate pair
        /// </summary>
        /// <param name="s">String containing the escaped surrogate pair</param>
        /// <param name="unescapeOptions">Char unescape options</param>
        /// <returns>String containing the high surrogate + low surrogate</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string UnescapeSurrogatePair(string s, CharUnescapeOptions unescapeOptions = null)
        {
            UnescapeSurrogatePair(s, out char highSurrogate, out char lowSurrogate, unescapeOptions);
            return new string(new char[] { highSurrogate, lowSurrogate });
        }
    }
}
