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
        /// <param name="addQuotes">Add quotes after escaping</param>
        /// <returns>String with escape sequences for string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string Escape(string s, StringEscapeOptions stringEscapeOptions = null, CharEscapeOptions charEscapeOptions = null, bool addQuotes = false)
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

            switch (charEscapeOptions.EscapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                    return EscapeCSharp(s, stringEscapeOptions, charEscapeOptions, addQuotes);
                case CharEscapeLanguage.FSharp:
                    return EscapeFSharp(s, stringEscapeOptions, charEscapeOptions, addQuotes);
                case CharEscapeLanguage.PowerShell:
                    return EscapePowerShell(s, stringEscapeOptions, charEscapeOptions, addQuotes);
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", charEscapeOptions.EscapeLanguage, nameof(charEscapeOptions.EscapeLanguage)), nameof(charEscapeOptions));
            }
        }

        /// <summary>
        /// Escape string with backslash sequences (e.g. \r\n -> \\r\\n)
        /// </summary>
        /// <param name="s">String to escape</param>
        /// <param name="stringEscapeOptions">String escape options</param>
        /// <param name="charEscapeOptions"></param>
        /// <param name="addQuotes">Add quotes after escaping</param>
        /// <returns>String with escape sequences for string</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string EscapeCSharp(string s, StringEscapeOptions stringEscapeOptions, CharEscapeOptions charEscapeOptions, bool addQuotes)
        {
            CharEscapeOptions charEscapeOptionsLowerCaseX4 = new CharEscapeOptions(
                escapeLanguage: charEscapeOptions.EscapeLanguage,
                escapeLetter: CharEscapeLetter.LowerCaseX4,
                escapeLetterFallback: charEscapeOptions.EscapeLetterFallback,
                surrogatePairEscapeLetter: charEscapeOptions.SurrogatePairEscapeLetter,
                useLowerCaseHex: charEscapeOptions.UseLowerCaseHex,
                useShortEscape: charEscapeOptions.UseShortEscape
            );

            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (stringEscapeOptions.EscapeSurrogatePairs && i + 1 < s.Length && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    sb.Append(CharUtils.EscapeSurrogatePairCSharp(s[i], s[++i], charEscapeOptions));
                }
                else
                {
                    switch (stringEscapeOptions.EscapeKind)
                    {
                        case StringEscapeKind.EscapeAll:
                            sb.Append(CharUtils.EscapeCSharp(s[i], charEscapeOptions));
                            break;
                        case StringEscapeKind.EscapeNonAscii:
                            if (s[i].IsPrintAscii() && !s[i].IsBackslash() && !s[i].IsDoubleQuote())
                            {
                                sb.Append(s[i]);
                            }
                            else
                            {
                                // pay special attention here because \x is variable length: H, HH, HHH, HHHH
                                // if the next char is hex then we don't want to insert it in any of the 'H' spaces
                                // instead we have to output the full fixed length \xHHHH so the next char doesn't become part of this \x sequence
                                if ((charEscapeOptions.EscapeLetter == CharEscapeLetter.LowerCaseX1 || charEscapeOptions.EscapeLetter == CharEscapeLetter.LowerCaseX2 || charEscapeOptions.EscapeLetter == CharEscapeLetter.LowerCaseX3) && i + 1 < s.Length && s[i + 1].IsHex())
                                {
                                    sb.Append(CharUtils.EscapeCSharp(s[i], charEscapeOptionsLowerCaseX4));
                                }
                                else
                                {
                                    sb.Append(CharUtils.EscapeCSharp(s[i], charEscapeOptions));
                                }
                            }
                            break;
                        default:
                            throw new ArgumentException(string.Format("{0} is not a valid {1}.", stringEscapeOptions.EscapeKind, nameof(stringEscapeOptions.EscapeKind)), nameof(stringEscapeOptions));
                    }
                }
            }

            if (addQuotes)
            {
                sb.Insert(0, '\"');
                sb.Append('\"');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Escape string with backslash sequences (e.g. \r\n -> \\r\\n)
        /// </summary>
        /// <param name="s">String to escape</param>
        /// <param name="stringEscapeOptions">String escape options</param>
        /// <param name="charEscapeOptions">Char escape options</param>
        /// <param name="addQuotes">Add quotes after escaping</param>
        /// <returns>String with escape sequences for string</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string EscapeFSharp(string s, StringEscapeOptions stringEscapeOptions, CharEscapeOptions charEscapeOptions, bool addQuotes)
        {
            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (stringEscapeOptions.EscapeSurrogatePairs && i + 1 < s.Length && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    sb.Append(CharUtils.EscapeSurrogatePairFSharp(s[i], s[++i], charEscapeOptions));
                }
                else
                {
                    switch (stringEscapeOptions.EscapeKind)
                    {
                        case StringEscapeKind.EscapeAll:
                            sb.Append(CharUtils.EscapeFSharp(s[i], charEscapeOptions));
                            break;
                        case StringEscapeKind.EscapeNonAscii:
                            if (s[i].IsPrintAscii() && !s[i].IsBackslash() && !s[i].IsDoubleQuote())
                            {
                                sb.Append(s[i]);
                            }
                            else
                            {
                                sb.Append(CharUtils.EscapeFSharp(s[i], charEscapeOptions));
                            }
                            break;
                        default:
                            throw new ArgumentException(string.Format("{0} is not a valid {1}.", stringEscapeOptions.EscapeKind, nameof(stringEscapeOptions.EscapeKind)), nameof(stringEscapeOptions));
                    }
                }
            }

            if (addQuotes)
            {
                sb.Insert(0, '\"');
                sb.Append('\"');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Escape string with backslash sequences (e.g. \r\n -> `r`n)
        /// </summary>
        /// <param name="s">String to escape</param>
        /// <param name="stringEscapeOptions">String escape options</param>
        /// <param name="charEscapeOptions">Char escape options</param>
        /// <param name="addQuotes">Add quotes after escaping</param>
        /// <returns>String with escape sequences for string</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string EscapePowerShell(string s, StringEscapeOptions stringEscapeOptions, CharEscapeOptions charEscapeOptions, bool addQuotes)
        {
            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (stringEscapeOptions.EscapeSurrogatePairs && i + 1 < s.Length && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    sb.Append(CharUtils.EscapeSurrogatePairPowerShell(s[i], s[++i], charEscapeOptions));
                }
                else
                {
                    switch (stringEscapeOptions.EscapeKind)
                    {
                        case StringEscapeKind.EscapeAll:
                            sb.Append(CharUtils.EscapePowerShell(s[i], charEscapeOptions));
                            break;
                        case StringEscapeKind.EscapeNonAscii:
                            if (s[i].IsPrintAscii() && !s[i].IsBacktick() && !s[i].IsDoubleQuote() && !s[i].IsDollarSign())
                            {
                                sb.Append(s[i]);
                            }
                            else
                            {
                                sb.Append(CharUtils.EscapePowerShell(s[i], charEscapeOptions));
                            }
                            break;
                        default:
                            throw new ArgumentException(string.Format("{0} is not a valid {1}.", stringEscapeOptions.EscapeKind, nameof(stringEscapeOptions.EscapeKind)), nameof(stringEscapeOptions));
                    }
                }
            }

            if (addQuotes)
            {
                sb.Insert(0, '\"');
                sb.Append('\"');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Unescape backslash sequences to string (e.g. \\r\\n -> \r\n)
        /// </summary>
        /// <param name="s">String to unescape (e.g. s from "s" with the quotes removed)</param>
        /// <param name="unescapeOptions">Char unescape options</param>
        /// <param name="removeQuotes">Remove quotes before parsing</param>
        /// <returns>String that's been unescaped</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string Unescape(string s, CharUnescapeOptions unescapeOptions = null, bool removeQuotes = false)
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
                    return UnescapePowerShell(s, unescapeOptions, removeQuotes);
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", unescapeOptions.EscapeLanguage, nameof(unescapeOptions.EscapeLanguage)), nameof(unescapeOptions));
            }
        }

        /// <summary>
        /// Unescape backslash sequences to string (e.g. \\r\\n -> \r\n)
        /// </summary>
        /// <param name="s">String to unescape</param>
        /// <param name="unescapeOptions">Char unescape options</param>
        /// <param name="removeQuotes">Remove quotes before parsing</param>
        /// <returns>String that's been unescaped</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal static string UnescapeCSharp(string s, CharUnescapeOptions unescapeOptions, bool removeQuotes = false)
        {
            if (removeQuotes)
            {
                if (s.Length >= 2 && s[0].IsDoubleQuote() && s[s.Length - 1].IsDoubleQuote())
                {
                    s = s.Substring(1, s.Length - 2);
                }
                else
                {
                    throw new ArgumentException("String was not double quoted.", nameof(s));
                }
            }

            if (s.IndexOfAny(new char[] { '\\', '\"', '\r', '\n', '\x85', '\u2028', '\u2029' }) == -1)
            {
                return s;
            }
            else
            {
                // using indexOf('\\') and and substring() instead of iterating over each char can be faster if there's relatively few \\ in the string
                // however, if there's relatively more \\ in the string then iterating over each char can be faster
                // since this is an unescape function, let's assume there will be more \\ than less
                StringBuilder sb = new StringBuilder(s.Length);
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i].IsBackslash())
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
                                        UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
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
                                        UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
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
                                            string temp = ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 1], s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6], s[i + 7], s[i + 8] }), NumberStyles.AllowHexSpecifier), unescapeOptions.IsUnrecognizedEscapeVerbatim);
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
                                        UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
                                    }
                                    break;
                                default:
                                    UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
                                    break;
                            }
                        }
                        else
                        {
                            UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                        }
                    }
                    else if (s[i].IsDoubleQuote())
                    {
                        // can't have an unescaped " within ""
                        UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                    }
                    else if (s[i].IsCarriageReturn() || s[i].IsLineFeed() || s[i].IsNextLine() || s[i].IsLineSeparator() || s[i].IsParagraphSeparator())
                    {
                        // can't have all these unescaped newline types unescaped in a string
                        UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
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
        /// <param name="unescapeOptions">Char unescape options</param>
        /// <param name="removeQuotes">Remove quotes before parsing</param>
        /// <returns>String that's been unescaped</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal static string UnescapeFSharp(string s, CharUnescapeOptions unescapeOptions, bool removeQuotes = false)
        {
            if (removeQuotes)
            {
                if (s.Length >= 2 && s[0].IsDoubleQuote() && s[s.Length - 1].IsDoubleQuote())
                {
                    s = s.Substring(1, s.Length - 2);
                }
                else
                {
                    throw new ArgumentException("String was not double quoted.", nameof(s));
                }
            }

            if (s.IndexOfAny(new char[] { '\\', '\"' }) == -1)
            {
                return s;
            }
            else
            {
                StringBuilder sb = new StringBuilder(s.Length);
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i].IsBackslash())
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
                                case '\r':
                                    // \(newline) is allowed but consumed
                                    // newline can be: \r\n, \n
                                    if (i + 1 < s.Length && s[i + 1].IsLineFeed())
                                    {
                                        ++i;
                                    }
                                    else
                                    {
                                        // \\ + \r w/o \n is verbatim
                                        sb.Append('\\');
                                        sb.Append(s[i]);
                                    }
                                    break;
                                case '\n':
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
                                            string temp = ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 1], s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6], s[i + 7], s[i + 8] }), NumberStyles.AllowHexSpecifier), unescapeOptions.IsUnrecognizedEscapeVerbatim);
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
                            UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                        }
                    }
                    else if (s[i].IsDoubleQuote())
                    {
                        // can't have an unescaped " within ""
                        UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
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
        /// <param name="unescapeOptions">Char unescape options</param>
        /// <param name="removeQuotes">Remove quotes before parsing</param>
        /// <returns>String that's been unescaped</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static string UnescapePowerShell(string s, CharUnescapeOptions unescapeOptions, bool removeQuotes)
        {
            if (removeQuotes)
            {
                if (s.Length >= 2 && s[0].IsPowerShellDoubleQuote() && s[s.Length - 1].IsPowerShellDoubleQuote())
                {
                    s = s.Substring(1, s.Length - 2);
                }
                else
                {
                    throw new ArgumentException("String was not double quoted.", nameof(s));
                }
            }

            if (s.IndexOfAny(new char[] { '`', '\"', '\u201E', '\u201C', '\u201D', '$' }) == -1)
            {
                return s;
            }
            else
            {
                StringBuilder sb = new StringBuilder(s.Length);
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i].IsBacktick())
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
                                case '`':
                                    sb.Append('`');
                                    break;
                                case '\"':
                                    sb.Append('\"');
                                    break;
                                case '$':
                                    sb.Append('$');
                                    break;
                                case 'u':
                                    // 1 to 6 hex chars is supported between the curly braces
                                    // if something goes wrong here then powershell will throw an error
                                    if (i + 8 < s.Length && s[i + 1].IsLeftBrace() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsHex() && s[i + 6].IsHex() && s[i + 7].IsHex() && s[i + 8].IsRightBrace())
                                    {
                                        if (s[i + 2].IsZero() && s[i + 3].IsZero())
                                        {
                                            i += 3;
                                            sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                            ++i;
                                        }
                                        else
                                        {
                                            string temp = ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6], s[i + 7] }), NumberStyles.AllowHexSpecifier), unescapeOptions.IsUnrecognizedEscapeVerbatim);
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
                                    else if (i + 7 < s.Length && s[i + 1].IsLeftBrace() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsHex() && s[i + 6].IsHex() && s[i + 7].IsRightBrace())
                                    {
                                        if (s[i + 2].IsZero())
                                        {
                                            i += 2;
                                            sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                            ++i;
                                        }
                                        else
                                        {
                                            string temp = ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6] }), NumberStyles.AllowHexSpecifier), unescapeOptions.IsUnrecognizedEscapeVerbatim);
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
                                    else if (i + 6 < s.Length && s[i + 1].IsLeftBrace() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsHex() && s[i + 6].IsRightBrace())
                                    {
                                        ++i;
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                        ++i;
                                    }
                                    else if (i + 5 < s.Length && s[i + 1].IsLeftBrace() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsRightBrace())
                                    {
                                        ++i;
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                        ++i;
                                    }
                                    else if (i + 4 < s.Length && s[i + 1].IsLeftBrace() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsRightBrace())
                                    {
                                        ++i;
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                        ++i;
                                    }
                                    else if (i + 3 < s.Length && s[i + 1].IsLeftBrace() && s[i + 2].IsHex() && s[i + 3].IsRightBrace())
                                    {
                                        ++i;
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i] }), NumberStyles.AllowHexSpecifier));
                                        ++i;
                                    }
                                    else
                                    {
                                        UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '`', s[i] });
                                        break;
                                    }
                                    break;
                                default:
                                    // powershell effectively ignores the backtick character for escape sequences it doesn't recognize
                                    sb.Append(s[i]);
                                    break;
                            }
                        }
                        else
                        {
                            // powershell doesn't allow ` as the last char in the string, that ends up being `" which is a double-quote
                            UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                        }
                    }
                    else if (s[i].IsPowerShellDoubleQuote())
                    {
                        // this is supporting "string"; not @"here-string"@ which also support escape sequences and allow unescaped quotes
                        if (i + 1 < s.Length && s[i + 1].IsPowerShellDoubleQuote())
                        {
                            // powershell allows "" in addition to `" to represent a " inside ""
                            // the 2nd double-quote type is the one that's used
                            sb.Append(s[++i]);
                        }
                        else
                        {
                            // can't have a single unescaped " within ""
                            UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                        }
                    }
                    else if (s[i].IsDollarSign() && i + 1 < s.Length)
                    {
                        // https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_variables?view=powershell-7.1#variable-names-that-include-special-characters
                        UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(s[i + 1]);
                        if (category == UnicodeCategory.UppercaseLetter || category == UnicodeCategory.LowercaseLetter || category == UnicodeCategory.TitlecaseLetter ||
                            category == UnicodeCategory.ModifierLetter || category == UnicodeCategory.OtherLetter || category == UnicodeCategory.DecimalDigitNumber ||
                            s[i + 1].IsUnderscore() || s[i + 1].IsQuestionMark() || s[i + 1].IsDollarSign() || s[i + 1].IsCaret() || s[i + 1].IsLeftParenthesis() || s[i + 1].IsLeftBrace())
                        {
                            // strings in powershell support interpolation, we can't deal with that, so this is an error condition
                            UnrecognizedEscapeOrAppend(sb, unescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                        }
                        else
                        {
                            sb.Append(s[i]);
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
        /// Re-usable code
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="isUnrecognizedEscapeVerbatim">bool</param>
        /// <param name="paramName">string</param>
        /// <param name="chars">char[]</param>
        /// <exception cref="ArgumentException"></exception>
        private static void UnrecognizedEscapeOrAppend(StringBuilder sb, bool isUnrecognizedEscapeVerbatim, string paramName, char[] chars)
        {
            if (isUnrecognizedEscapeVerbatim)
            {
                foreach (char c in chars)
                {
                    sb.Append(c);
                }
            }
            else
            {
                throw new ArgumentException("Unrecognized escape sequence.", paramName);
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
