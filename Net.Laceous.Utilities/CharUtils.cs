using System;
using System.Globalization;
using System.Unicode;

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
                case CharEscapeLanguage.Python:
                    return EscapePython(c, escapeOptions);
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", escapeOptions.EscapeLanguage, nameof(escapeOptions.EscapeLanguage)), nameof(escapeOptions));
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
                // `` and `" might be useful
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
                    throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.EscapeLetter, nameof(escapeOptions.EscapeLetter), escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            return "`u{" + ((int)c).ToString(hex) + "}"; // this is supported in PowerShell v7, but not Windows PowerShell v5
        }

        /// <summary>
        /// Escape char with backslash sequence (e.g. \n -> \\n)
        /// </summary>
        /// <param name="c">Char to escape</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <returns>String with escape sequence for char</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string EscapePython(char c, CharEscapeOptions escapeOptions)
        {
            if (escapeOptions.UseShortEscape)
            {
                // \0 isn't defined on its own, but it technically works as part of an octal escape
                switch (c)
                {
                    case '\\':
                        return "\\\\";
                    case '\'':
                        return "\\\'";
                    case '\"':
                        return "\\\"";
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

            string xu = null;
            string suffix = null;
            switch (escapeOptions.EscapeLetter)
            {
                case CharEscapeLetter.None1:
                    if (c <= 0x1FF) // 777 (max 3 char octal), 511 (decimal)
                    {
                        xu = "";
                        suffix = Convert.ToString((int)c, 8); // convert to octal
                    }
                    break;
                case CharEscapeLetter.None2:
                    if (c <= 0x1FF)
                    {
                        xu = "";
                        suffix = Convert.ToString((int)c, 8).PadLeft(2, '0');
                    }
                    break;
                case CharEscapeLetter.None3:
                    if (c <= 0x1FF)
                    {
                        xu = "";
                        suffix = Convert.ToString((int)c, 8).PadLeft(3, '0');
                    }
                    break;
                case CharEscapeLetter.LowerCaseX2:
                    if (c <= 0xFF) // 255 (decimal)
                    {
                        xu = "x";
                        suffix = ((int)c).ToString(escapeOptions.UseLowerCaseHex ? "x2" : "X2");
                    }
                    break;
                case CharEscapeLetter.LowerCaseU4:
                    xu = "u";
                    suffix = ((int)c).ToString(escapeOptions.UseLowerCaseHex ? "x4" : "X4");
                    break;
                case CharEscapeLetter.UpperCaseU8:
                    xu = "U";
                    suffix = ((int)c).ToString(escapeOptions.UseLowerCaseHex ? "x8" : "X8");
                    break;
                case CharEscapeLetter.UpperCaseN1:
                    UnicodeCharInfo charInfo = UnicodeInfo.GetCharInfo((int)c); // python 3.3 added support for aliases, fall back to that if there's no name
                    string name = !string.IsNullOrEmpty(charInfo.Name) ? charInfo.Name : charInfo.NameAliases.Count > 0 ? charInfo.NameAliases[0].Name : null;
                    if (!string.IsNullOrEmpty(name) && !name.Contains("}")) // StringComparison.Ordinal
                    {
                        xu = "N";
                        suffix = "{" + CultureInfo.InvariantCulture.TextInfo.ToTitleCase(name.ToLowerInvariant()) + "}";
                    }
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.EscapeLetter, nameof(escapeOptions.EscapeLetter), escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            if (xu == null || suffix == null)
            {
                switch (escapeOptions.EscapeLetterFallback)
                {
                    case CharEscapeLetter.LowerCaseU4:
                        xu = "u";
                        suffix = ((int)c).ToString(escapeOptions.UseLowerCaseHex ? "x4" : "X4");
                        break;
                    case CharEscapeLetter.UpperCaseU8:
                        xu = "U";
                        suffix = ((int)c).ToString(escapeOptions.UseLowerCaseHex ? "x8" : "X8");
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.EscapeLetterFallback, nameof(escapeOptions.EscapeLetterFallback), escapeOptions.EscapeLanguage), nameof(escapeOptions));
                }
            }

            return "\\" + xu + suffix;
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

            switch (escapeOptions.EscapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                    return EscapeSurrogatePairCSharp(highSurrogate, lowSurrogate, escapeOptions);
                case CharEscapeLanguage.FSharp:
                    return EscapeSurrogatePairFSharp(highSurrogate, lowSurrogate, escapeOptions);
                case CharEscapeLanguage.PowerShell:
                    return EscapeSurrogatePairPowerShell(highSurrogate, lowSurrogate, escapeOptions);
                case CharEscapeLanguage.Python:
                    return EscapeSurrogatePairPython(highSurrogate, lowSurrogate, escapeOptions);
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", escapeOptions.EscapeLanguage, nameof(escapeOptions.EscapeLanguage)), nameof(escapeOptions));
            }
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
        private static string EscapeSurrogatePairCSharp(char highSurrogate, char lowSurrogate, CharEscapeOptions escapeOptions)
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
        private static string EscapeSurrogatePairFSharp(char highSurrogate, char lowSurrogate, CharEscapeOptions escapeOptions)
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
        private static string EscapeSurrogatePairPowerShell(char highSurrogate, char lowSurrogate, CharEscapeOptions escapeOptions)
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
        /// Escape surrogate pair with \\UHHHHHHHH
        /// </summary>
        /// <param name="highSurrogate">High surrogate</param>
        /// <param name="lowSurrogate">Low surrogate</param>
        /// <param name="escapeOptions">Char escape options</param>
        /// <returns>String with escape sequence for surrogate pair</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static string EscapeSurrogatePairPython(char highSurrogate, char lowSurrogate, CharEscapeOptions escapeOptions)
        {
            int codePoint = char.ConvertToUtf32(highSurrogate, lowSurrogate);

            string xu = null;
            string suffix = null;
            switch (escapeOptions.SurrogatePairEscapeLetter)
            {
                case CharEscapeLetter.UpperCaseU8:
                    xu = "U";
                    suffix = codePoint.ToString(escapeOptions.UseLowerCaseHex ? "x8" : "X8");
                    break;
                case CharEscapeLetter.UpperCaseN1:
                    UnicodeCharInfo charInfo = UnicodeInfo.GetCharInfo(codePoint);
                    string name = !string.IsNullOrEmpty(charInfo.Name) ? charInfo.Name : charInfo.NameAliases.Count > 0 ? charInfo.NameAliases[0].Name : null;
                    if (!string.IsNullOrEmpty(name) && !name.Contains("}"))
                    {
                        xu = "N";
                        suffix = "{" + CultureInfo.InvariantCulture.TextInfo.ToTitleCase(name.ToLowerInvariant()) + "}";
                    }
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.SurrogatePairEscapeLetter, nameof(escapeOptions.SurrogatePairEscapeLetter), escapeOptions.EscapeLanguage), nameof(escapeOptions));
            }

            if (xu == null || suffix == null)
            {
                switch (escapeOptions.SurrogatePairEscapeLetterFallback)
                {
                    case CharEscapeLetter.UpperCaseU8:
                        xu = "U";
                        suffix = codePoint.ToString(escapeOptions.UseLowerCaseHex ? "x8" : "X8");
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", escapeOptions.SurrogatePairEscapeLetterFallback, nameof(escapeOptions.SurrogatePairEscapeLetterFallback), escapeOptions.EscapeLanguage), nameof(escapeOptions));
                }
            }

            return "\\" + xu + suffix;
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

            string unescaped = string.Empty;
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
                case CharEscapeLanguage.Python:
                    // escaped char will have more than 1 char
                    // right now the max length of name in \N{name} is 88; min overall is 5: \N{x}
                    // non-N max is: \UHHHHHHHH
                    if ((s.Length > 1 && s.Length <= 10 && s.StartsWith("\\", StringComparison.Ordinal)) || (s.Length >= 5 && s.StartsWith("\\N{", StringComparison.Ordinal) && s.IndexOf('}') == s.Length - 1))
                    {
                        unescaped = StringUtils.Unescape(s, new StringUnescapeOptions(isUnrecognizedEscapeVerbatim: true), unescapeOptions);
                    }
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", unescapeOptions.EscapeLanguage, nameof(unescapeOptions.EscapeLanguage)), nameof(unescapeOptions));
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

            string unescaped = string.Empty;
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
                case CharEscapeLanguage.Python:
                    // escaped surrogate pairs look like this: \UHHHHHHHH, \N{name}
                    if ((s.Length == 10 && s.StartsWith("\\U", StringComparison.Ordinal)) || (s.Length >= 5 && s.StartsWith("\\N{", StringComparison.Ordinal) && s.IndexOf('}') == s.Length - 1))
                    {
                        unescaped = StringUtils.Unescape(s, new StringUnescapeOptions(isUnrecognizedEscapeVerbatim: true), unescapeOptions);
                    }
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", unescapeOptions.EscapeLanguage, nameof(unescapeOptions.EscapeLanguage)), nameof(unescapeOptions));
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
