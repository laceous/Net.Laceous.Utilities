# Net.Laceous.Utilities

This currently contains char and string utilities targeting [.NET Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

Char and string escaping:

```c#
CharEscapeOptions cOptions = new CharEscapeOptions()
{
    EscapeLanguage = EscapeLanguage.CSharp,
    EscapeLetter = EscapeLetter.LowerCaseU4,
    UseLowerCaseHex = false,
    UseShortEscape = false,
};
StringEscapeOptions sOptions = new StringEscapeOptions()
{
    EscapeType = EscapeType.EscapeNonAscii,
    EscapeSurrogatePairs = true
};
CharUnescapeOptions cuOptions = new CharUnescapeOptions()
{
    EscapeLanguage = cOptions.EscapeLanguage
};
StringUnescapeOptions suOptions = new StringUnescapeOptions()
{
    IsUnrecognizedEscapeVerbatim = true
};

char cOriginal = 'Ä';
string cEscaped = CharUtils.Escape(cOriginal, escapeOptions: cOptions);
char cUnescaped = CharUtils.Unescape(cEscaped, unescapeOptions: cuOptions);
Debug.WriteLine("\'" + cEscaped + "\'"); // '\u00C4'
Debug.WriteLine(cUnescaped);             // Ä

string eOriginal = "😁"; // 2 char emoji
string eEscaped = CharUtils.EscapeSurrogatePair(eOriginal, useLowerCaseHex: cOptions.UseLowerCaseHex);
string eUnescaped = CharUtils.UnescapeSurrogatePair(eEscaped);
Debug.WriteLine("\"" + eEscaped + "\""); // "\U0001F601"
Debug.WriteLine(eUnescaped);             // 😁

string sOriginal = "abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓";
string sEscaped = StringUtils.Escape(sOriginal, stringEscapeOptions: sOptions, charEscapeOptions: cOptions);
string sUnescaped = StringUtils.Unescape(sEscaped, stringUnescapeOptions: suOptions, charUnescapeOptions: cuOptions);
Debug.WriteLine("\"" + sEscaped + "\""); // "abc ABC 123 \u00C4\u00D6\u00DC \u3131\u3134\u3137 \U0001F601\U0001F603\U0001F613"
Debug.WriteLine(sUnescaped);             // abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
```

Supported [C# escape sequences](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#string-escape-sequences):
* `\'` (Single quote)
* `\"` (Double quote)
* `\\` (Backslash)
* `\0` (Null)
* `\a` (Alert)
* `\b` (Backspace)
* `\f` (Form feed)
* `\n` (New line)
* `\r` (Carriage return)
* `\t` (Horizontal tab)
* `\v` (Vertical tab)
* `\uHHHH` (Unicode escape sequence)
* `\xH` or `\xHH` or `\xHHH` or `\xHHHH` (Variable length unicode escape sequence)
* `\UHHHHHHHH` (Unicode escape sequence for surrogate pairs)

Supported [F# escape sequences](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/strings#remarks):
* `\a` (Alert)
* `\b` (Backspace)
* `\f` (Form feed)
* `\n` (Newline)
* `\r` (Carriage return)
* `\t` (Tab)
* `\v` (Vertical tab)
* `\\` (Backslash)
* `\"` (Quotation mark)
* `\'` (Apostrophe)
* `\DDD` (Decimal escape sequence; 000-255)
* `\xHH` (Hexadecimal escape sequence; 00-FF)
* `\uHHHH` (Unicode escape sequence)
* `\UHHHHHHHH` (Unicode escape sequence for surrogate pairs)

Surrogate pairs:

```c#
string emoji = "😁"; // 2 char emoji
Debug.WriteLine(CharUtils.IsSurrogatePair(emoji));        // True

string s = "abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓";
Debug.WriteLine(StringUtils.HasSurrogatePair(s));         // True
Debug.WriteLine(StringUtils.IndexOfSurrogatePair(s));     // 20
Debug.WriteLine(StringUtils.LastIndexOfSurrogatePair(s)); // 24
```
