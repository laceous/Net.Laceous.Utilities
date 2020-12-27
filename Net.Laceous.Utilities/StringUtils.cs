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

            CharEscapeOptions charEscapeOptionsLowerCaseX4 = new CharEscapeOptions(
                escapeLanguage: charEscapeOptions.EscapeLanguage,
                escapeLetter: CharEscapeLetter.LowerCaseX4,
                useLowerCaseHex: charEscapeOptions.UseLowerCaseHex,
                useShortEscape: charEscapeOptions.UseShortEscape
            );

            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (stringEscapeOptions.EscapeSurrogatePairs && char.IsHighSurrogate(s[i]) && i + 1 < s.Length && char.IsLowSurrogate(s[i + 1]))
                {
                    sb.Append(CharUtils.EscapeSurrogatePair(s[i], s[++i], charEscapeOptions));
                }
                else
                {
                    switch (stringEscapeOptions.EscapeType)
                    {
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
                        default: // StringEscapeType.EscapeAll
                            sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                            break;
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
                case CharEscapeLanguage.FSharp:
                    return UnescapeFSharp(s, stringUnescapeOptions, charUnescapeOptions);
                case CharEscapeLanguage.PowerShell:
                    return UnescapePowerShell(s, stringUnescapeOptions, charUnescapeOptions);
                default: // EscapeLanguage.CSharp
                    return UnescapeCSharp(s, stringUnescapeOptions, charUnescapeOptions);
            }
        }

        /// <summary>
        /// Unescape backslash sequences to string (e.g. \\r\\n -> \r\n)
        /// </summary>
        /// <param name="s">String to unescape</param>
        /// <param name="stringUnescapeOptions">String unescape options</param>
        /// <param name="charUnescapeOptions">Char unescape options</param>
        /// <returns>String that's been unescaped</returns>
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
                                            string temp;
                                            try
                                            {
                                                // System.ArgumentOutOfRangeException: A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).
                                                temp = char.ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 1], s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6], s[i + 7], s[i + 8] }), NumberStyles.AllowHexSpecifier));
                                            }
                                            catch (ArgumentOutOfRangeException)
                                            {
                                                if (!stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                                                {
                                                    throw;
                                                }
                                                temp = null;
                                            }
                                            if (temp != null)
                                            {
                                                i += 8;
                                                sb.Append(temp);
                                            }
                                            else
                                            {
                                                sb.Append('\\');
                                                sb.Append(s[i]);
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
                            break;
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
                                    if (i + 2 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
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
                                            string temp;
                                            try
                                            {
                                                // System.ArgumentOutOfRangeException: A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).
                                                temp = char.ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 1], s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6], s[i + 7], s[i + 8] }), NumberStyles.AllowHexSpecifier));
                                            }
                                            catch (ArgumentOutOfRangeException)
                                            {
                                                if (!stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                                                {
                                                    throw;
                                                }
                                                temp = null;
                                            }
                                            if (temp != null)
                                            {
                                                i += 8;
                                                sb.Append(temp);
                                            }
                                            else
                                            {
                                                sb.Append('\\');
                                                sb.Append(s[i]);
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
                            break;
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
                                case '\"':
                                    sb.Append('\"');
                                    break;
                                case '`':
                                    sb.Append('`');
                                    break;
                                case 'u':
                                    // 1 to 6 hex chars is supported between the curly braces
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
                                            string temp;
                                            try
                                            {
                                                // System.ArgumentOutOfRangeException: A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).
                                                temp = char.ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6], s[i + 7] }), NumberStyles.AllowHexSpecifier));
                                            }
                                            catch (ArgumentOutOfRangeException)
                                            {
                                                if (!stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                                                {
                                                    throw;
                                                }
                                                temp = null;
                                            }
                                            if (temp != null)
                                            {
                                                i += 8;
                                                sb.Append(temp);
                                            }
                                            else
                                            {
                                                sb.Append('`');
                                                sb.Append(s[i]);
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
                                            string temp;
                                            try
                                            {
                                                // System.ArgumentOutOfRangeException: A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).
                                                temp = char.ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6] }), NumberStyles.AllowHexSpecifier));
                                            }
                                            catch (ArgumentOutOfRangeException)
                                            {
                                                if (!stringUnescapeOptions.IsUnrecognizedEscapeVerbatim)
                                                {
                                                    throw;
                                                }
                                                temp = null;
                                            }
                                            if (temp != null)
                                            {
                                                i += 7;
                                                sb.Append(temp);
                                            }
                                            else
                                            {
                                                sb.Append('`');
                                                sb.Append(s[i]);
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
                            break;
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
        /// Checks if the string contains at least one surrogate pair
        /// </summary>
        /// <param name="s">String to check</param>
        /// <returns>True if at least one surrogate pair, otherwise false</returns>
        public static bool HasSurrogatePair(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return IndexOfSurrogatePair(s) != -1;
        }

        /// <summary>
        /// Gets the index for the first surrogate pair in the string
        /// </summary>
        /// <param name="s">String to search</param>
        /// <param name="startIndex">Where to start search from</param>
        /// <param name="count">Num of chars to look at</param>
        /// <returns>Index or -1 if not found</returns>
        public static int IndexOfSurrogatePair(string s, int? startIndex = null, int? count = null)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (startIndex == null)
            {
                startIndex = 0;
            }
            if (count == null)
            {
                count = s.Length - startIndex;
            }

            // > (rather than >=) allows for empty strings
            if (startIndex < 0 || startIndex > s.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Start index must be positive and less than the length of the string.");
            }
            if (count < 0 || startIndex + count > s.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be positive and must refer to a location within the string.");
            }

            int c = 0;
            int i = startIndex.Value;
            for (; c < count && i < s.Length; c++, i++)
            {
                if (c + 1 < count && char.IsHighSurrogate(s[i]) && i + 1 < s.Length && char.IsLowSurrogate(s[i + 1]))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Gets the index for the last surrogate pair in the string
        /// </summary>
        /// <param name="s">String to search</param>
        /// <param name="startIndex">Where to start search from</param>
        /// <param name="count">Num of chars to look at</param>
        /// <returns>Index or -1 if not found</returns>
        public static int LastIndexOfSurrogatePair(string s, int? startIndex = null, int? count = null)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (startIndex == null)
            {
                startIndex = s.Length == 0 ? 0 : s.Length - 1;
            }
            if (count == null)
            {
                count = s.Length == 0 ? 0 : startIndex + 1;
            }

            if (startIndex < 0 || startIndex > s.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Start index must be positive and less than the length of the string.");
            }
            if (count < 0 || (s.Length == 0 && count > 0) || startIndex - count < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be positive and must refer to a location within the string.");
            }

            int c = 0;
            int i = startIndex.Value;
            for (; c < count && i >= 0; c++, i--)
            {
                if (c + 1 < count && char.IsLowSurrogate(s[i]) && i - 1 >= 0 && char.IsHighSurrogate(s[i - 1]))
                {
                    return --i;
                }
            }
            return -1;
        }
    }
}
