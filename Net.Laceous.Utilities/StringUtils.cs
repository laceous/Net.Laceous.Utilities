using Net.Laceous.Utilities.Extensions;
using System;
using System.Globalization;
using System.Text;

namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Static methods related to strings
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Escape string with backslash sequences (e.g. \r\n -> \\r\\n)
        /// </summary>
        /// <param name="s">String to escape</param>
        /// <param name="stringEscapeOptions">String escape options</param>
        /// <param name="charEscapeOptions">Char escape options</param>
        /// <returns>String with escape sequences for string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string Escape(string s, StringEscapeOptions stringEscapeOptions = null, CharEscapeOptions charEscapeOptions = null)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (stringEscapeOptions == null)
            {
                stringEscapeOptions = new StringEscapeOptions();
            }
            if (charEscapeOptions == null)
            {
                charEscapeOptions = new CharEscapeOptions();
            }

            CharEscapeOptions charEscapeOptionsSurrogatePairs = new CharEscapeOptions(
                escapeLanguage: charEscapeOptions.EscapeLanguage,
                escapeLetter: stringEscapeOptions.EscapeLetterSurrogatePairs.HasValue ? stringEscapeOptions.EscapeLetterSurrogatePairs.Value : charEscapeOptions.EscapeLetter,
                escapeLetterFallback: charEscapeOptions.EscapeLetterFallback,
                useLowerCaseHex: charEscapeOptions.UseLowerCaseHex,
                useShortEscape: charEscapeOptions.UseShortEscape
            );

            CharEscapeOptions charEscapeOptionsLowerCaseX4 = new CharEscapeOptions(
                escapeLanguage: charEscapeOptions.EscapeLanguage,
                escapeLetter: CharEscapeLetter.LowerCaseX4,
                escapeLetterFallback: charEscapeOptions.EscapeLetterFallback,
                useLowerCaseHex: charEscapeOptions.UseLowerCaseHex,
                useShortEscape: charEscapeOptions.UseShortEscape
            );

            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (stringEscapeOptions.EscapeSurrogatePairs && char.IsHighSurrogate(s[i]) && i + 1 < s.Length && char.IsLowSurrogate(s[i + 1]))
                {
                    sb.Append(CharUtils.EscapeSurrogatePair(s[i], s[++i], charEscapeOptionsSurrogatePairs));
                }
                else
                {
                    switch (stringEscapeOptions.EscapeType)
                    {
                        case StringEscapeType.EscapeAll:
                            sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                            break;
                        case StringEscapeType.EscapeNonAscii:
                            if (s[i].IsQuotableAscii(charEscapeOptions.EscapeLanguage))
                            {
                                sb.Append(s[i]);
                            }
                            else
                            {
                                if (charEscapeOptions.EscapeLanguage == CharEscapeLanguage.CSharp)
                                {
                                    // pay special attention here because \x is variable length: H, HH, HHH, HHHH
                                    // if the next char is hex then we don't want to insert it in any of the 'H' spaces
                                    // instead we have to output the full fixed length \xHHHH so the next char doesn't become part of this \x sequence
                                    if ((charEscapeOptions.EscapeLetter == CharEscapeLetter.LowerCaseX1 || charEscapeOptions.EscapeLetter == CharEscapeLetter.LowerCaseX2 || charEscapeOptions.EscapeLetter == CharEscapeLetter.LowerCaseX3) && i + 1 < s.Length && s[i + 1].IsHex())
                                    {
                                        sb.Append(CharUtils.Escape(s[i], charEscapeOptionsLowerCaseX4));
                                    }
                                    else
                                    {
                                        sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                                    }
                                }
                                else // FSharp/PowerShell
                                {
                                    sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                                }
                            }
                            break;
                        default:
                            throw new ArgumentException(string.Format("{0} is not a valid EscapeType.", stringEscapeOptions.EscapeType), nameof(stringEscapeOptions));
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Unescape backslash sequences to string (e.g. \\r\\n -> \r\n)
        /// </summary>
        /// <param name="s">String to unescape</param>
        /// <param name="stringUnescapeOptions">String unescape options</param>
        /// <param name="charUnescapeOptions">Char unescape options</param>
        /// <returns>String that's been unescaped</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string Unescape(string s, StringUnescapeOptions stringUnescapeOptions = null, CharUnescapeOptions charUnescapeOptions = null)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (stringUnescapeOptions == null)
            {
                stringUnescapeOptions = new StringUnescapeOptions();
            }
            if (charUnescapeOptions == null)
            {
                charUnescapeOptions = new CharUnescapeOptions();
            }

            switch (charUnescapeOptions.EscapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                    return UnescapeCSharp(s, stringUnescapeOptions, charUnescapeOptions);
                case CharEscapeLanguage.FSharp:
                    return UnescapeFSharp(s, stringUnescapeOptions, charUnescapeOptions);
                case CharEscapeLanguage.PowerShell:
                    return UnescapePowerShell(s, stringUnescapeOptions, charUnescapeOptions);
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid EscapeLanguage.", charUnescapeOptions.EscapeLanguage), nameof(charUnescapeOptions));
            }
        }

        /// <summary>
        /// Unescape backslash sequences to string (e.g. \\r\\n -> \r\n)
        /// </summary>
        /// <param name="s">String to unescape</param>
        /// <param name="stringUnescapeOptions">String unescape options</param>
        /// <param name="charUnescapeOptions">Char unescape options</param>
        /// <returns>String that's been unescaped</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static string UnescapeCSharp(string s, StringUnescapeOptions stringUnescapeOptions, CharUnescapeOptions charUnescapeOptions)
        {
            // using indexOf('\\') and and substring() instead of iterating over each char can be faster if there's relatively few \\ in the string
            // however, if there's relatively more \\ in the string then iterating over each char can be faster
            // since this is an unescape function, let's assume there will be more \\ than less
            if (s.IndexOf('\\') == -1)
            {
                return s;
            }
            else
            {
                StringBuilder sb = new StringBuilder(s.Length);
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '\\')
                    {
                        if (i + 1 < s.Length)
                        {
                            switch (s[++i])
                            {
                                case '\'':
                                    sb.Append('\'');
                                    break;
                                case '\"':
                                    sb.Append('\"');
                                    break;
                                case '\\':
                                    sb.Append('\\');
                                    break;
                                case '0':
                                    sb.Append('\0');
                                    break;
                                case 'a':
                                    sb.Append('\a');
                                    break;
                                case 'b':
                                    sb.Append('\b');
                                    break;
                                case 'f':
                                    sb.Append('\f');
                                    break;
                                case 'n':
                                    sb.Append('\n');
                                    break;
                                case 'r':
                                    sb.Append('\r');
                                    break;
                                case 't':
                                    sb.Append('\t');
                                    break;
                                case 'v':
                                    sb.Append('\v');
                                    break;
                                case 'u':
                                    if (i + 4 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                    }
                                    else
                                    {
                                        if (stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                                        {
                                            sb.Append('\\');
                                            sb.Append(s[i]);
                                        }
                                        else
                                        {
                                            throw new ArgumentException("Unrecognized escape sequence.", nameof(s));
                                        }
                                    }
                                    break;
                                case 'x':
                                    if (i + 4 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                    }
                                    else if (i + 3 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                    }
                                    else if (i + 2 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                    }
                                    else if (i + 1 < s.Length && s[i + 1].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i] }), NumberStyles.AllowHexSpecifier));
                                    }
                                    else
                                    {
                                        if (stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                                        {
                                            sb.Append('\\');
                                            sb.Append(s[i]);
                                        }
                                        else
                                        {
                                            throw new ArgumentException("Unrecognized escape sequence.", nameof(s));
                                        }
                                    }
                                    break;
                                case 'U':
                                    if (i + 8 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsHex() && s[i + 6].IsHex() && s[i + 7].IsHex() && s[i + 8].IsHex())
                                    {
                                        if (s[i + 1].IsZero() && s[i + 2].IsZero() && s[i + 3].IsZero() && s[i + 4].IsZero())
                                        {
                                            // this lets us parse the surrogate codepoint values (0x00d800 ~ 0x00dfff) which we're already supporting for \u and \x
                                            i += 4;
                                            sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                        }
                                        else
                                        {
                                            // int.MaxValue.ToString("X") yields 7FFFFFFF
                                            // the largest value we could possibly need to parse here is FFFFFFFF which is too large so it becomes -1
                                            // char.ConvertFromUtf32 will only deal with the following:
                                            // "A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff)."
                                            // otherwise it throws an ArgumentOutOfRangeException which we check for
                                            string temp = ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 1], s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6], s[i + 7], s[i + 8] }), NumberStyles.AllowHexSpecifier), stringUnescapeOptions.IsUnrecognizedEscapeVerbatim);
                                            if (temp == null)
                                            {
                                                sb.Append('\\');
                                                sb.Append(s[i]);
                                            }
                                            else
                                            {
                                                i += 8;
                                                sb.Append(temp);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                                        {
                                            sb.Append('\\');
                                            sb.Append(s[i]);
                                        }
                                        else
                                        {
                                            throw new ArgumentException("Unrecognized escape sequence.", nameof(s));
                                        }
                                    }
                                    break;
                                default:
                                    if (stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                                    {
                                        sb.Append('\\');
                                        sb.Append(s[i]);
                                    }
                                    else
                                    {
                                        throw new ArgumentException("Unrecognized escape sequence.", nameof(s));
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            if (stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                            {
                                sb.Append(s[i]);
                            }
                            else
                            {
                                throw new ArgumentException("Unrecognized escape sequence.", nameof(s));
                            }
                        }
                    }
                    else
                    {
                        sb.Append(s[i]);
                    }
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Unescape backslash sequences to string (e.g. \\r\\n -> \r\n)
        /// </summary>
        /// <param name="s">String to unescape</param>
        /// <param name="stringUnescapeOptions">String unescape options</param>
        /// <param name="charUnescapeOptions">Char unescape options</param>
        /// <returns>String that's been unescaped</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static string UnescapeFSharp(string s, StringUnescapeOptions stringUnescapeOptions, CharUnescapeOptions charUnescapeOptions)
        {
            if (s.IndexOf('\\') == -1)
            {
                return s;
            }
            else
            {
                StringBuilder sb = new StringBuilder(s.Length);
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '\\')
                    {
                        if (i + 1 < s.Length)
                        {
                            switch (s[++i])
                            {
                                case 'a':
                                    sb.Append('\a');
                                    break;
                                case 'b':
                                    sb.Append('\b');
                                    break;
                                case 'f':
                                    sb.Append('\f');
                                    break;
                                case 'n':
                                    sb.Append('\n');
                                    break;
                                case 'r':
                                    sb.Append('\r');
                                    break;
                                case 't':
                                    sb.Append('\t');
                                    break;
                                case 'v':
                                    sb.Append('\v');
                                    break;
                                case '\\':
                                    sb.Append('\\');
                                    break;
                                case '\'':
                                    sb.Append('\'');
                                    break;
                                case '\"':
                                    sb.Append('\"');
                                    break;
                                case 'u':
                                    if (i + 4 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                    }
                                    else
                                    {
                                        // F# automatically consumes unrecognized escape sequences as verbatim
                                        sb.Append('\\');
                                        sb.Append(s[i]);
                                    }
                                    break;
                                case 'x':
                                    if (i + 2 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                    }
                                    else
                                    {
                                        sb.Append('\\');
                                        sb.Append(s[i]);
                                    }
                                    break;
                                case 'U':
                                    if (i + 8 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsHex() && s[i + 6].IsHex() && s[i + 7].IsHex() && s[i + 8].IsHex())
                                    {
                                        if (s[i + 1].IsZero() && s[i + 2].IsZero() && s[i + 3].IsZero() && s[i + 4].IsZero())
                                        {
                                            i += 4;
                                            sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                        }
                                        else
                                        {
                                            string temp = ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 1], s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6], s[i + 7], s[i + 8] }), NumberStyles.AllowHexSpecifier), stringUnescapeOptions.IsUnrecognizedEscapeVerbatim);
                                            if (temp == null)
                                            {
                                                sb.Append('\\');
                                                sb.Append(s[i]);
                                            }
                                            else
                                            {
                                                i += 8;
                                                sb.Append(temp);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        sb.Append('\\');
                                        sb.Append(s[i]);
                                    }
                                    break;
                                default:
                                    if (i + 2 < s.Length && s[i].IsDecimal() && s[i + 1].IsDecimal() && s[i + 2].IsDecimal())
                                    {
                                        // the docs say 000-255 are allowed
                                        // in practice, it appears that you can go up to 999 but it rolls over after 255
                                        // e.g. 256 == 000, 257 == 001, etc
                                        int temp = int.Parse(new string(new char[] { s[i], s[++i], s[++i] }), NumberStyles.None);
                                        while (temp > 255)
                                        {
                                            temp -= 256; // 255 + 0th place == 256 total
                                        }
                                        sb.Append((char)temp);
                                    }
                                    else
                                    {
                                        sb.Append('\\');
                                        sb.Append(s[i]);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            // F# doesn't allow \ as the last char in the string, that ends up being \" which is a double-quote
                            if (stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                            {
                                sb.Append(s[i]);
                            }
                            else
                            {
                                throw new ArgumentException("Unrecognized escape sequence.", nameof(s));
                            }
                        }
                    }
                    else
                    {
                        sb.Append(s[i]);
                    }
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Unescape backslash sequences to string (e.g. `r`n -> \r\n)
        /// </summary>
        /// <param name="s">String to unescape</param>
        /// <param name="stringUnescapeOptions">String unescape options</param>
        /// <param name="charUnescapeOptions">Char unescape options</param>
        /// <returns>String that's been unescaped</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static string UnescapePowerShell(string s, StringUnescapeOptions stringUnescapeOptions, CharUnescapeOptions charUnescapeOptions)
        {
            if (s.IndexOf('`') == -1)
            {
                return s;
            }
            else
            {
                StringBuilder sb = new StringBuilder(s.Length);
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '`')
                    {
                        if (i + 1 < s.Length)
                        {
                            switch (s[++i])
                            {
                                case '0':
                                    sb.Append('\0');
                                    break;
                                case 'a':
                                    sb.Append('\a');
                                    break;
                                case 'b':
                                    sb.Append('\b');
                                    break;
                                case 'e':
                                    sb.Append('\x1B');
                                    break;
                                case 'f':
                                    sb.Append('\f');
                                    break;
                                case 'n':
                                    sb.Append('\n');
                                    break;
                                case 'r':
                                    sb.Append('\r');
                                    break;
                                case 't':
                                    sb.Append('\t');
                                    break;
                                case 'v':
                                    sb.Append('\v');
                                    break;
                                case 'u':
                                    // 1 to 6 hex chars is supported between the curly braces
                                    // if something goes wrong here then powershell will throw an error
                                    if (i + 8 < s.Length && s[i + 1].IsOpeningCurlyBrace() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsHex() && s[i + 6].IsHex() && s[i + 7].IsHex() && s[i + 8].IsClosingCurlyBrace())
                                    {
                                        if (s[i + 2].IsZero() && s[i + 3].IsZero())
                                        {
                                            i += 3;
                                            sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                            ++i;
                                        }
                                        else
                                        {
                                            string temp = ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6], s[i + 7] }), NumberStyles.AllowHexSpecifier), stringUnescapeOptions.IsUnrecognizedEscapeVerbatim);
                                            if (temp == null)
                                            {
                                                sb.Append('`');
                                                sb.Append(s[i]);
                                            }
                                            else
                                            {
                                                i += 8;
                                                sb.Append(temp);
                                            }
                                        }
                                    }
                                    else if (i + 7 < s.Length && s[i + 1].IsOpeningCurlyBrace() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsHex() && s[i + 6].IsHex() && s[i + 7].IsClosingCurlyBrace())
                                    {
                                        if (s[i + 2].IsZero())
                                        {
                                            i += 2;
                                            sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                            ++i;
                                        }
                                        else
                                        {
                                            string temp = ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6] }), NumberStyles.AllowHexSpecifier), stringUnescapeOptions.IsUnrecognizedEscapeVerbatim);
                                            if (temp == null)
                                            {
                                                sb.Append('`');
                                                sb.Append(s[i]);
                                            }
                                            else
                                            {
                                                i += 7;
                                                sb.Append(temp);
                                            }
                                        }
                                    }
                                    else if (i + 6 < s.Length && s[i + 1].IsOpeningCurlyBrace() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsHex() && s[i + 6].IsClosingCurlyBrace())
                                    {
                                        ++i;
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                        ++i;
                                    }
                                    else if (i + 5 < s.Length && s[i + 1].IsOpeningCurlyBrace() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsClosingCurlyBrace())
                                    {
                                        ++i;
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                        ++i;
                                    }
                                    else if (i + 4 < s.Length && s[i + 1].IsOpeningCurlyBrace() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsClosingCurlyBrace())
                                    {
                                        ++i;
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                        ++i;
                                    }
                                    else if (i + 3 < s.Length && s[i + 1].IsOpeningCurlyBrace() && s[i + 2].IsHex() && s[i + 3].IsClosingCurlyBrace())
                                    {
                                        ++i;
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i] }), NumberStyles.AllowHexSpecifier));
                                        ++i;
                                    }
                                    else
                                    {
                                        if (stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                                        {
                                            sb.Append('`');
                                            sb.Append(s[i]);
                                        }
                                        else
                                        {
                                            throw new ArgumentException("Unrecognized escape sequence.", nameof(s));
                                        }
                                        break;
                                    }
                                    break;
                                default:
                                    // powershell effectively ignores the backtick character for escape sequences it doesn't recognize
                                    //sb.Append('`');
                                    sb.Append(s[i]);
                                    break;
                            }
                        }
                        else
                        {
                            // powershell doesn't allow ` as the last char in the string, that ends up being `" which is a double-quote
                            if (stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                            {
                                sb.Append(s[i]);
                            }
                            else
                            {
                                throw new ArgumentException("Unrecognized escape sequence.", nameof(s));
                            }
                        }
                    }
                    else
                    {
                        sb.Append(s[i]);
                    }
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Wrapper for char.ConvertFromUtf32
        /// </summary>
        /// <param name="utf32">Hex to int</param>
        /// <param name="isUnrecognizedEscapeVerbatim">Whether to throw or not</param>
        /// <returns>utf32 converted to string</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static string ConvertFromUtf32(int utf32, bool isUnrecognizedEscapeVerbatim)
        {
            try
            {
                // System.ArgumentOutOfRangeException: A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).
                return char.ConvertFromUtf32(utf32);
            }
            catch (ArgumentOutOfRangeException)
            {
                if (!isUnrecognizedEscapeVerbatim)
                {
                    throw;
                }
                return null;
            }
        }
    }
}
