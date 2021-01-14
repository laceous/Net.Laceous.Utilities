using Net.Laceous.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Unicode;

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

            switch (charEscapeOptions.EscapeLanguage)
            {
                case CharEscapeLanguage.CSharp:
                    return EscapeCSharp(s, stringEscapeOptions, charEscapeOptions);
                case CharEscapeLanguage.FSharp:
                    return EscapeFSharp(s, stringEscapeOptions, charEscapeOptions);
                case CharEscapeLanguage.PowerShell:
                    return EscapePowerShell(s, stringEscapeOptions, charEscapeOptions);
                case CharEscapeLanguage.Python:
                    return EscapePython(s, stringEscapeOptions, charEscapeOptions);
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", charEscapeOptions.EscapeLanguage, nameof(charEscapeOptions.EscapeLanguage)), nameof(charEscapeOptions));
            }
        }

        private static string EscapeCSharp(string s, StringEscapeOptions stringEscapeOptions, CharEscapeOptions charEscapeOptions)
        {
            CharEscapeOptions charEscapeOptionsLowerCaseX4 = new CharEscapeOptions(
                escapeLanguage: charEscapeOptions.EscapeLanguage,
                escapeLetter: CharEscapeLetter.LowerCaseX4,
                escapeLetterFallback: charEscapeOptions.EscapeLetterFallback,
                surrogatePairEscapeLetter: charEscapeOptions.SurrogatePairEscapeLetter,
                surrogatePairEscapeLetterFallback: charEscapeOptions.SurrogatePairEscapeLetterFallback,
                useLowerCaseHex: charEscapeOptions.UseLowerCaseHex,
                useShortEscape: charEscapeOptions.UseShortEscape
            );

            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (stringEscapeOptions.EscapeSurrogatePairs && i + 1 < s.Length && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    sb.Append(CharUtils.EscapeSurrogatePair(s[i], s[++i], charEscapeOptions));
                }
                else
                {
                    switch (stringEscapeOptions.EscapeKind)
                    {
                        case StringEscapeKind.EscapeAll:
                            sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
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
                                    sb.Append(CharUtils.Escape(s[i], charEscapeOptionsLowerCaseX4));
                                }
                                else
                                {
                                    sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                                }
                            }
                            break;
                        default:
                            throw new ArgumentException(string.Format("{0} is not a valid {1}.", stringEscapeOptions.EscapeKind, nameof(stringEscapeOptions.EscapeKind)), nameof(stringEscapeOptions));
                    }
                }
            }

            if (stringEscapeOptions.AddQuotes)
            {
                switch (stringEscapeOptions.QuoteKind)
                {
                    case StringQuoteKind.DoubleQuote:
                        sb.Insert(0, '\"');
                        sb.Append('\"');
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringEscapeOptions.QuoteKind, nameof(stringEscapeOptions.QuoteKind), charEscapeOptions.EscapeLanguage), nameof(stringEscapeOptions));
                }
            }
            return sb.ToString();
        }

        private static string EscapeFSharp(string s, StringEscapeOptions stringEscapeOptions, CharEscapeOptions charEscapeOptions)
        {
            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (stringEscapeOptions.EscapeSurrogatePairs && i + 1 < s.Length && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    sb.Append(CharUtils.EscapeSurrogatePair(s[i], s[++i], charEscapeOptions));
                }
                else
                {
                    switch (stringEscapeOptions.EscapeKind)
                    {
                        case StringEscapeKind.EscapeAll:
                            sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                            break;
                        case StringEscapeKind.EscapeNonAscii:
                            if (s[i].IsPrintAscii() && !s[i].IsBackslash() && !s[i].IsDoubleQuote())
                            {
                                sb.Append(s[i]);
                            }
                            else
                            {
                                sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                            }
                            break;
                        default:
                            throw new ArgumentException(string.Format("{0} is not a valid {1}.", stringEscapeOptions.EscapeKind, nameof(stringEscapeOptions.EscapeKind)), nameof(stringEscapeOptions));
                    }
                }
            }

            if (stringEscapeOptions.AddQuotes)
            {
                switch (stringEscapeOptions.QuoteKind)
                {
                    case StringQuoteKind.DoubleQuote:
                        sb.Insert(0, '\"');
                        sb.Append('\"');
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringEscapeOptions.QuoteKind, nameof(stringEscapeOptions.QuoteKind), charEscapeOptions.EscapeLanguage), nameof(stringEscapeOptions));
                }
            }
            return sb.ToString();
        }

        private static string EscapePowerShell(string s, StringEscapeOptions stringEscapeOptions, CharEscapeOptions charEscapeOptions)
        {
            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (stringEscapeOptions.EscapeSurrogatePairs && i + 1 < s.Length && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    sb.Append(CharUtils.EscapeSurrogatePair(s[i], s[++i], charEscapeOptions));
                }
                else
                {
                    switch (stringEscapeOptions.EscapeKind)
                    {
                        case StringEscapeKind.EscapeAll:
                            sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                            break;
                        case StringEscapeKind.EscapeNonAscii:
                            if (s[i].IsPrintAscii() && !s[i].IsBacktick() && !s[i].IsDoubleQuote())
                            {
                                sb.Append(s[i]);
                            }
                            else
                            {
                                sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                            }
                            break;
                        default:
                            throw new ArgumentException(string.Format("{0} is not a valid {1}.", stringEscapeOptions.EscapeKind, nameof(stringEscapeOptions.EscapeKind)), nameof(stringEscapeOptions));
                    }
                }
            }

            if (stringEscapeOptions.AddQuotes)
            {
                switch (stringEscapeOptions.QuoteKind)
                {
                    case StringQuoteKind.DoubleQuote:
                        sb.Insert(0, '\"');
                        sb.Append('\"');
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringEscapeOptions.QuoteKind, nameof(stringEscapeOptions.QuoteKind), charEscapeOptions.EscapeLanguage), nameof(stringEscapeOptions));
                }
            }
            return sb.ToString();
        }

        private static string EscapePython(string s, StringEscapeOptions stringEscapeOptions, CharEscapeOptions charEscapeOptions)
        {
            CharEscapeOptions charEscapeOptionsNone3 = new CharEscapeOptions(
                escapeLanguage: charEscapeOptions.EscapeLanguage,
                escapeLetter: CharEscapeLetter.None3,
                escapeLetterFallback: charEscapeOptions.EscapeLetterFallback,
                surrogatePairEscapeLetter: charEscapeOptions.SurrogatePairEscapeLetter,
                surrogatePairEscapeLetterFallback: charEscapeOptions.SurrogatePairEscapeLetterFallback,
                useLowerCaseHex: charEscapeOptions.UseLowerCaseHex,
                useShortEscape: charEscapeOptions.UseShortEscape
            );

            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (stringEscapeOptions.EscapeSurrogatePairs && i + 1 < s.Length && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    sb.Append(CharUtils.EscapeSurrogatePair(s[i], s[++i], charEscapeOptions));
                }
                else
                {
                    switch (stringEscapeOptions.EscapeKind)
                    {
                        case StringEscapeKind.EscapeAll:
                            sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                            break;
                        case StringEscapeKind.EscapeNonAscii:
                            // go simple for now and escape all the quote chars
                            // that way you can copy the output and put it within any python string quote type: "s", 's', """s""", '''s'''
                            if (s[i].IsPrintAscii() && !s[i].IsBackslash() && !s[i].IsDoubleQuote() && !s[i].IsSingleQuote())
                            {
                                sb.Append(s[i]);
                            }
                            else
                            {
                                // workaround for variable length: \o, \oo, \ooo
                                if ((charEscapeOptions.EscapeLetter == CharEscapeLetter.None1 || charEscapeOptions.EscapeLetter == CharEscapeLetter.None2) && i + 1 < s.Length && s[i + 1].IsOctal())
                                {
                                    sb.Append(CharUtils.Escape(s[i], charEscapeOptionsNone3));
                                }
                                else
                                {
                                    sb.Append(CharUtils.Escape(s[i], charEscapeOptions));
                                }
                            }
                            break;
                        default:
                            throw new ArgumentException(string.Format("{0} is not a valid {1}.", stringEscapeOptions.EscapeKind, nameof(stringEscapeOptions.EscapeKind)), nameof(stringEscapeOptions));
                    }
                }
            }

            if (stringEscapeOptions.AddQuotes)
            {
                switch (stringEscapeOptions.QuoteKind)
                {
                    case StringQuoteKind.DoubleQuote:
                        sb.Insert(0, '\"');
                        sb.Append('\"');
                        break;
                    case StringQuoteKind.SingleQuote:
                        sb.Insert(0, '\'');
                        sb.Append('\'');
                        break;
                    case StringQuoteKind.TripleDoubleQuote:
                        sb.Insert(0, "\"\"\"");
                        sb.Append("\"\"\"");
                        break;
                    case StringQuoteKind.TripleSingleQuote:
                        sb.Insert(0, "\'\'\'");
                        sb.Append("\'\'\'");
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringEscapeOptions.QuoteKind, nameof(stringEscapeOptions.QuoteKind), charEscapeOptions.EscapeLanguage), nameof(stringEscapeOptions));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Unescape backslash sequences to string (e.g. \\r\\n -> \r\n)
        /// </summary>
        /// <param name="s">String to unescape (e.g. s from "s" with the quotes removed)</param>
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
                case CharEscapeLanguage.Python:
                    return UnescapePython(s, stringUnescapeOptions, charUnescapeOptions);
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid {1}.", charUnescapeOptions.EscapeLanguage, nameof(charUnescapeOptions.EscapeLanguage)), nameof(charUnescapeOptions));
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
            if (stringUnescapeOptions.RemoveQuotes)
            {
                switch (stringUnescapeOptions.QuoteKind)
                {
                    case StringQuoteKind.DoubleQuote:
                        if (s.Length >= 2 && s.StartsWith("\"", StringComparison.Ordinal) && s.EndsWith("\"", StringComparison.Ordinal))
                        {
                            s = s.Substring(1, s.Length - 2);
                        }
                        else
                        {
                            throw new ArgumentException("String was not double-quoted.", nameof(s));
                        }
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringUnescapeOptions.QuoteKind, nameof(stringUnescapeOptions.QuoteKind), charUnescapeOptions.EscapeLanguage), nameof(stringUnescapeOptions));
                }
            }

            // using indexOf('\\') and and substring() instead of iterating over each char can be faster if there's relatively few \\ in the string
            // however, if there's relatively more \\ in the string then iterating over each char can be faster
            // since this is an unescape function, let's assume there will be more \\ than less
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
                                        UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
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
                                        UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
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
                                        UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
                                    }
                                    break;
                                default:
                                    UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
                                    break;
                            }
                        }
                        else
                        {
                            UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                        }
                    }
                    else if (s[i].IsDoubleQuote())
                    {
                        switch (stringUnescapeOptions.QuoteKind)
                        {
                            case StringQuoteKind.DoubleQuote:
                                // can't have an unescaped " within ""
                                UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                                break;
                            default:
                                throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringUnescapeOptions.QuoteKind, nameof(stringUnescapeOptions.QuoteKind), charUnescapeOptions.EscapeLanguage), nameof(stringUnescapeOptions));
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
            if (stringUnescapeOptions.RemoveQuotes)
            {
                switch (stringUnescapeOptions.QuoteKind)
                {
                    case StringQuoteKind.DoubleQuote:
                        if (s.Length >= 2 && s.StartsWith("\"", StringComparison.Ordinal) && s.EndsWith("\"", StringComparison.Ordinal))
                        {
                            s = s.Substring(1, s.Length - 2);
                        }
                        else
                        {
                            throw new ArgumentException("String was not double-quoted.", nameof(s));
                        }
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringUnescapeOptions.QuoteKind, nameof(stringUnescapeOptions.QuoteKind), charUnescapeOptions.EscapeLanguage), nameof(stringUnescapeOptions));
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
                            UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                        }
                    }
                    else if (s[i].IsDoubleQuote())
                    {
                        switch (stringUnescapeOptions.QuoteKind)
                        {
                            case StringQuoteKind.DoubleQuote:
                                // can't have an unescaped " within ""
                                UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                                break;
                            default:
                                throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringUnescapeOptions.QuoteKind, nameof(stringUnescapeOptions.QuoteKind), charUnescapeOptions.EscapeLanguage), nameof(stringUnescapeOptions));
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
            if (stringUnescapeOptions.RemoveQuotes)
            {
                switch (stringUnescapeOptions.QuoteKind)
                {
                    case StringQuoteKind.DoubleQuote:
                        if (s.Length >= 2 && s.StartsWith("\"", StringComparison.Ordinal) && s.EndsWith("\"", StringComparison.Ordinal))
                        {
                            s = s.Substring(1, s.Length - 2);
                        }
                        else
                        {
                            throw new ArgumentException("String was not double-quoted.", nameof(s));
                        }
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringUnescapeOptions.QuoteKind, nameof(stringUnescapeOptions.QuoteKind), charUnescapeOptions.EscapeLanguage), nameof(stringUnescapeOptions));
                }
            }

            if (s.IndexOfAny(new char[] { '`', '\"' }) == -1)
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
                                        UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '`', s[i] });
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
                            UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                        }
                    }
                    else if (s[i].IsDoubleQuote())
                    {
                        switch (stringUnescapeOptions.QuoteKind)
                        {
                            case StringQuoteKind.DoubleQuote:
                                if (i + 1 < s.Length && s[i + 1].IsDoubleQuote())
                                {
                                    // powershell allows "" in addition to `" to represent a " inside ""
                                    sb.Append(s[++i]);
                                }
                                else
                                {
                                    // can't have a single unescaped " within ""
                                    UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                                }
                                break;
                            default:
                                throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringUnescapeOptions.QuoteKind, nameof(stringUnescapeOptions.QuoteKind), charUnescapeOptions.EscapeLanguage), nameof(stringUnescapeOptions));
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

        private static string UnescapePython(string s, StringUnescapeOptions stringUnescapeOptions, CharUnescapeOptions charUnescapeOptions)
        {
            if (stringUnescapeOptions.RemoveQuotes)
            {
                switch (stringUnescapeOptions.QuoteKind)
                {
                    case StringQuoteKind.DoubleQuote:
                        if (s.Length >= 2 && s.StartsWith("\"", StringComparison.Ordinal) && s.EndsWith("\"", StringComparison.Ordinal))
                        {
                            s = s.Substring(1, s.Length - 2);
                        }
                        else
                        {
                            throw new ArgumentException("String was not double-quoted.", nameof(s));
                        }
                        break;
                    case StringQuoteKind.SingleQuote:
                        if (s.Length >= 2 && s.StartsWith("\'", StringComparison.Ordinal) && s.EndsWith("\'", StringComparison.Ordinal))
                        {
                            s = s.Substring(1, s.Length - 2);
                        }
                        else
                        {
                            throw new ArgumentException("String was not single-quoted.", nameof(s));
                        }
                        break;
                    case StringQuoteKind.TripleDoubleQuote:
                        if (s.Length >= 6 && s.StartsWith("\"\"\"", StringComparison.Ordinal) && s.EndsWith("\"\"\"", StringComparison.Ordinal))
                        {
                            s = s.Substring(3, s.Length - 6);
                        }
                        else
                        {
                            throw new ArgumentException("String was not triple-double-quoted.", nameof(s));
                        }
                        break;
                    case StringQuoteKind.TripleSingleQuote:
                        if (s.Length >= 6 && s.StartsWith("\'\'\'", StringComparison.Ordinal) && s.EndsWith("\'\'\'", StringComparison.Ordinal))
                        {
                            s = s.Substring(3, s.Length - 6);
                        }
                        else
                        {
                            throw new ArgumentException("String was not triple-single-quoted.", nameof(s));
                        }
                        break;
                    default:
                        throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringUnescapeOptions.QuoteKind, nameof(stringUnescapeOptions.QuoteKind), charUnescapeOptions.EscapeLanguage), nameof(stringUnescapeOptions));
                }
            }

            if (s.IndexOfAny(new char[] { '\\', '\"', '\'' }) == -1)
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
                                case '\\':
                                    sb.Append('\\');
                                    break;
                                case '\'':
                                    sb.Append('\'');
                                    break;
                                case '\"':
                                    sb.Append('\"');
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
                                case '0':
                                    sb.Append('\0');
                                    break;
                                case 'u':
                                    if (i + 4 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                    }
                                    else
                                    {
                                        UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
                                    }
                                    break;
                                case 'x':
                                    if (i + 2 < s.Length && s[i + 1].IsHex() && s[i + 2].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i] }), NumberStyles.AllowHexSpecifier));
                                    }
                                    else
                                    {
                                        UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
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
                                        UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
                                    }
                                    break;
                                case 'N':
                                    {
                                        // \N{Latin Capital Letter A} -> A
                                        string temp = FindStringFromBracedName(s, ref i, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim);
                                        if (temp == null)
                                        {
                                            UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { '\\', s[i] });
                                        }
                                        else
                                        {
                                            sb.Append(temp);
                                        }
                                    }
                                    break;
                                default:
                                    // "" can support up to \777 unlike b"" which can only support up to \377 before rolling over
                                    if (i + 2 < s.Length && s[i].IsOctal() && s[i + 1].IsOctal() && s[i + 2].IsOctal())
                                    {
                                        sb.Append((char)Convert.ToInt32(new string(new char[] { s[i], s[++i], s[++i] }), 8));
                                    }
                                    else if (i + 1 < s.Length && s[i].IsOctal() && s[i + 1].IsOctal())
                                    {
                                        sb.Append((char)Convert.ToInt32(new string(new char[] { s[i], s[++i] }), 8));
                                    }
                                    else if (s[i].IsOctal())
                                    {
                                        sb.Append((char)Convert.ToInt32(new string(new char[] { s[i] }), 8));
                                    }
                                    else
                                    {
                                        // python treats this as verbatim
                                        sb.Append('\\');
                                        sb.Append(s[i]);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                        }
                    }
                    else if (s[i].IsDoubleQuote())
                    {
                        switch (stringUnescapeOptions.QuoteKind)
                        {
                            case StringQuoteKind.DoubleQuote:
                                // " within ""
                                UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                                break;
                            case StringQuoteKind.TripleDoubleQuote:
                                if (i + 2 < s.Length && s[i + 1].IsDoubleQuote() && s[i + 2].IsDoubleQuote())
                                {
                                    // can't have unescaped triple quotes within triple quotes
                                    UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                                }
                                else
                                {
                                    sb.Append(s[i]);
                                }
                                break;
                            case StringQuoteKind.SingleQuote:
                            case StringQuoteKind.TripleSingleQuote:
                                sb.Append(s[i]);
                                break;
                            default:
                                throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringUnescapeOptions.QuoteKind, nameof(stringUnescapeOptions.QuoteKind), charUnescapeOptions.EscapeLanguage), nameof(stringUnescapeOptions));
                        }
                    }
                    else if (s[i].IsSingleQuote())
                    {
                        switch (stringUnescapeOptions.QuoteKind)
                        {
                            case StringQuoteKind.SingleQuote:
                                // ' within ''
                                UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                                break;
                            case StringQuoteKind.TripleSingleQuote:
                                if (i + 2 < s.Length && s[i + 1].IsSingleQuote() && s[i + 2].IsSingleQuote())
                                {
                                    // can't have unescaped triple quotes within triple quotes
                                    UnrecognizedEscapeOrAppend(sb, stringUnescapeOptions.IsUnrecognizedEscapeVerbatim, nameof(s), new char[] { s[i] });
                                }
                                else
                                {
                                    sb.Append(s[i]);
                                }
                                break;
                            case StringQuoteKind.DoubleQuote:
                            case StringQuoteKind.TripleDoubleQuote:
                                sb.Append(s[i]);
                                break;
                            default:
                                throw new ArgumentException(string.Format("{0} is not a valid {1} for {2}.", stringUnescapeOptions.QuoteKind, nameof(stringUnescapeOptions.QuoteKind), charUnescapeOptions.EscapeLanguage), nameof(stringUnescapeOptions));
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
        /// <param name="sb"></param>
        /// <param name="isUnrecognizedEscapeVerbatim"></param>
        /// <param name="paramName"></param>
        /// <param name="chars"></param>
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

        /// <summary>
        /// Lookup for name -> codePoint because UnicodeInfo only has codePoint -> name
        /// </summary>
        private static Dictionary<string, int> NameToCodePointDictionary = null;

        /// <summary>
        /// Lazy initialize NameToCodePointDictionary
        /// </summary>
        private static void InitializeNameToCodePointDictionary()
        {
            if (NameToCodePointDictionary == null)
            {
                NameToCodePointDictionary = new Dictionary<string, int>();
                // for (int codePoint = 0; codePoint <= 0x10FFFF; codePoint++)
                foreach (UnicodeBlock block in UnicodeInfo.GetBlocks())
                {
                    foreach (int codePoint in block.CodePointRange)
                    {
                        UnicodeCharInfo charInfo = UnicodeInfo.GetCharInfo(codePoint);
                        if (!string.IsNullOrEmpty(charInfo.Name))
                        {
                            NameToCodePointDictionary[charInfo.Name.ToUpperInvariant()] = codePoint;
                        }
                        foreach (UnicodeNameAlias alias in charInfo.NameAliases)
                        {
                            if (!string.IsNullOrEmpty(alias.Name))
                            {
                                NameToCodePointDictionary[alias.Name.ToUpperInvariant()] = codePoint;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Pull out name from {name}, convert it to a codePoint, convert it to a string
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="i">String index</param>
        /// <returns>String for name</returns>
        private static string FindStringFromBracedName(string s, ref int i, bool isUnrecognizedEscapeVerbatim)
        {
            int j = 1;
            if (i + j < s.Length && s[i + j].IsLeftBrace())
            {
                bool foundRightBrace = false;
                StringBuilder sb = new StringBuilder();
                while (!foundRightBrace && i + j + 1 < s.Length)
                {
                    if (s[i + ++j].IsRightBrace())
                    {
                        foundRightBrace = true;
                    }
                    else
                    {
                        sb.Append(s[i + j]);
                    }
                }
                if (foundRightBrace)
                {
                    if (j >= 3) // {x}
                    {
                        InitializeNameToCodePointDictionary();
                        if (NameToCodePointDictionary.TryGetValue(sb.ToString().ToUpperInvariant(), out int codePoint))
                        {
                            if (codePoint >= char.MinValue && codePoint <= char.MaxValue)
                            {
                                i += j;
                                return ((char)codePoint).ToString();
                            }
                            else
                            {
                                string temp = ConvertFromUtf32(codePoint, isUnrecognizedEscapeVerbatim);
                                if (temp != null)
                                {
                                    i += j;
                                    return temp;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
