using Xunit;

namespace Net.Laceous.Utilities.Tests
{
    public class StringUtilsTests
    {   
        [Fact]
        public void EscapeTest_EscapeType_EscapeAll()
        {
            StringEscapeOptions options = new StringEscapeOptions()
            {
                EscapeType = StringEscapeType.EscapeAll
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: options);
            Assert.Equal("\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13\\u0020\\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_EscapeType_EscapeNonAscii()
        {
            StringEscapeOptions options = new StringEscapeOptions()
            {
                EscapeType = StringEscapeType.EscapeNonAscii
            };
            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: options);
            Assert.Equal("ABC \\u00C4\\u00D6\\u00DC \\u3131\\u3134\\u3137 \\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13 \\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_EscapeLetter_LowerCaseU()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLetter = CharEscapeLetter.LowerCaseU
            };

            string original = "\r\n\t";
            string escaped = StringUtils.Escape(original, charEscapeOptions: options);
            Assert.Equal("\\u000D\\u000A\\u0009", escaped);
        }
        
        [Fact]
        public void EscapeTest_EscapeLetter_UpperCaseU()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLetter = CharEscapeLetter.UpperCaseU
            };

            string original = "\r\n\t";
            string escaped = StringUtils.Escape(original, charEscapeOptions: options);
            Assert.Equal("\\U0000000D\\U0000000A\\U00000009", escaped);
        }

        [Fact]
        public void EscapeTest_EscapeLetter_LowerCaseXFixedLength()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLetter = CharEscapeLetter.LowerCaseXFixedLength
            };

            string original = "\r\n\t";
            string escaped = StringUtils.Escape(original, charEscapeOptions: options);
            Assert.Equal("\\x000D\\x000A\\x0009", escaped);
        }
        
        [Fact]
        public void EscapeTest_EscapeLetter_LowerCaseXVariableLength()
        {
            StringEscapeOptions stringOptions = new StringEscapeOptions()
            {
                EscapeType = StringEscapeType.EscapeNonAscii
            };
            CharEscapeOptions charOptions = new CharEscapeOptions()
            {
                EscapeLetter = CharEscapeLetter.LowerCaseXVariableLength,
                UseShortEscape = false
            };

            string original = "\r\n\tA";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: stringOptions, charEscapeOptions: charOptions);
            Assert.Equal("\\xD\\xA\\x0009A", escaped);
            Assert.NotEqual("\\xD\\xA\\x9A", escaped); // this is not equal because \x9A is not the same as '\x9' + 'A'
        }

        [Fact]
        public void EscapeTest_UseLowerCaseHex()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                UseLowerCaseHex = true
            };

            string original = "\r\n\t";
            string escaped = StringUtils.Escape(original, charEscapeOptions: options);
            Assert.Equal("\\u000d\\u000a\\u0009", escaped);
        }
        
        [Fact]
        public void EscapeTest_EscapeSurrogatePairs()
        {
            StringEscapeOptions options = new StringEscapeOptions()
            {
                EscapeSurrogatePairs = true
            };

            string original = "😁😃😓";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: options);
            Assert.Equal("\\U0001F601\\U0001F603\\U0001F613", escaped);
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
            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t A \\u41", unescaped); // \\u41 is not a valid sequence but we're treating it as verbatim
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
            Assert.Equal(8, StringUtils.IndexOfSurrogatePair(s, 4));
        }

        [Fact]
        public void IndexOfSurrogatePairTest_Index_Count()
        {
            string s = "abc😁def😃ghi😓jklm";
            Assert.Equal(-1, StringUtils.IndexOfSurrogatePair(s, 4, 1));
        }

        [Fact]
        public void LastIndexOfSurrogatePairTest()
        {
            string s = "abc😁def😃ghi😓jklm";
            Assert.Equal(13, StringUtils.LastIndexOfSurrogatePair(s));
        }

        [Fact]
        public void LastIndexOfSurrogatePairTest_Index()
        {
            string s = "abc😁def😃ghi😓jklm";
            Assert.Equal(8, StringUtils.LastIndexOfSurrogatePair(s, 12));
        }

        [Fact]
        public void LastIndexOfSurrogatePairTest_Index_Count()
        {
            string s = "abc😁def😃ghi😓jklm";
            Assert.Equal(-1, StringUtils.LastIndexOfSurrogatePair(s, 12, 1));
        }
    }
}
