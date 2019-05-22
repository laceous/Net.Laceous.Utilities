using Net.Laceous.Utilities.Extensions;
using System;
using System.Collections.Generic;
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
                    sb.Append(CharUtils.EscapeSurrogatePair(s[i], s[++i], escapeOptions.UseLowerCaseHex));
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
        /// <param name="unrecognizedEscapeIsVerbatim">Treat unrecognized escape sequences as verbatim, otherwise throw an exception</param>
        /// <returns>String that's been unescaped</returns>
        public static string Unescape(string s, bool unrecognizedEscapeIsVerbatim = false)
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
                                        if (unrecognizedEscapeIsVerbatim)
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
                                        if (unrecognizedEscapeIsVerbatim)
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
                                            if (!unrecognizedEscapeIsVerbatim)
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
                                        if (unrecognizedEscapeIsVerbatim)
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
                                    if (unrecognizedEscapeIsVerbatim)
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
                            if (unrecognizedEscapeIsVerbatim)
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

        /// <summary>
        /// Checks if the string contains at least one surrogate pair
        /// </summary>
        /// <param name="s">String to check</param>
        /// <returns>True if at least one surrogate pair, otherwise false</returns>
        public static bool HasSurrogatePair(string s)
        {
            if (s == null)
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsHighSurrogate(s[i]) && s.Length > i + 1 && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Counts the number of chars in the string (each surrogate pair only increments the count by 1)
        /// </summary>
        /// <param name="s">String to count</param>
        /// <returns>Count</returns>
        public static int CountCompleteChars(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            int c = 0;
            int i = 0;
            for (; i < s.Length; c++, i++)
            {
                if (char.IsHighSurrogate(s[i]) && s.Length > i + 1 && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    i++;
                }
            }
            return c;
        }

        /// <summary>
        /// Gets the index for the first surrogate pair in the string
        /// </summary>
        /// <param name="s">String to search</param>
        /// <param name="startIndex">Where to start search from</param>
        /// <param name="count">Num of chars to look at</param>
        /// <returns>Index or -1 if not found</returns>
        public static int IndexOfSurrogatePair(string s, int startIndex = -1, int count = -1)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (startIndex < 0)
            {
                startIndex = 0;
            }
            if (count < 0)
            {
                count = s.Length;
            }
            
            int c = 0;
            int i = startIndex;
            for (; i < s.Length && c < count; c++, i++)
            {
                if (char.IsHighSurrogate(s[i]) && s.Length > i + 1 && char.IsSurrogatePair(s[i], s[i + 1]))
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
        public static int LastIndexOfSurrogatePair(string s, int startIndex = -1, int count = -1)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            
            if (startIndex < 0)
            {
                startIndex = s.Length - 1;
            }
            if (count < 0)
            {
                count = s.Length;
            }

            int c = 0;
            int i = startIndex;
            for (; i >= 0 && c < count; c++, i--)
            {
                if (char.IsLowSurrogate(s[i]) && i - 1 >= 0 && char.IsSurrogatePair(s[i - 1], s[i]))
                {
                    return --i;
                }
            }
            return -1;
        }
    }
}
