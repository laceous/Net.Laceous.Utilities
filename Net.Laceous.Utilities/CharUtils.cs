using Net.Laceous.Utilities.Extensions;
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
        /// <param name="addQuotes">Add quotes after escaping</param>
        /// <returns>String with escape sequence for char</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Escape(char c, CharEscapeOptions escapeOptions = null, bool addQuotes = false)
        {
            if (escapeOptions == null)
            {
                escapeOptions = new CharEscapeOptions();
            }

            switch (escapeOptions.EscapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                    return EscapeCSharp(c, escapeOptions, addQuotes);
                case CharEscapeLanguage.FSharp:
                    return EscapeFSharp(c, escapeOptions, addQuotes);
                case CharEscapeLanguage.PowerShell:
                    throw new ArgumentException(string.Format("{0} does not have a char type.", escapeOptions.EscapeLanguage), nameof(escapeOptions));
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", escapeOptions.EscapeLanguage, nameof(escapeOptions.EscapeLanguage)), nameof(escapeOptions));
            }
        }

        /// <summary>
        /// Escape char with backslash sequence (e.g. \n -> \\n)
        /// </summary>
        /// <param name="c">Char to escape</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <param name="addQuotes">Add quotes after escaping</param>
        /// <returns>String with escape sequence for char</returns>
        /// <exception cref="ArgumentException"></exception>
        internal static string EscapeCSharp(char c, CharEscapeOptions escapeOptions, bool addQuotes = false)
        {
            if (escapeOptions.UseShortEscape)
            {
                switch (c)
                {
                    case '\'':
                        return "\\\'";
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
                    throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.EscapeLetter, nameof(escapeOptions.EscapeLetter), escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            string escaped = "\\" + xu + ((int)c).ToString(hex);
            if (addQuotes)
            {
                escaped = "\'" + escaped + "\'";
            }
            return escaped;
        }

        /// <summary>
        /// Escape char with backslash sequence (e.g. \n -> \\n)
        /// </summary>
        /// <param name="c">Char to escape</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <param name="addQuotes">Add quotes after escaping</param>
        /// <returns>String with escape sequence for char</returns>
        /// <exception cref="ArgumentException"></exception>
        internal static string EscapeFSharp(char c, CharEscapeOptions escapeOptions, bool addQuotes = false)
        {
            if (escapeOptions.UseShortEscape)
            {
                // FSharp doesn't define \0, the closest is \000
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
                        return "\\\'";
                    case '\"':
                        return "\\\"";
                }
            }

            string xu = null;
            string hex = null;
            switch (escapeOptions.EscapeLetter)
            {
                case CharEscapeLetter.None3:
                    if (c <= 255)
                    {
                        xu = "";
                        hex = "D3";
                    }
                    break;
                case CharEscapeLetter.LowerCaseX2:
                    if (c <= 0xFF)
                    {
                        xu = "x";
                        hex = escapeOptions.UseLowerCaseHex ? "x2" : "X2";
                    }
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
                    throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.EscapeLetter, nameof(escapeOptions.EscapeLetter), escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            if (xu == null || hex == null)
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
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.EscapeLetterFallback, nameof(escapeOptions.EscapeLetterFallback), escapeOptions.EscapeLanguage), nameof(escapeOptions));
                }
            }

            string escaped = "\\" + xu + ((int)c).ToString(hex);
            if (addQuotes)
            {
                escaped = "\'" + escaped + "\'";
            }
            return escaped;
        }

        /// <summary>
        /// Escape char with backslash sequence (e.g. \n -> `n)
        /// </summary>
        /// <param name="c">Char to escape</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <returns>String with escape sequence for char</returns>
        /// <exception cref="ArgumentException"></exception>
        internal static string EscapePowerShell(char c, CharEscapeOptions escapeOptions)
        {
            if (escapeOptions.UseShortEscape)
            {
                // escaping single quotes here doesn't make any sense
                // within double-quoted strings `' is the same as '
                // within single-quoted strings `' doesn't work, you have to double ' to ''
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
                    case '`':
                        return "``";
                    case '\"':
                        return "`\""; // technically you can use either `" or "" within double-quoted strings
                    case '$':
                        return "`$";  // dollar sign interpolates variables, something we can't support, so escape it
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
                    throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.EscapeLetter, nameof(escapeOptions.EscapeLetter), escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            return "`u{" + ((int)c).ToString(hex) + "}"; // this is supported in PowerShell v7, but not Windows PowerShell v5
        }

        /// <summary>
        /// Escape surrogate pair with \\UHHHHHHHH
        /// </summary>
        /// <param name="highSurrogate">High surrogate</param>
        /// <param name="lowSurrogate">Low surrogate</param>
        /// <param name="escapeOptions">Char escape options</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal static string EscapeSurrogatePairCSharp(char highSurrogate, char lowSurrogate, CharEscapeOptions escapeOptions)
        {
            int codePoint = char.ConvertToUtf32(highSurrogate, lowSurrogate);

            string hex;
            switch (escapeOptions.SurrogatePairEscapeLetter)
            {
                case CharEscapeLetter.UpperCaseU8:
                    hex = escapeOptions.UseLowerCaseHex ? "x8" : "X8";
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.SurrogatePairEscapeLetter, nameof(escapeOptions.SurrogatePairEscapeLetter), escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            return "\\U" + codePoint.ToString(hex);
        }

        /// <summary>
        /// Escape surrogate pair with \\UHHHHHHHH
        /// </summary>
        /// <param name="highSurrogate">High surrogate</param>
        /// <param name="lowSurrogate">Low surrogate</param>
        /// <param name="escapeOptions">Char escape options</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal static string EscapeSurrogatePairFSharp(char highSurrogate, char lowSurrogate, CharEscapeOptions escapeOptions)
        {
            int codePoint = char.ConvertToUtf32(highSurrogate, lowSurrogate);

            string hex;
            switch (escapeOptions.SurrogatePairEscapeLetter)
            {
                case CharEscapeLetter.UpperCaseU8:
                    hex = escapeOptions.UseLowerCaseHex ? "x8" : "X8";
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.SurrogatePairEscapeLetter, nameof(escapeOptions.SurrogatePairEscapeLetter), escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            return "\\U" + codePoint.ToString(hex);
        }

        /// <summary>
        /// Escape surrogate pair with `u{HHHHH}
        /// </summary>
        /// <param name="highSurrogate">High surrogate</param>
        /// <param name="lowSurrogate">Low surrogate</param>
        /// <param name="escapeOptions">Char escape options</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal static string EscapeSurrogatePairPowerShell(char highSurrogate, char lowSurrogate, CharEscapeOptions escapeOptions)
        {
            int codePoint = char.ConvertToUtf32(highSurrogate, lowSurrogate);

            string hex;
            switch (escapeOptions.SurrogatePairEscapeLetter)
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
                    throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.SurrogatePairEscapeLetter, nameof(escapeOptions.SurrogatePairEscapeLetter), escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            return "`u{" + codePoint.ToString(hex) + "}";
        }

        /// <summary>
        /// Unescape backslash sequence to char (e.g. \\n -> \n)
        /// </summary>
        /// <param name="s">String containing the escaped char</param>
        /// <param name="unescapeOptions">Unescape options</param>
        /// <param name="removeQuotes">Remove quotes before parsing</param>
        /// <returns>Char that's been unescaped</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static char Unescape(string s, CharUnescapeOptions unescapeOptions = null, bool removeQuotes = false)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (unescapeOptions == null)
            {
                unescapeOptions = new CharUnescapeOptions();
            }

            switch (unescapeOptions.EscapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                    return UnescapeCSharp(s, unescapeOptions, removeQuotes);
                case CharEscapeLanguage.FSharp:
                    return UnescapeFSharp(s, unescapeOptions, removeQuotes);
                case CharEscapeLanguage.PowerShell:
                    throw new ArgumentException(string.Format("{0} does not have a char type.", unescapeOptions.EscapeLanguage), nameof(unescapeOptions));
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", unescapeOptions.EscapeLanguage, nameof(unescapeOptions.EscapeLanguage)), nameof(unescapeOptions));
            }
        }

        /// <summary>
        /// Unescape backslash sequence to char (e.g. \\n -> \n)
        /// </summary>
        /// <param name="s">String containing the escaped char</param>
        /// <param name="unescapeOptions">Unescape options</param>
        /// <param name="removeQuotes">Remove quotes before parsing</param>
        /// <returns>Char that's been unescaped</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static char UnescapeCSharp(string s, CharUnescapeOptions unescapeOptions, bool removeQuotes)
        {
            // chars in C# are surrounded by single-quotes
            if (removeQuotes)
            {
                if (s.Length >= 2 && s[0].IsSingleQuote() && s[s.Length - 1].IsSingleQuote())
                {
                    s = s.Substring(1, s.Length - 2);
                }
                else
                {
                    throw new ArgumentException("Char was not single quoted.", nameof(s));
                }
            }

            string unescaped = null;
            if (s.Length == 1)
            {
                if (s[0].IsSingleQuote() || s[0].IsCarriageReturn() || s[0].IsLineFeed() || s[0].IsNextLine() || s[0].IsLineSeparator() || s[0].IsParagraphSeparator())
                {
                    if (!unescapeOptions.IsUnrecognizedEscapeVerbatim)
                    {
                        throw new ArgumentException("Unrecognized escape sequence.", nameof(s));
                    }
                }
                unescaped = s;
            }
            else if (s.Length > 1 && s.Length <= 10 && s.StartsWith("\\", StringComparison.Ordinal))
            {
                // escaped char will have more than 1 char
                // longest escaped string: \UHHHHHHHH
                unescaped = StringUtils.UnescapeCSharp(s, unescapeOptions);
            }

            if (unescaped != null && unescaped.Length == 1)
            {
                return unescaped[0];
            }
            throw new ArgumentException("String did not contain exactly one char (escaped or unescaped).", nameof(s));
        }

        /// <summary>
        /// Unescape backslash sequence to char (e.g. \\n -> \n)
        /// </summary>
        /// <param name="s">String containing the escaped char</param>
        /// <param name="unescapeOptions">Unescape options</param>
        /// <param name="removeQuotes">Remove quotes before parsing</param>
        /// <returns>Char that's been unescaped</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static char UnescapeFSharp(string s, CharUnescapeOptions unescapeOptions, bool removeQuotes)
        {
            // chars in F# are surrounded by single-quotes
            if (removeQuotes)
            {
                if (s.Length >= 2 && s[0].IsSingleQuote() && s[s.Length - 1].IsSingleQuote())
                {
                    s = s.Substring(1, s.Length - 2);
                }
                else
                {
                    throw new ArgumentException("Char was not single quoted.", nameof(s));
                }
            }

            string unescaped = null;
            if (s.Length == 1)
            {
                if (s[0].IsSingleQuote() || s[0].IsCarriageReturn() || s[0].IsLineFeed())
                {
                    if (!unescapeOptions.IsUnrecognizedEscapeVerbatim)
                    {
                        throw new ArgumentException("Unrecognized escape sequence.", nameof(s));
                    }
                }
                unescaped = s;
            }
            else if (s.Length > 1 && s.Length <= 10 && s.StartsWith("\\", StringComparison.Ordinal))
            {
                unescaped = StringUtils.UnescapeFSharp(s, unescapeOptions);
            }

            if (unescaped != null && unescaped.Length == 1)
            {
                return unescaped[0];
            }
            throw new ArgumentException("String did not contain exactly one char (escaped or unescaped).", nameof(s));
        }
    }
}
