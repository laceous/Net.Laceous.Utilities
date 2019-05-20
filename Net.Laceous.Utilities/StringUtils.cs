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
        /// <param name="escapeSurrogatePairs">Escape surrogate pairs with \\Unnnnnnnn</param>
        /// <param name="escapeOptions">Escape options</param>
        /// <returns>String with escape sequences for string</returns>
        public static string Escape(string s, bool escapeSurrogatePairs = false, CharEscapeOptions escapeOptions = null)
        {
            if (s == null)
            {
                return null;
            }
            if (escapeOptions == null)
            {
                escapeOptions = new CharEscapeOptions();
            }

            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (escapeSurrogatePairs && char.IsHighSurrogate(s[i]) && s.Length > i + 1 && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    sb.Append(CharUtils.EscapeSurrogatePair(s[i], s[++i], escapeOptions.UseLowerCaseXInsteadOfLowerCaseU));
                }
                else
                {
                    sb.Append(CharUtils.Escape(s[i], escapeOptions));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Unescape backslash sequences to string (e.g. \\r\\n -> \r\n)
        /// </summary>
        /// <param name="s">String to unescape</param>
        /// <param name="treatUnrecognizedEscapeSequencesAsVerbatim">Treat unrecognized escape sequences as verbatim, otherwise throw an exception</param>
        /// <returns>String that's been unescaped</returns>
        public static string Unescape(string s, bool treatUnrecognizedEscapeSequencesAsVerbatim = false)
        {
            if (s == null)
            {
                return null;
            }

            // checking for the next \\ instead of iterating over each char can be faster if there's relatively few \\ in the string
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
                        if (s.Length > i + 1)
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
                                    if (s.Length > i + 4 && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.HexNumber));
                                    }
                                    else
                                    {
                                        if (treatUnrecognizedEscapeSequencesAsVerbatim)
                                        {
                                            sb.Append("\\" + s[i]);
                                        }
                                        else
                                        {
                                            throw new ArgumentException("Unrecognized escape sequence.", "s");
                                        }
                                    }
                                    break;
                                case 'x':
                                    if (s.Length > i + 4 && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i], s[++i] }), NumberStyles.HexNumber));
                                    }
                                    else if (s.Length > i + 3 && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i], s[++i] }), NumberStyles.HexNumber));
                                    }
                                    else if (s.Length > i + 2 && s[i + 1].IsHex() && s[i + 2].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i], s[++i] }), NumberStyles.HexNumber));
                                    }
                                    else if (s.Length > i + 1 && s[i + 1].IsHex())
                                    {
                                        sb.Append((char)int.Parse(new string(new char[] { s[++i] }), NumberStyles.HexNumber));
                                    }
                                    else
                                    {
                                        if (treatUnrecognizedEscapeSequencesAsVerbatim)
                                        {
                                            sb.Append("\\" + s[i]);
                                        }
                                        else
                                        {
                                            throw new ArgumentException("Unrecognized escape sequence.", "s");
                                        }
                                    }
                                    break;
                                case 'U':
                                    if (s.Length > i + 8 && s[i + 1].IsHex() && s[i + 2].IsHex() && s[i + 3].IsHex() && s[i + 4].IsHex() && s[i + 5].IsHex() && s[i + 6].IsHex() && s[i + 7].IsHex() && s[i + 8].IsHex())
                                    {
                                        // System.ArgumentOutOfRangeException: A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).
                                        string temp;
                                        try
                                        {
                                            temp = char.ConvertFromUtf32(int.Parse(new string(new char[] { s[i + 1], s[i + 2], s[i + 3], s[i + 4], s[i + 5], s[i + 6], s[i + 7], s[i + 8] }), NumberStyles.HexNumber));
                                        }
                                        catch (ArgumentOutOfRangeException)
                                        {
                                            if (!treatUnrecognizedEscapeSequencesAsVerbatim)
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
                                            sb.Append("\\" + s[i]);
                                        }
                                    }
                                    else
                                    {
                                        if (treatUnrecognizedEscapeSequencesAsVerbatim)
                                        {
                                            sb.Append("\\" + s[i]);
                                        }
                                        else
                                        {
                                            throw new ArgumentException("Unrecognized escape sequence.", "s");
                                        }
                                    }
                                    break;
                                default:
                                    if (treatUnrecognizedEscapeSequencesAsVerbatim)
                                    {
                                        sb.Append("\\" + s[i]);
                                    }
                                    else
                                    {
                                        throw new ArgumentException("Unrecognized escape sequence.", "s");
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            if (treatUnrecognizedEscapeSequencesAsVerbatim)
                            {
                                sb.Append("\\" + s[i]);
                            }
                            else
                            {
                                throw new ArgumentException("Unrecognized escape sequence.", "s");
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
    }
}
