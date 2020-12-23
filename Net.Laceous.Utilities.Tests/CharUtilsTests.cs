using System;
using Xunit;

namespace Net.Laceous.Utilities.Tests
{
    public class CharUtilsTests
    {
        [Fact]
        public void EscapeTest_CSharp_LowerCaseU4()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\u0041", escaped1);
            Assert.Equal("\\u0009", escaped2);
            Assert.Equal("\\u00C4", escaped3);
            Assert.Equal("\\u3131", escaped4);
            Assert.Equal("\\u0020", escaped5);
            Assert.Equal("\\u00A0", escaped6);
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseX1()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseX1
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\x41", escaped1);
            Assert.Equal("\\x9", escaped2);
            Assert.Equal("\\xC4", escaped3);
            Assert.Equal("\\x3131", escaped4);
            Assert.Equal("\\x20", escaped5);
            Assert.Equal("\\xA0", escaped6);
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseX2()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseX2
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\x41", escaped1);
            Assert.Equal("\\x09", escaped2);
            Assert.Equal("\\xC4", escaped3);
            Assert.Equal("\\x3131", escaped4);
            Assert.Equal("\\x20", escaped5);
            Assert.Equal("\\xA0", escaped6);
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseX3()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseX3
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\x041", escaped1);
            Assert.Equal("\\x009", escaped2);
            Assert.Equal("\\x0C4", escaped3);
            Assert.Equal("\\x3131", escaped4);
            Assert.Equal("\\x020", escaped5);
            Assert.Equal("\\x0A0", escaped6);
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseX4()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseX4
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\x0041", escaped1);
            Assert.Equal("\\x0009", escaped2);
            Assert.Equal("\\x00C4", escaped3);
            Assert.Equal("\\x3131", escaped4);
            Assert.Equal("\\x0020", escaped5);
            Assert.Equal("\\x00A0", escaped6);
        }

        [Fact]
        public void EscapeTest_CSharp_UpperCaseU8()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.UpperCaseU8
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\U00000041", escaped1);
            Assert.Equal("\\U00000009", escaped2);
            Assert.Equal("\\U000000C4", escaped3);
            Assert.Equal("\\U00003131", escaped4);
            Assert.Equal("\\U00000020", escaped5);
            Assert.Equal("\\U000000A0", escaped6);
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

        [Fact]
        public void EscapeTest_FSharp_LowerCaseU4()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\u0041", escaped1);
            Assert.Equal("\\u0009", escaped2);
            Assert.Equal("\\u00C4", escaped3);
            Assert.Equal("\\u3131", escaped4);
            Assert.Equal("\\u0020", escaped5);
            Assert.Equal("\\u00A0", escaped6);
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

        [Fact]
        public void EscapeTest_FSharp_LowerCaseX2()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseX2
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\x41", escaped1);
            Assert.Equal("\\x09", escaped2);
            Assert.Equal("\\xC4", escaped3);
            Assert.Equal("\\u3131", escaped4); // this won't fit into \xHH so it becomes \uHHHH
            Assert.Equal("\\x20", escaped5);
            Assert.Equal("\\xA0", escaped6);
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

        [Fact]
        public void EscapeTest_FSharp_UpperCaseU8()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.UpperCaseU8
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\U00000041", escaped1);
            Assert.Equal("\\U00000009", escaped2);
            Assert.Equal("\\U000000C4", escaped3);
            Assert.Equal("\\U00003131", escaped4);
            Assert.Equal("\\U00000020", escaped5);
            Assert.Equal("\\U000000A0", escaped6);
        }

        [Fact]
        public void EscapeTest_FSharp_Decimal3()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.Decimal3
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\065", escaped1);
            Assert.Equal("\\009", escaped2);
            Assert.Equal("\\196", escaped3);
            Assert.Equal("\\u3131", escaped4); // this won't fit into \DDD so it becomes \uHHHH
            Assert.Equal("\\032", escaped5);
            Assert.Equal("\\160", escaped6);
        }

        [Fact]
        public void EscapeTest_CSharp_UseShortEscape()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                UseShortEscape = true
            };

            char original1 = '\'';
            char original2 = '\"';
            char original3 = '\\';
            char original4 = '\0';
            char original5 = '\a';
            char original6 = '\b';
            char original7 = '\f';
            char original8 = '\n';
            char original9 = '\r';
            char original10 = '\t';
            char original11 = '\v';

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);
            string escaped7 = CharUtils.Escape(original7, options);
            string escaped8 = CharUtils.Escape(original8, options);
            string escaped9 = CharUtils.Escape(original9, options);
            string escaped10 = CharUtils.Escape(original10, options);
            string escaped11 = CharUtils.Escape(original11, options);

            Assert.Equal("\\'", escaped1);
            Assert.Equal("\\\"", escaped2);
            Assert.Equal("\\\\", escaped3);
            Assert.Equal("\\0", escaped4);
            Assert.Equal("\\a", escaped5);
            Assert.Equal("\\b", escaped6);
            Assert.Equal("\\f", escaped7);
            Assert.Equal("\\n", escaped8);
            Assert.Equal("\\r", escaped9);
            Assert.Equal("\\t", escaped10);
            Assert.Equal("\\v", escaped11);
        }

        [Fact]
        public void EscapeTest_FSharp_UseShortEscape()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                UseShortEscape = true
            };

            char original1 = '\'';
            char original2 = '\"';
            char original3 = '\\';
            char original4 = '\0';
            char original5 = '\a';
            char original6 = '\b';
            char original7 = '\f';
            char original8 = '\n';
            char original9 = '\r';
            char original10 = '\t';
            char original11 = '\v';

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);
            string escaped7 = CharUtils.Escape(original7, options);
            string escaped8 = CharUtils.Escape(original8, options);
            string escaped9 = CharUtils.Escape(original9, options);
            string escaped10 = CharUtils.Escape(original10, options);
            string escaped11 = CharUtils.Escape(original11, options);

            Assert.Equal("\\'", escaped1);
            Assert.Equal("\\\"", escaped2);
            Assert.Equal("\\\\", escaped3);
            Assert.Equal("\\u0000", escaped4); // fsharp doesn't define \0
            Assert.Equal("\\a", escaped5);
            Assert.Equal("\\b", escaped6);
            Assert.Equal("\\f", escaped7);
            Assert.Equal("\\n", escaped8);
            Assert.Equal("\\r", escaped9);
            Assert.Equal("\\t", escaped10);
            Assert.Equal("\\v", escaped11);
        }

        [Fact]
        public void EscapeTest_CSharp_UseLowerCaseHex()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4,
                UseLowerCaseHex = true
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\u0041", escaped1);
            Assert.Equal("\\u0009", escaped2);
            Assert.Equal("\\u00c4", escaped3);
            Assert.Equal("\\u3131", escaped4);
            Assert.Equal("\\u0020", escaped5);
            Assert.Equal("\\u00a0", escaped6);
        }

        [Fact]
        public void EscapeTest_FSharp_UseLowerCaseHex()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4,
                UseLowerCaseHex = true
            };

            char original1 = 'A';
            char original2 = '\t';
            char original3 = 'Ä';
            char original4 = 'ㄱ';
            char original5 = ' ';
            char original6 = '\u00A0'; // non-breaking space

            string escaped1 = CharUtils.Escape(original1, options);
            string escaped2 = CharUtils.Escape(original2, options);
            string escaped3 = CharUtils.Escape(original3, options);
            string escaped4 = CharUtils.Escape(original4, options);
            string escaped5 = CharUtils.Escape(original5, options);
            string escaped6 = CharUtils.Escape(original6, options);

            Assert.Equal("\\u0041", escaped1);
            Assert.Equal("\\u0009", escaped2);
            Assert.Equal("\\u00c4", escaped3);
            Assert.Equal("\\u3131", escaped4);
            Assert.Equal("\\u0020", escaped5);
            Assert.Equal("\\u00a0", escaped6);
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

        [Fact]
        public void UnescapeTest_CSharp()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp
            };

            string escaped1 = "\\u0041";
            string escaped2 = "\\t";
            string escaped3 = "\\xC4";
            string escaped4 = "\\x3131";
            string escaped5 = "\\x020";
            string escaped6 = "\\U000000A0";

            char unescaped1 = CharUtils.Unescape(escaped1, options);
            char unescaped2 = CharUtils.Unescape(escaped2, options);
            char unescaped3 = CharUtils.Unescape(escaped3, options);
            char unescaped4 = CharUtils.Unescape(escaped4, options);
            char unescaped5 = CharUtils.Unescape(escaped5, options);
            char unescaped6 = CharUtils.Unescape(escaped6, options);

            Assert.Equal('A', unescaped1);
            Assert.Equal('\t', unescaped2);
            Assert.Equal('Ä', unescaped3);
            Assert.Equal('ㄱ', unescaped4);
            Assert.Equal(' ', unescaped5);
            Assert.Equal('\u00A0', unescaped6); // non-breaking space
        }

        [Fact]
        public void UnescapeTest_CSharp_BadInput()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp
            };

            string escaped1 = "a";      // not an escaped char
            string escaped2 = "\\uBAD"; // not a valid escape

            Assert.Throws<ArgumentException>(() => CharUtils.Unescape(escaped1, options));
            Assert.Throws<ArgumentException>(() => CharUtils.Unescape(escaped2, options));
        }

        [Fact]
        public void UnescapeTest_FSharp()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp
            };

            string escaped1 = "\\u0041";
            string escaped2 = "\\t";
            string escaped3 = "\\xC4";
            string escaped4 = "\\u3131";
            string escaped5 = "\\032";
            string escaped6 = "\\U000000A0";

            char unescaped1 = CharUtils.Unescape(escaped1, options);
            char unescaped2 = CharUtils.Unescape(escaped2, options);
            char unescaped3 = CharUtils.Unescape(escaped3, options);
            char unescaped4 = CharUtils.Unescape(escaped4, options);
            char unescaped5 = CharUtils.Unescape(escaped5, options);
            char unescaped6 = CharUtils.Unescape(escaped6, options);

            Assert.Equal('A', unescaped1);
            Assert.Equal('\t', unescaped2);
            Assert.Equal('Ä', unescaped3);
            Assert.Equal('ㄱ', unescaped4);
            Assert.Equal(' ', unescaped5);
            Assert.Equal('\u00A0', unescaped6); // non-breaking space
        }

        [Fact]
        public void UnescapeTest_FSharp_GreaterThan255()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp
            };

            string escaped1 = "\\065"; // valid value (between 000-255)
            string escaped2 = "\\321"; // the rest of these work because the parser allows up to 999 when it parses out DDD
            string escaped3 = "\\577"; // the numbers roll over after 255
            string escaped4 = "\\833";

            char unescaped1 = CharUtils.Unescape(escaped1, options);
            char unescaped2 = CharUtils.Unescape(escaped2, options);
            char unescaped3 = CharUtils.Unescape(escaped3, options);
            char unescaped4 = CharUtils.Unescape(escaped4, options);

            Assert.Equal('A', unescaped1);
            Assert.Equal('A', unescaped2);
            Assert.Equal('A', unescaped3);
            Assert.Equal('A', unescaped4);
        }

        [Fact]
        public void UnescapeTest_FSharp_BadInput()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp
            };

            string escaped1 = "a";      // not an escaped char
            string escaped2 = "\\uBAD"; // not a valid escape

            Assert.Throws<ArgumentException>(() => CharUtils.Unescape(escaped1, options));
            Assert.Throws<ArgumentException>(() => CharUtils.Unescape(escaped2, options));
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
