using System;
using Xunit;

namespace Net.Laceous.Utilities.Tests
{
    public class StringUtilsTests
    {
        [Fact]
        public void EscapeTest_CSharp_LowerCaseU4_EscapeAll()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeType = EscapeType.EscapeAll
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13\\u0020\\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseU4_EscapeAll()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeType = EscapeType.EscapeAll
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13\\u0020\\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseU4_EscapeNonAscii()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeType = EscapeType.EscapeNonAscii
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("ABC \\u00C4\\u00D6\\u00DC \\u3131\\u3134\\u3137 \\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13 \\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseU4_EscapeNonAscii()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeType = EscapeType.EscapeNonAscii
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("ABC \\u00C4\\u00D6\\u00DC \\u3131\\u3134\\u3137 \\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13 \\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseU4_EscapeAll_EscapeSurrogatePairs()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeType = EscapeType.EscapeAll,
                EscapeSurrogatePairs = true
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\U0001F601\\U0001F603\\U0001F613\\u0020\\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_FSharp_LowerCaseU4_EscapeAll_EscapeSurrogatePairs()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeType = EscapeType.EscapeAll,
                EscapeSurrogatePairs = true
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseU4
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\U0001F601\\U0001F603\\U0001F613\\u0020\\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_CSharp_EscapeNonAscii_LowerCaseX2()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeType = EscapeType.EscapeNonAscii
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                EscapeLetter = EscapeLetter.LowerCaseX2 // or LowerCaseX1/LowerCaseX3
            };

            string original = "ÄA";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.NotEqual("\\xC4A", escaped); // '\xC4' + 'A' is not the same as '\xC4A'
            Assert.Equal("\\x00C4A", escaped);  // so we have to use the full '\x00C4' + 'A' sequence here
        }

        [Fact]
        public void EscapeTest_FSharp_EscapeNonAscii_LowerCaseX2()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeType = EscapeType.EscapeNonAscii
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                EscapeLetter = EscapeLetter.LowerCaseX2 // or LowerCaseX1/LowerCaseX3
            };

            string original = "ㄱg";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.NotEqual("\\x3131g", escaped); // the escape here is 4 chars (3131) so it can't be used with \xHH
            Assert.Equal("\\u3131g", escaped);    // instead we automatically switch to \uHHHH
        }

        [Fact]
        public void UnescapeTest_CSharp()
        {
            StringUnescapeOptions options = new StringUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp
            };

            string escaped = "\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13\\u0020\\u000D\\u000A\\u0009";
            string unescaped = StringUtils.Unescape(escaped, unescapeOptions: options);

            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t", unescaped);
        }

        [Fact]
        public void UnescapeTest_FSharp()
        {
            StringUnescapeOptions options = new StringUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp
            };

            string escaped = "\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13\\u0020\\u000D\\u000A\\u0009";
            string unescaped = StringUtils.Unescape(escaped, unescapeOptions: options);

            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t", unescaped);
        }

        [Fact]
        public void UnescapeTest_CSharp_BadInput()
        {
            StringUnescapeOptions options = new StringUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.CSharp,
                IsUnrecognizedEscapeVerbatim = false
            };

            string escaped = "\\uBAD";

            Assert.Throws<ArgumentException>(() => StringUtils.Unescape(escaped, unescapeOptions: options));

            options.IsUnrecognizedEscapeVerbatim = true;
            string unescaped = StringUtils.Unescape(escaped, unescapeOptions: options);

            Assert.Equal("\\uBAD", unescaped);
        }

        [Fact]
        public void UnescapeTest_FSharp_BadInput()
        {
            StringUnescapeOptions options = new StringUnescapeOptions()
            {
                EscapeLanguage = EscapeLanguage.FSharp,
                IsUnrecognizedEscapeVerbatim = false
            };

            string escaped = "\\uBAD";

            Assert.Throws<ArgumentException>(() => StringUtils.Unescape(escaped, unescapeOptions: options));

            options.IsUnrecognizedEscapeVerbatim = true;
            string unescaped = StringUtils.Unescape(escaped, unescapeOptions: options);

            Assert.Equal("\\uBAD", unescaped);
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

        [Fact]
        public void ArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => StringUtils.Escape(null));
            Assert.Throws<ArgumentNullException>(() => StringUtils.HasSurrogatePair(null));
            Assert.Throws<ArgumentNullException>(() => StringUtils.IndexOfSurrogatePair(null));
            Assert.Throws<ArgumentNullException>(() => StringUtils.LastIndexOfSurrogatePair(null));
            Assert.Throws<ArgumentNullException>(() => StringUtils.Unescape(null));
        }
    }
}
