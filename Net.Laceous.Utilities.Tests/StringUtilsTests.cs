using Xunit;

namespace Net.Laceous.Utilities.Tests
{
    public class StringUtilsTests
    {
        [Fact]
        public void EscapeTest_EscapeType_Default()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeType = CharEscapeType.Default
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, escapeOptions: options);
            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \\r\\n\\t", escaped);
        }

        [Fact]
        public void EscapeTest_EscapeType_EscapeAllCharsExceptAscii()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeType = CharEscapeType.EscapeAllCharsExceptAscii
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, escapeOptions: options);
            Assert.Equal("ABC \\u00C4\\u00D6\\u00DC \\u3131\\u3134\\u3137 \\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13 \\r\\n\\t", escaped);
        }

        [Fact]
        public void EscapeTest_EscapeType_EscapeAllChars()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeType = CharEscapeType.EscapeAllChars
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, escapeOptions: options);
            Assert.Equal("\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13\\u0020\\r\\n\\t", escaped);
        }

        [Fact]
        public void EscapeTest_AlwaysUseUnicodeEscape()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                AlwaysUseUnicodeEscape = true
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, escapeOptions: options);
            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_UseLowerCaseHex()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                AlwaysUseUnicodeEscape = true,
                UseLowerCaseHex = true
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, escapeOptions: options);
            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \\u000d\\u000a\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_UseLowerCaseX()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                AlwaysUseUnicodeEscape = true,
                UseLowerCaseX = true
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, escapeOptions: options);
            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \\x000D\\x000A\\x0009", escaped);
        }

        [Fact]
        public void UnescapeTest()
        {
            string escaped = "ABC \\u00C4\\u00D6\\u00DC \\u3131\\u3134\\u3137 \\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13 \\r\\n\\t";
            string unescaped = StringUtils.Unescape(escaped);
            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t", unescaped);
        }

        [Fact]
        public void UnescapeTest_UnrecognizedEscapeIsVerbatim()
        {
            string escaped = "ABC \\u00C4\\u00D6\\u00DC \\u3131\\u3134\\u3137 \\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13 \\r\\n\\t \\x41 \\u41";
            string unescaped = StringUtils.Unescape(escaped, unrecognizedEscapeIsVerbatim: true);
            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t A \\u41", unescaped);
        }

        [Fact]
        public void HasSurrogatePairTest()
        {
            string s1 = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string s2 = "ABC ÄÖÜ ㄱㄴㄷ \r\n\t";
            Assert.True(StringUtils.HasSurrogatePair(s1));
            Assert.False(StringUtils.HasSurrogatePair(s2));
        }

        [Fact]
        public void CountCompleteCharsTest()
        {
            string s = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            int length = s.Length;
            int count = StringUtils.CountCompleteChars(s);
            Assert.Equal(22, length);
            Assert.Equal(19, count);
        }

        [Fact]
        public void IndexOfSurrogatePairTest()
        {
            string s = "abc😁def😃ghi😓jklm";
            int i = StringUtils.IndexOfSurrogatePair(s);
            Assert.Equal(3, i);
        }

        [Fact]
        public void IndexOfSurrogatePairTest_Index()
        {
            string s = "abc😁def😃ghi😓jklm";
            int i = StringUtils.IndexOfSurrogatePair(s, 4);
            Assert.Equal(8, i);
        }

        [Fact]
        public void IndexOfSurrogatePairTest_Index_Count()
        {
            string s = "abc😁def😃ghi😓jklm";
            int i = StringUtils.IndexOfSurrogatePair(s, 4, 1);
            Assert.Equal(-1, i);
        }

        [Fact]
        public void LastIndexOfSurrogatePairTest()
        {
            string s = "abc😁def😃ghi😓jklm";
            int i = StringUtils.LastIndexOfSurrogatePair(s);
            Assert.Equal(13, i);
        }

        [Fact]
        public void LastIndexOfSurrogatePairTest_Index()
        {
            string s = "abc😁def😃ghi😓jklm";
            int i = StringUtils.LastIndexOfSurrogatePair(s, 12);
            Assert.Equal(8, i);
        }

        [Fact]
        public void LastIndexOfSurrogatePairTest_Index_Count()
        {
            string s = "abc😁def😃ghi😓jklm";
            int i = StringUtils.LastIndexOfSurrogatePair(s, 12, 1);
            Assert.Equal(-1, i);
        }

        [Fact]
        public void AllIndexesOfSurrogatePairsTest()
        {
            string s = "abc😁def😃ghi😓jklm";
            int[] iArr = StringUtils.AllIndexesOfSurrogatePairs(s);
            Assert.Equal(3, iArr.Length);
            Assert.Equal(3, iArr[0]);
            Assert.Equal(8, iArr[1]);
            Assert.Equal(13, iArr[2]);
        }
    }
}
