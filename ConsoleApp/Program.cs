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
                AlwaysUseUnicodeEscapeSequence = false,
                UseLowerCaseHexInsteadOfUpperCaseHex = false,
                UseLowerCaseXInsteadOfLowerCaseU = false
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
            string eEscaped2 = CharUtils.EscapeSurrogatePair(eOriginal, useLowerCaseHexInsteadOfUpperCaseHex: options.UseLowerCaseHexInsteadOfUpperCaseHex);
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
            string sUnescaped = StringUtils.Unescape(sEscaped, treatUnrecognizedEscapeSequencesAsVerbatim: false);
            Console.WriteLine(sOriginal);              // abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
            Console.WriteLine("\"" + sEscaped + "\""); // "abc ABC 123 \u00C4\u00D6\u00DC \u3131\u3134\u3137 \U0001F601\U0001F603\U0001F613"
            Console.WriteLine(sUnescaped);             // abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
            Console.WriteLine();
        }
    }
}
