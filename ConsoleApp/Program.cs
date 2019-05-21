using Net.Laceous.Utilities;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeType = CharEscapeType.EscapeAllCharsExceptAscii,
                AlwaysUseUnicodeEscape = false,
                UseLowerCaseX = false,
                UseUpperCaseHex = true
            };

            char cOriginal = 'Ä';
            string cEscaped = CharUtils.Escape(cOriginal, escapeOptions: options);
            char cUnescaped = CharUtils.Unescape(cEscaped);
            Console.WriteLine(cOriginal);              // Ä
            Console.WriteLine("\'" + cEscaped + "\'"); // '\u00C4'
            Console.WriteLine(cUnescaped);             // Ä
            Console.WriteLine();

            string eOriginal = "😁"; // 2 char emoji
            string eEscaped1 = CharUtils.Escape(eOriginal[0], escapeOptions: options) + CharUtils.Escape(eOriginal[1], escapeOptions: options);
            string eEscaped2 = CharUtils.EscapeSurrogatePair(eOriginal, useUpperCaseHex: options.UseUpperCaseHex);
            string eUnescaped1 = CharUtils.UnescapeSurrogatePair(eEscaped1);
            string eUnescaped2 = CharUtils.UnescapeSurrogatePair(eEscaped2);
            Console.WriteLine(eOriginal);               // 😁
            Console.WriteLine("\"" + eEscaped1 + "\""); // "\uD83D\uDE01"
            Console.WriteLine("\"" + eEscaped2 + "\""); // "\U0001F601"
            Console.WriteLine(eUnescaped1);             // 😁
            Console.WriteLine(eUnescaped2);             // 😁
            Console.WriteLine();

            string sOriginal = "abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓";
            string sEscaped = StringUtils.Escape(sOriginal, escapeSurrogatePairs: true, escapeOptions: options);
            string sUnescaped = StringUtils.Unescape(sEscaped, unrecognizedEscapeIsVerbatim: false);
            Console.WriteLine(sOriginal);              // abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
            Console.WriteLine("\"" + sEscaped + "\""); // "abc ABC 123 \u00C4\u00D6\u00DC \u3131\u3134\u3137 \U0001F601\U0001F603\U0001F613"
            Console.WriteLine(sUnescaped);             // abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
            Console.WriteLine();

            int sLength = sOriginal.Length;                         // fast
            int sCount = StringUtils.CountCompleteChars(sOriginal); // slow
            Console.WriteLine(sLength); // 26 (num of chars in the string)
            Console.WriteLine(sCount);  // 23 (num of chars the human eye sees)
            Console.WriteLine();

            bool sHas1 = CharUtils.IsSurrogatePair(eOriginal);
            bool sHas2 = StringUtils.HasSurrogatePair(sOriginal);
            bool sHas3 = StringUtils.HasSurrogatePair(sEscaped);
            Console.WriteLine(sHas1); // True
            Console.WriteLine(sHas2); // True
            Console.WriteLine(sHas3); // False
            Console.WriteLine();

            int sIndex1 = StringUtils.IndexOfSurrogatePair(sOriginal);
            int sIndex2 = StringUtils.LastIndexOfSurrogatePair(sOriginal);
            Console.WriteLine(sIndex1); // 20
            Console.WriteLine(sIndex2); // 24
            Console.WriteLine();

            foreach (int i in StringUtils.AllIndexesOfSurrogatePairs(sOriginal))
            {
                string s = CharUtils.EscapeSurrogatePair(sOriginal, i, options.UseUpperCaseHex);
                Console.Write(s); // \U0001F601\U0001F603\U0001F613
            }
            Console.WriteLine();
        }
    }
}
