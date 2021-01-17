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

        [Theory]
        [InlineData('A', "`u{41}")]
        [InlineData('\t', "`u{9}")]
        [InlineData('Ä', "`u{C4}")]
        [InlineData('ㄱ', "`u{3131}")]
        [InlineData(' ', "`u{20}")]
        [InlineData('\u00A0', "`u{A0}")]
        public void EscapeTest_PowerShell_LowerCaseU1(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU1
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "`u{41}")]
        [InlineData('\t', "`u{09}")]
        [InlineData('Ä', "`u{C4}")]
        [InlineData('ㄱ', "`u{3131}")]
        [InlineData(' ', "`u{20}")]
        [InlineData('\u00A0', "`u{A0}")]
        public void EscapeTest_PowerShell_LowerCaseU2(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU2
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "`u{041}")]
        [InlineData('\t', "`u{009}")]
        [InlineData('Ä', "`u{0C4}")]
        [InlineData('ㄱ', "`u{3131}")]
        [InlineData(' ', "`u{020}")]
        [InlineData('\u00A0', "`u{0A0}")]
        public void EscapeTest_PowerShell_LowerCaseU3(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU3
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "`u{0041}")]
        [InlineData('\t', "`u{0009}")]
        [InlineData('Ä', "`u{00C4}")]
        [InlineData('ㄱ', "`u{3131}")]
        [InlineData(' ', "`u{0020}")]
        [InlineData('\u00A0', "`u{00A0}")]
        public void EscapeTest_PowerShell_LowerCaseU4(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU4
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "`u{00041}")]
        [InlineData('\t', "`u{00009}")]
        [InlineData('Ä', "`u{000C4}")]
        [InlineData('ㄱ', "`u{03131}")]
        [InlineData(' ', "`u{00020}")]
        [InlineData('\u00A0', "`u{000A0}")]
        public void EscapeTest_PowerShell_LowerCaseU5(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU5
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Theory]
        [InlineData('A', "`u{000041}")]
        [InlineData('\t', "`u{000009}")]
        [InlineData('Ä', "`u{0000C4}")]
        [InlineData('ㄱ', "`u{003131}")]
        [InlineData(' ', "`u{000020}")]
        [InlineData('\u00A0', "`u{0000A0}")]
        public void EscapeTest_PowerShell_LowerCaseU6(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU6
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_PowerShell_LowerCaseX1()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseX1
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_PowerShell_LowerCaseX2()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseX2
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_PowerShell_LowerCaseX3()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseX3
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_PowerShell_LowerCaseX4()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseX4
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_PowerShell_UpperCaseU8()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.UpperCaseU8
            };

            char original = 'A';

            Assert.Throws<ArgumentException>(() => CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeTest_PowerShell_None3()
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.None3
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
        [InlineData('\'', "`u{0027}")]
        [InlineData('\"', "`\"")]
        [InlineData('`', "``")]
        [InlineData('\0', "`0")]
        [InlineData('\a', "`a")]
        [InlineData('\b', "`b")]
        [InlineData('\x1B', "`e")]
        [InlineData('\f', "`f")]
        [InlineData('\n', "`n")]
        [InlineData('\r', "`r")]
        [InlineData('\t', "`t")]
        [InlineData('\v', "`v")]
        public void EscapeTest_PowerShell_UseShortEscape(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
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
        [InlineData('Ä', "`u{00c4}")]
        [InlineData('\u00A0', "`u{00a0}")]
        public void EscapeTest_PowerShell_UseLowerCaseHex(char original, string escaped)
        {
            CharEscapeOptions options = new CharEscapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell,
                EscapeLetter = CharEscapeLetter.LowerCaseU4,
                UseLowerCaseHex = true
            };

            Assert.Equal(escaped, CharUtils.Escape(original, options));
        }

        [Fact]
        public void EscapeSurrogatePairTest_CSharp()
        {
            string original = "😁"; // 2 char emoji
            string escaped1 = CharUtils.EscapeSurrogatePair(original[0], original[1], new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.CSharp, surrogatePairEscapeLetter: CharEscapeLetter.UpperCaseU8, useLowerCaseHex: false));
            string escaped2 = CharUtils.EscapeSurrogatePair(original, new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.CSharp, surrogatePairEscapeLetter: CharEscapeLetter.UpperCaseU8, useLowerCaseHex: true));
            Assert.Equal("\\U0001F601", escaped1);
            Assert.Equal("\\U0001f601", escaped2);
        }

        [Fact]
        public void EscapeSurrogatePairTest_FSharp()
        {
            string original = "😁"; // 2 char emoji
            string escaped1 = CharUtils.EscapeSurrogatePair(original[0], original[1], new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.FSharp, surrogatePairEscapeLetter: CharEscapeLetter.UpperCaseU8, useLowerCaseHex: false));
            string escaped2 = CharUtils.EscapeSurrogatePair(original, new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.FSharp, surrogatePairEscapeLetter: CharEscapeLetter.UpperCaseU8, useLowerCaseHex: true));
            Assert.Equal("\\U0001F601", escaped1);
            Assert.Equal("\\U0001f601", escaped2);
        }

        [Fact]
        public void EscapeSurrogatePairTest_PowerShell()
        {
            string original = "😁"; // 2 char emoji
            string escaped1 = CharUtils.EscapeSurrogatePair(original[0], original[1], new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.PowerShell, surrogatePairEscapeLetter: CharEscapeLetter.LowerCaseU5, useLowerCaseHex: false));
            string escaped2 = CharUtils.EscapeSurrogatePair(original, new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.PowerShell, surrogatePairEscapeLetter: CharEscapeLetter.LowerCaseU6, useLowerCaseHex: true));
            Assert.Equal("`u{1F601}", escaped1);
            Assert.Equal("`u{01f601}", escaped2);
        }

        [Fact]
        public void EscapeSurrogatePairTest_CSharp_BadInput()
        {
            string original = "ab"; // not a surrogate pair

            Assert.Throws<ArgumentOutOfRangeException>(() => CharUtils.EscapeSurrogatePair(original[0], original[1], new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.CSharp, surrogatePairEscapeLetter: CharEscapeLetter.UpperCaseU8, useLowerCaseHex: false)));
            Assert.Throws<ArgumentException>(() => CharUtils.EscapeSurrogatePair(original, new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.CSharp, surrogatePairEscapeLetter: CharEscapeLetter.UpperCaseU8, useLowerCaseHex: true)));
        }

        [Fact]
        public void EscapeSurrogatePairTest_FSharp_BadInput()
        {
            string original = "ab"; // not a surrogate pair

            Assert.Throws<ArgumentOutOfRangeException>(() => CharUtils.EscapeSurrogatePair(original[0], original[1], new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.FSharp, surrogatePairEscapeLetter: CharEscapeLetter.UpperCaseU8, useLowerCaseHex: false)));
            Assert.Throws<ArgumentException>(() => CharUtils.EscapeSurrogatePair(original, new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.FSharp, surrogatePairEscapeLetter: CharEscapeLetter.UpperCaseU8, useLowerCaseHex: true)));
        }

        [Fact]
        public void EscapeSurrogatePairTest_PowerShell_BadInput()
        {
            string original = "ab"; // not a surrogate pair

            Assert.Throws<ArgumentOutOfRangeException>(() => CharUtils.EscapeSurrogatePair(original[0], original[1], new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.PowerShell, surrogatePairEscapeLetter: CharEscapeLetter.LowerCaseU5, useLowerCaseHex: false)));
            Assert.Throws<ArgumentException>(() => CharUtils.EscapeSurrogatePair(original, new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.PowerShell, surrogatePairEscapeLetter: CharEscapeLetter.LowerCaseU5, useLowerCaseHex: true)));
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

        [Theory]
        [InlineData("`u{0041}", 'A')]
        [InlineData("`t", '\t')]
        [InlineData("`u{C4}", 'Ä')]
        [InlineData("`u{3131}", 'ㄱ')]
        [InlineData("`u{020}", ' ')]
        [InlineData("`u{0000A0}", '\u00A0')]
        public void UnescapeTest_PowerShell(string escaped, char unescaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell
            };

            Assert.Equal(unescaped, CharUtils.Unescape(escaped, options));
        }

        [Theory]
        [InlineData("`u{0000BAD}")] // not a valid escape
        public void UnescapeTest_PowerShell_BadInput(string escaped)
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell
            };

            Assert.Throws<ArgumentException>(() => CharUtils.Unescape(escaped, options));
        }

        [Fact]
        public void UnescapeSurrogatePairTest_CSharp()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp
            };

            string escaped = "\\U0001F601";

            string unescaped1 = CharUtils.UnescapeSurrogatePair(escaped, options);
            CharUtils.UnescapeSurrogatePair(escaped, out char highSurrogate, out char lowSurrogate, options);
            string unescaped2 = new string(new char[] { highSurrogate, lowSurrogate });

            Assert.Equal("😁", unescaped1);
            Assert.Equal("😁", unescaped2);
        }

        [Fact]
        public void UnescapeSurrogatePairTest_FSharp()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp
            };

            string escaped = "\\U0001F601";

            string unescaped1 = CharUtils.UnescapeSurrogatePair(escaped, options);
            CharUtils.UnescapeSurrogatePair(escaped, out char highSurrogate, out char lowSurrogate, options);
            string unescaped2 = new string(new char[] { highSurrogate, lowSurrogate });

            Assert.Equal("😁", unescaped1);
            Assert.Equal("😁", unescaped2);
        }

        [Fact]
        public void UnescapeSurrogatePairTest_PowerShell()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell
            };

            string escaped = "`u{1F601}";

            string unescaped1 = CharUtils.UnescapeSurrogatePair(escaped, options);
            CharUtils.UnescapeSurrogatePair(escaped, out char highSurrogate, out char lowSurrogate, options);
            string unescaped2 = new string(new char[] { highSurrogate, lowSurrogate });

            Assert.Equal("😁", unescaped1);
            Assert.Equal("😁", unescaped2);
        }

        [Fact]
        public void UnescapeSurrogatePairTest_CSharp_BadInput()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.CSharp
            };

            string escaped = "\\UBAD";

            Assert.Throws<ArgumentException>(() => CharUtils.UnescapeSurrogatePair(escaped, options));
            Assert.Throws<ArgumentException>(() => CharUtils.UnescapeSurrogatePair(escaped, out char highSurrogate, out char lowSurrogate, options));
        }

        [Fact]
        public void UnescapeSurrogatePairTest_FSharp_BadInput()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.FSharp
            };

            string escaped = "\\UBAD";

            Assert.Throws<ArgumentException>(() => CharUtils.UnescapeSurrogatePair(escaped, options));
            Assert.Throws<ArgumentException>(() => CharUtils.UnescapeSurrogatePair(escaped, out char highSurrogate, out char lowSurrogate, options));
        }

        [Fact]
        public void UnescapeSurrogatePairTest_PowerShell_BadInput()
        {
            CharUnescapeOptions options = new CharUnescapeOptions()
            {
                EscapeLanguage = CharEscapeLanguage.PowerShell
            };

            string escaped = "`u{BAD}";

            Assert.Throws<ArgumentException>(() => CharUtils.UnescapeSurrogatePair(escaped, options));
            Assert.Throws<ArgumentException>(() => CharUtils.UnescapeSurrogatePair(escaped, out char highSurrogate, out char lowSurrogate, options));
        }

        [Fact]
        public void ArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => CharUtils.EscapeSurrogatePair(null));
            Assert.Throws<ArgumentNullException>(() => CharUtils.Unescape(null));
            Assert.Throws<ArgumentNullException>(() => CharUtils.UnescapeSurrogatePair(null));
            Assert.Throws<ArgumentNullException>(() => CharUtils.UnescapeSurrogatePair(null, out char highSurrogate, out char lowSurrogate));
        }
    }
}
