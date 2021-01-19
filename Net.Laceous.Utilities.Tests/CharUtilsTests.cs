using System;
using Xunit;

namespace Net.Laceous.Utilities.Tests
{
    public class CharUtilsTests
    {
        [Fact]
        public void EscapeTest_CSharp_LowerCaseU1()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU1
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseU2()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU2
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseU3()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU3
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "\\u0041")]
        [InlineData('\t', "\\u0009")]
        [InlineData('Ä', "\\u00C4")]
        [InlineData('ㄱ', "\\u3131")]
        [InlineData(' ', "\\u0020")]
        [InlineData('\u00A0', "\\u00A0")]
        public void EscapeTest_CSharp_LowerCaseU4(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU4
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseU5()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU5
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseU6()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU6
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "\\x41")]
        [InlineData('\t', "\\x9")]
        [InlineData('Ä', "\\xC4")]
        [InlineData('ㄱ', "\\x3131")]
        [InlineData(' ', "\\x20")]
        [InlineData('\u00A0', "\\xA0")]
        public void EscapeTest_CSharp_LowerCaseX1(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX1
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "\\x41")]
        [InlineData('\t', "\\x09")]
        [InlineData('Ä', "\\xC4")]
        [InlineData('ㄱ', "\\x3131")]
        [InlineData(' ', "\\x20")]
        [InlineData('\u00A0', "\\xA0")]
        public void EscapeTest_CSharp_LowerCaseX2(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX2
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "\\x041")]
        [InlineData('\t', "\\x009")]
        [InlineData('Ä', "\\x0C4")]
        [InlineData('ㄱ', "\\x3131")]
        [InlineData(' ', "\\x020")]
        [InlineData('\u00A0', "\\x0A0")]
        public void EscapeTest_CSharp_LowerCaseX3(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX3
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "\\x0041")]
        [InlineData('\t', "\\x0009")]
        [InlineData('Ä', "\\x00C4")]
        [InlineData('ㄱ', "\\x3131")]
        [InlineData(' ', "\\x0020")]
        [InlineData('\u00A0', "\\x00A0")]
        public void EscapeTest_CSharp_LowerCaseX4(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX4
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "\\U00000041")]
        [InlineData('\t', "\\U00000009")]
        [InlineData('Ä', "\\U000000C4")]
        [InlineData('ㄱ', "\\U00003131")]
        [InlineData(' ', "\\U00000020")]
        [InlineData('\u00A0', "\\U000000A0")]
        public void EscapeTest_CSharp_UpperCaseU8(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.UpperCaseU8
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_CSharp_None3()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.None3
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_CSharp_AddQuotes()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp
            };

            char original = 'A';

            Assert.Equal("\'\\u0041\'", CharUtils.Escape(original, options, true));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseU1()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU1
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseU2()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU2
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseU3()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU3
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "\\u0041")]
        [InlineData('\t', "\\u0009")]
        [InlineData('Ä', "\\u00C4")]
        [InlineData('ㄱ', "\\u3131")]
        [InlineData(' ', "\\u0020")]
        [InlineData('\u00A0', "\\u00A0")]
        public void EscapeTest_FSharp_LowerCaseU4(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU4
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseU5()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU5
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseU6()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU6
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseX1()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX1
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "\\x41")]
        [InlineData('\t', "\\x09")]
        [InlineData('Ä', "\\xC4")]
        [InlineData('ㄱ', "\\u3131")] // this won't fit into \xHH so it becomes \uHHHH
        [InlineData(' ', "\\x20")]
        [InlineData('\u00A0', "\\xA0")]
        public void EscapeTest_FSharp_LowerCaseX2(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX2
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseX2_Fallback()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX2
            };

            char original = 'ㄱ';

            options.EscapeLetterFallback = CharEscapeLetter.LowerCaseU4;
            Assert.Equal("\\u3131", CharUtils.Escape(original, options));

            options.EscapeLetterFallback = CharEscapeLetter.UpperCaseU8;
            Assert.Equal("\\U00003131", CharUtils.Escape(original, options));

            options.EscapeLetterFallback = CharEscapeLetter.LowerCaseX2; // bad
            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseX3()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX3
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseX4()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX4
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "\\U00000041")]
        [InlineData('\t', "\\U00000009")]
        [InlineData('Ä', "\\U000000C4")]
        [InlineData('ㄱ', "\\U00003131")]
        [InlineData(' ', "\\U00000020")]
        [InlineData('\u00A0', "\\U000000A0")]
        public void EscapeTest_FSharp_UpperCaseU8(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.UpperCaseU8
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "\\065")]
        [InlineData('\t', "\\009")]
        [InlineData('Ä', "\\196")]
        [InlineData('ㄱ', "\\u3131")] // this won't fit into \DDD so it becomes \uHHHH
        [InlineData(' ', "\\032")]
        [InlineData('\u00A0', "\\160")]
        public void EscapeTest_FSharp_None3(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.None3
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_None3_Fallback()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.None3
            };

            char original = 'ㄱ';

            options.EscapeLetterFallback = CharEscapeLetter.LowerCaseU4;
            Assert.Equal("\\u3131", CharUtils.Escape(original, options));

            options.EscapeLetterFallback = CharEscapeLetter.UpperCaseU8;
            Assert.Equal("\\U00003131", CharUtils.Escape(original, options));

            options.EscapeLetterFallback = CharEscapeLetter.None3; // bad
            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_AddQuotes()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp
            };

            char original = 'A';

            Assert.Equal("\'\\u0041\'", CharUtils.Escape(original, options, true));
        }

        [Fact]
        public void EscapeTest_PowerShell()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('\'', "\\\'")]
        [InlineData('\"', "\\\"")]
        [InlineData('\\', "\\\\")]
        [InlineData('\0', "\\0")]
        [InlineData('\a', "\\a")]
        [InlineData('\b', "\\b")]
        [InlineData('\f', "\\f")]
        [InlineData('\n', "\\n")]
        [InlineData('\r', "\\r")]
        [InlineData('\t', "\\t")]
        [InlineData('\v', "\\v")]
        public void EscapeTest_CSharp_UseShortEscape(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                UseShortEscape = true
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('\'', "\\\'")]
        [InlineData('\"', "\\\"")]
        [InlineData('\\', "\\\\")]
        [InlineData('\0', "\\u0000")] // fsharp doesn't define \0
        [InlineData('\a', "\\a")]
        [InlineData('\b', "\\b")]
        [InlineData('\f', "\\f")]
        [InlineData('\n', "\\n")]
        [InlineData('\r', "\\r")]
        [InlineData('\t', "\\t")]
        [InlineData('\v', "\\v")]
        public void EscapeTest_FSharp_UseShortEscape(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                UseShortEscape = true
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('Ä', "\\u00c4")]
        [InlineData('\u00A0', "\\u00a0")]
        public void EscapeTest_CSharp_UseLowerCaseHex(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU4,
                UseLowerCaseHex = true
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('Ä', "\\u00c4")]
        [InlineData('\u00A0', "\\u00a0")]
        public void EscapeTest_FSharp_UseLowerCaseHex(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU4,
                UseLowerCaseHex = true
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData("\\u0041", 'A')]
        [InlineData("\\t", '\t')]
        [InlineData("\\xC4", 'Ä')]
        [InlineData("\\x3131", 'ㄱ')]
        [InlineData("\\x020", ' ')]
        [InlineData("\\U000000A0", '\u00A0')]
        [InlineData("\\\"", '\"')]
        [InlineData("\"", '\"')]
        public void UnescapeTest_CSharp(string escaped, char unescaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp
            };

            Assert.Equal(unescaped, CharUtils.Unescape(escaped, options));
        }

        [Theory]
        [InlineData("\\uBAD")] // not a valid escape
        public void UnescapeTest_CSharp_BadInput(string escaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp
            };

            Assert.Throws<ArgumentException>(() => CharUtils.Unescape(escaped, options));
        }

        [Fact]
        public void UnescapeTest_CSharp_RemoveQuotes()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp
            };

            string escaped = "\'\\u0041\'";

            Assert.Equal('A', CharUtils.Unescape(escaped, options, true));
        }

        [Theory]
        [InlineData("\\u0041", 'A')]
        [InlineData("\\t", '\t')]
        [InlineData("\\xC4", 'Ä')]
        [InlineData("\\u3131", 'ㄱ')]
        [InlineData("\\032", ' ')]
        [InlineData("\\U000000A0", '\u00A0')]
        [InlineData("\\\"", '\"')]
        [InlineData("\"", '\"')]
        public void UnescapeTest_FSharp(string escaped, char unescaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp
            };

            Assert.Equal(unescaped, CharUtils.Unescape(escaped, options));
        }

        [Theory]
        [InlineData("\\065", 'A')] // valid value (between 000-255)
        [InlineData("\\321", 'A')] // the rest of these work because the parser allows up to 999 when it parses out DDD
        [InlineData("\\577", 'A')] // the numbers roll over after 255
        [InlineData("\\833", 'A')]
        public void UnescapeTest_FSharp_GreaterThan255(string escaped, char unescaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp
            };

            Assert.Equal(unescaped, CharUtils.Unescape(escaped, options));
        }

        [Theory]
        [InlineData("\\uBAD")] // not a valid escape
        public void UnescapeTest_FSharp_BadInput(string escaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp
            };

            Assert.Throws<ArgumentException>(() => CharUtils.Unescape(escaped, options));
        }

        [Fact]
        public void UnescapeTest_FSharp_RemoveQuotes()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp
            };

            string escaped = "\'\\u0041\'";

            Assert.Equal('A', CharUtils.Unescape(escaped, options, true));
        }

        [Fact]
        public void UnescapeTest_PowerShell()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell
            };

            string escaped = "`n";

            Assert.Throws<ArgumentException>(() => CharUtils.Unescape(escaped, options));
        }

        [Fact]
        public void ArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => CharUtils.Unescape(null));
        }
    }
}
