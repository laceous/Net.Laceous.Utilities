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
                EscapeKind = StringEscapeKind.EscapeAll
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU4
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
                EscapeKind = StringEscapeKind.EscapeAll
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU4
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13\\u0020\\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_PowerShell_LowerCaseU4_EscapeAll()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeKind = StringEscapeKind.EscapeAll
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU4
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("`u{0041}`u{0042}`u{0043}`u{0020}`u{00C4}`u{00D6}`u{00DC}`u{0020}`u{3131}`u{3134}`u{3137}`u{0020}`u{D83D}`u{DE01}`u{D83D}`u{DE03}`u{D83D}`u{DE13}`u{0020}`u{000D}`u{000A}`u{0009}", escaped);
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseU4_EscapeNonAscii()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeKind = StringEscapeKind.EscapeNonAscii
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU4
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
                EscapeKind = StringEscapeKind.EscapeNonAscii
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU4
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("ABC \\u00C4\\u00D6\\u00DC \\u3131\\u3134\\u3137 \\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13 \\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_PowerShell_LowerCaseU4_EscapeNonAscii()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeKind = StringEscapeKind.EscapeNonAscii
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU4
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("ABC `u{00C4}`u{00D6}`u{00DC} `u{3131}`u{3134}`u{3137} `u{D83D}`u{DE01}`u{D83D}`u{DE03}`u{D83D}`u{DE13} `u{000D}`u{000A}`u{0009}", escaped);
        }

        [Fact]
        public void EscapeTest_CSharp_LowerCaseU4_EscapeAll_EscapeSurrogatePairs()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeKind = StringEscapeKind.EscapeAll,
                EscapeSurrogatePairs = true
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU4,
                SurrogatePairEscapeLetter = CharEscapeLetter.UpperCaseU8
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
                EscapeKind = StringEscapeKind.EscapeAll,
                EscapeSurrogatePairs = true
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseU4,
                SurrogatePairEscapeLetter = CharEscapeLetter.UpperCaseU8
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\U0001F601\\U0001F603\\U0001F613\\u0020\\u000D\\u000A\\u0009", escaped);
        }

        [Fact]
        public void EscapeTest_PowerShell_LowerCaseU4_EscapeAll_EscapeSurrogatePairs()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeKind = StringEscapeKind.EscapeAll,
                EscapeSurrogatePairs = true
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU4,
                SurrogatePairEscapeLetter = CharEscapeLetter.LowerCaseU5
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("`u{0041}`u{0042}`u{0043}`u{0020}`u{00C4}`u{00D6}`u{00DC}`u{0020}`u{3131}`u{3134}`u{3137}`u{0020}`u{1F601}`u{1F603}`u{1F613}`u{0020}`u{000D}`u{000A}`u{0009}", escaped);
        }

        [Fact]
        public void EscapeTest_PowerShell_LowerCaseU4_EscapeAll_EscapeSurrogatePairs_LowerCaseU6()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeKind = StringEscapeKind.EscapeAll,
                EscapeSurrogatePairs = true
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU4,
                SurrogatePairEscapeLetter = CharEscapeLetter.LowerCaseU6
            };

            string original = "ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.Equal("`u{0041}`u{0042}`u{0043}`u{0020}`u{00C4}`u{00D6}`u{00DC}`u{0020}`u{3131}`u{3134}`u{3137}`u{0020}`u{01F601}`u{01F603}`u{01F613}`u{0020}`u{000D}`u{000A}`u{0009}", escaped);
        }

        [Fact]
        public void EscapeTest_CSharp_EscapeNonAscii_LowerCaseX2()
        {
            StringEscapeOptions sOptions = new StringEscapeOptions()
            {
                EscapeKind = StringEscapeKind.EscapeNonAscii
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX2 // or LowerCaseX1/LowerCaseX3
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
                EscapeKind = StringEscapeKind.EscapeNonAscii
            };
            CharEscapeOptions cOptions = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                EscapeLetter = CharEscapeLetter.LowerCaseX2, // or LowerCaseX1/LowerCaseX3
                EscapeLetterFallback = CharEscapeLetter.LowerCaseU4
            };

            string original = "ㄱg";
            string escaped = StringUtils.Escape(original, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);

            Assert.NotEqual("\\x3131g", escaped); // the escape here is 4 chars (3131) so it can't be used with \xHH
            Assert.Equal("\\u3131g", escaped);    // instead we automatically switch to \uHHHH
        }

        [Fact]
        public void UnescapeTest_CSharp()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp
            };

            string escaped = "\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13\\u0020\\u000D\\u000A\\u0009";
            string unescaped = StringUtils.Unescape(escaped, unescapeOptions: options);

            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t", unescaped);
        }

        [Fact]
        public void UnescapeTest_FSharp()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp
            };

            string escaped = "\\u0041\\u0042\\u0043\\u0020\\u00C4\\u00D6\\u00DC\\u0020\\u3131\\u3134\\u3137\\u0020\\uD83D\\uDE01\\uD83D\\uDE03\\uD83D\\uDE13\\u0020\\u000D\\u000A\\u0009";
            string unescaped = StringUtils.Unescape(escaped, unescapeOptions: options);

            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t", unescaped);
        }

        [Fact]
        public void UnescapeTest_PowerShell()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell
            };

            string escaped = "`u{0041}`u{0042}`u{0043}`u{0020}`u{00C4}`u{00D6}`u{00DC}`u{0020}`u{3131}`u{3134}`u{3137}`u{0020}`u{D83D}`u{DE01}`u{D83D}`u{DE03}`u{D83D}`u{DE13}`u{0020}`u{000D}`u{000A}`u{0009}";
            string unescaped = StringUtils.Unescape(escaped, unescapeOptions: options);

            Assert.Equal("ABC ÄÖÜ ㄱㄴㄷ 😁😃😓 \r\n\t", unescaped);
        }

        [Fact]
        public void UnescapeTest_CSharp_BadInput()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp,
                IsUnrecognizedEscapeVerbatim = false
            };

            string escaped = "\\uBAD"; // bad because it doesn't match a valid escape sequence

            Assert.Throws<ArgumentException>(() => StringUtils.Unescape(escaped, unescapeOptions: options));

            options.IsUnrecognizedEscapeVerbatim = true;
            string unescaped = StringUtils.Unescape(escaped, unescapeOptions: options);

            Assert.Equal("\\uBAD", unescaped);
        }

        [Fact]
        public void UnescapeTest_FSharp_BadInput()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp,
                IsUnrecognizedEscapeVerbatim = false
            };

            string escaped = "\\uBAD"; // this is fine, it's treated as verbatim in F#

            string unescaped = StringUtils.Unescape(escaped, unescapeOptions: options);

            Assert.Equal("\\uBAD", unescaped);
        }

        [Fact]
        public void UnescapeTest_PowerShell_BadInput()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                IsUnrecognizedEscapeVerbatim = false
            };

            string escaped = "`u{0000BAD}"; // bad because it has 7 digits between the curly braces

            Assert.Throws<ArgumentException>(() => StringUtils.Unescape(escaped, unescapeOptions: options));

            options.IsUnrecognizedEscapeVerbatim = true;
            string unescaped = StringUtils.Unescape(escaped, unescapeOptions: options);

            Assert.Equal("`u{0000BAD}", unescaped);
        }

        [Fact]
        public void ArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => StringUtils.Escape(null));
            Assert.Throws<ArgumentNullException>(() => StringUtils.Unescape(null));
        }
    }
}
