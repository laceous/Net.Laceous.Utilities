using System;
using Xunit;

namespace Net.Laceous.Utilities.Tests
{
    public class CharUtilsTests
    {
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
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
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
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseX1
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
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseX2
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
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseX3
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
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseX4
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
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.UpperCaseU8
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_CSharp_Decimal3()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.Decimal3
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
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseX1()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseX1
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
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseX2
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseX3()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseX3
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseX4()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseX4
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
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.UpperCaseU8
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
        public void EscapeTest_FSharp_Decimal3(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.Decimal3
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('\'', "\\'")]
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
                EscapeLanguage = EscapeLanguage.CSharp,
                UseShortEscape = true
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('\'', "\\'")]
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
                EscapeLanguage = EscapeLanguage.FSharp,
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
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4,
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
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4,
                UseLowerCaseHex = true
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeSurrogatePairTest()
        {
            string original = "😁"; // 2 char emoji
            string escaped1 = CharUtils.EscapeSurrogatePair(original[0], original[1], useLowerCaseHex: false);
            string escaped2 = CharUtils.EscapeSurrogatePair(original, useLowerCaseHex: true);
            Assert.Equal("\\U0001F601", escaped1);
            Assert.Equal("\\U0001f601", escaped2);
        }

        [Fact]
        public void EscapeSurrogatePairTest_Fail()
        {
            string original = "ab"; // not a surrogate pair

            Assert.Throws<ArgumentOutOfRangeException>(() => CharUtils.EscapeSurrogatePair(original[0], original[1], useLowerCaseHex: false));
            Assert.Throws<ArgumentException>(() => CharUtils.EscapeSurrogatePair(original, useLowerCaseHex: true));
        }

        [Theory]
        [InlineData("\\u0041", 'A')]
        [InlineData("\\t", '\t')]
        [InlineData("\\xC4", 'Ä')]
        [InlineData("\\x3131", 'ㄱ')]
        [InlineData("\\x020", ' ')]
        [InlineData("\\U000000A0", '\u00A0')]
        public void UnescapeTest_CSharp(string escaped, char unescaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp
            };

            Assert.Equal(unescaped, CharUtils.Unescape(escaped, options));
        }

        [Theory]
        [InlineData("a")]      // not an escaped char
        [InlineData("\\uBAD")] // not a valid escape
        public void UnescapeTest_CSharp_BadInput(string escaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp
            };

            Assert.Throws<ArgumentException>(() => CharUtils.Unescape(escaped, options));
        }

        [Theory]
        [InlineData("\\u0041", 'A')]
        [InlineData("\\t", '\t')]
        [InlineData("\\xC4", 'Ä')]
        [InlineData("\\u3131", 'ㄱ')]
        [InlineData("\\032", ' ')]
        [InlineData("\\U000000A0", '\u00A0')]
        public void UnescapeTest_FSharp(string escaped, char unescaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp
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
                EscapeLanguage = EscapeLanguage.FSharp
            };

            Assert.Equal(unescaped, CharUtils.Unescape(escaped, options));
        }

        [Theory]
        [InlineData("a")]      // not an escaped char
        [InlineData("\\uBAD")] // not a valid escape
        public void UnescapeTest_FSharp_BadInput(string escaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp
            };

            Assert.Throws<ArgumentException>(() => CharUtils.Unescape(escaped, options));
        }

        [Fact]
        public void UnescapeSurrogatePairTest()
        {
            string escaped = "\\U0001F601";

            string unescaped1 = CharUtils.UnescapeSurrogatePair(escaped);
            CharUtils.UnescapeSurrogatePair(escaped, out char highSurrogate, out char lowSurrogate);
            string unescaped2 = new string(new char[] { highSurrogate, lowSurrogate });

            Assert.Equal("😁", unescaped1);
            Assert.Equal("😁", unescaped2);
        }

        [Fact]
        public void UnescapeSurrogatePairTest_BadInput()
        {
            string escaped = "\\UBAD";

            Assert.Throws<ArgumentException>(() => CharUtils.UnescapeSurrogatePair(escaped));
            Assert.Throws<ArgumentException>(() => CharUtils.UnescapeSurrogatePair(escaped, out char highSurrogate, out char lowSurrogate));
        }

        [Fact]
        public void IsSurrogatePairTest()
        {
            string s1 = "😁";
            string s2 = "ab"; // not a surrogate pair

            Assert.True(CharUtils.IsSurrogatePair(s1));
            Assert.True(CharUtils.IsSurrogatePair(s1[0], s1[1]));
            Assert.False(CharUtils.IsSurrogatePair(s2));
            Assert.False(CharUtils.IsSurrogatePair(s2[0], s2[1]));
        }

        [Fact]
        public void ArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => CharUtils.EscapeSurrogatePair(null));
            Assert.Throws<ArgumentNullException>(() => CharUtils.IsSurrogatePair(null));
            Assert.Throws<ArgumentNullException>(() => CharUtils.Unescape(null));
            Assert.Throws<ArgumentNullException>(() => CharUtils.UnescapeSurrogatePair(null));
            Assert.Throws<ArgumentNullException>(() => CharUtils.UnescapeSurrogatePair(null, out char highSurrogate, out char lowSurrogate));
        }
    }
}
