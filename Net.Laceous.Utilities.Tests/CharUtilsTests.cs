using Xunit;

namespace Net.Laceous.Utilities.Tests
{
    public class CharUtilsTests
    {
        [Fact]
        public void EscapeTest_EscapeType_Default()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeType = CharEscapeType.Default
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

            Assert.Equal("A", escaped1);
            Assert.Equal("\\t", escaped2);
            Assert.Equal("Ä", escaped3);
            Assert.Equal("ㄱ", escaped4);
            Assert.Equal(" ", escaped5);
            Assert.Equal("\\u00A0", escaped6);
        }

        [Fact]
        public void EscapeTest_EscapeType_EscapeNonAscii()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeType = CharEscapeType.EscapeNonAscii
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

            Assert.Equal("A", escaped1);
            Assert.Equal("\\t", escaped2);
            Assert.Equal("\\u00C4", escaped3);
            Assert.Equal("\\u3131", escaped4);
            Assert.Equal(" ", escaped5);
            Assert.Equal("\\u00A0", escaped6);
        }

        [Fact]
        public void EscapeTest_EscapeType_EscapeEverything()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeType = CharEscapeType.EscapeEverything
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
            Assert.Equal("\\t", escaped2);
            Assert.Equal("\\u00C4", escaped3);
            Assert.Equal("\\u3131", escaped4);
            Assert.Equal("\\u0020", escaped5);
            Assert.Equal("\\u00A0", escaped6);
        }

        [Fact]
        public void EscapeTest_EscapeLetter_LowerCaseU()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLetter = CharEscapeLetter.LowerCaseU
            };

            char original = '\u00A0'; // non-breaking space
            string escaped = CharUtils.Escape(original, options);
            Assert.Equal("\\u00A0", escaped);
        }

        [Fact]
        public void EscapeTest_EscapeLetter_UpperCaseU()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLetter = CharEscapeLetter.UpperCaseU
            };

            char original = '\u00A0'; // non-breaking space
            string escaped = CharUtils.Escape(original, options);
            Assert.Equal("\\U000000A0", escaped);
        }

        [Fact]
        public void EscapeTest_EscapeLetter_LowerCaseXFixedLength()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLetter = CharEscapeLetter.LowerCaseXFixedLength
            };

            char original = '\u00A0'; // non-breaking space
            string escaped = CharUtils.Escape(original, options);
            Assert.Equal("\\x00A0", escaped);
        }

        [Fact]
        public void EscapeTest_EscapeLetter_LowerCaseXVariableLength()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLetter = CharEscapeLetter.LowerCaseXVariableLength
            };

            char original = '\u00A0'; // non-breaking space
            string escaped = CharUtils.Escape(original, options);
            Assert.Equal("\\xA0", escaped);
        }

        [Fact]
        public void EscapeTest_AlwaysUseUnicodeEscape()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                AlwaysUseUnicodeEscape = true
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

            Assert.Equal("\\u0027", escaped1);
            Assert.Equal("\\u0022", escaped2);
            Assert.Equal("\\u005C", escaped3);
            Assert.Equal("\\u0000", escaped4);
            Assert.Equal("\\u0007", escaped5);
            Assert.Equal("\\u0008", escaped6);
            Assert.Equal("\\u000C", escaped7);
            Assert.Equal("\\u000A", escaped8);
            Assert.Equal("\\u000D", escaped9);
            Assert.Equal("\\u0009", escaped10);
            Assert.Equal("\\u000B", escaped11);
        }

        [Fact]
        public void EscapeTest_UseLowerCaseHex()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                UseLowerCaseHex = true
            };

            char original = '\u00A0'; // non-breaking space
            string escaped = CharUtils.Escape(original, options);
            Assert.Equal("\\u00a0", escaped);
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
        public void UnescapeTest()
        {
            string escaped1 = "A";
            string escaped2 = "\\t";
            string escaped3 = "\\u00C4";
            string escaped4 = "\\u3131";
            string escaped5 = " ";
            string escaped6 = "\\u00A0";

            char unescaped1 = CharUtils.Unescape(escaped1);
            char unescaped2 = CharUtils.Unescape(escaped2);
            char unescaped3 = CharUtils.Unescape(escaped3);
            char unescaped4 = CharUtils.Unescape(escaped4);
            char unescaped5 = CharUtils.Unescape(escaped5);
            char unescaped6 = CharUtils.Unescape(escaped6);

            Assert.Equal('A', unescaped1);
            Assert.Equal('\t', unescaped2);
            Assert.Equal('Ä', unescaped3);
            Assert.Equal('ㄱ', unescaped4);
            Assert.Equal(' ', unescaped5);
            Assert.Equal('\u00A0', unescaped6); // non-breaking space
        }

        [Fact]
        public void UnescapeSurrogatePairTest()
        {
            string escaped1 = "\\uD83D\\uDE01";
            string escaped2 = "\\U0001F601";

            string unescaped1 = CharUtils.UnescapeSurrogatePair(escaped1);
            CharUtils.UnescapeSurrogatePair(escaped2, out char highSurrogate, out char lowSurrogate);
            string unescaped2 = new string(new char[] { highSurrogate, lowSurrogate });

            Assert.Equal("😁", unescaped1);
            Assert.Equal("😁", unescaped2);
        }

        [Fact]
        public void IsSurrogatePairTest()
        {
            string s1 = "😁";
            string s2 = "ab";

            Assert.True(CharUtils.IsSurrogatePair(s1));
            Assert.True(CharUtils.IsSurrogatePair(s1[0], s1[1]));
            Assert.False(CharUtils.IsSurrogatePair(s2));
            Assert.False(CharUtils.IsSurrogatePair(s2[0], s2[1]));
        }
    }
}
