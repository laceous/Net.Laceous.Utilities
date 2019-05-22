# Net.Laceous.Utilities

This currently contains char and string utilities targeting [.NET Standard 1.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

Char and string escaping:

```c#
CharEscapeOptions options = new CharEscapeOptions()
{
    EscapeType = CharEscapeType.EscapeAllCharsExceptAscii,
    AlwaysUseUnicodeEscape = false,
    UseLowerCaseHex = false,
    UseLowerCaseX = false
};

char cOriginal = 'Ä';
string cEscaped = CharUtils.Escape(cOriginal, escapeOptions: options);
char cUnescaped = CharUtils.Unescape(cEscaped);
Console.WriteLine("\'" + cEscaped + "\'"); // '\u00C4'
Console.WriteLine(cUnescaped);             // Ä

string eOriginal = "😁"; // 2 char emoji
string eEscaped1 = CharUtils.Escape(eOriginal[0], escapeOptions: options) + CharUtils.Escape(eOriginal[1], escapeOptions: options);
string eEscaped2 = CharUtils.EscapeSurrogatePair(eOriginal, useLowerCaseHex: options.UseLowerCaseHex);
string eUnescaped1 = CharUtils.UnescapeSurrogatePair(eEscaped1);
string eUnescaped2 = CharUtils.UnescapeSurrogatePair(eEscaped2);
Console.WriteLine("\"" + eEscaped1 + "\""); // "\uD83D\uDE01"
Console.WriteLine("\"" + eEscaped2 + "\""); // "\U0001F601"
Console.WriteLine(eUnescaped1);             // 😁
Console.WriteLine(eUnescaped2);             // 😁

string sOriginal = "abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓";
string sEscaped = StringUtils.Escape(sOriginal, escapeSurrogatePairs: true, escapeOptions: options);
string sUnescaped = StringUtils.Unescape(sEscaped, unrecognizedEscapeIsVerbatim: false);
Console.WriteLine("\"" + sEscaped + "\""); // "abc ABC 123 \u00C4\u00D6\u00DC \u3131\u3134\u3137 \U0001F601\U0001F603\U0001F613"
Console.WriteLine(sUnescaped);             // abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
```

Supported escape sequences:
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
* `\unnnn` (Unicode escape sequence)
* `\xn` or `\xnn` or `\xnnn` or `\xnnnn` (Variable length unicode escape sequence)
* `\Unnnnnnnn` (Unicode escape sequence for surrogate pairs)

Surrogate pairs:

```c#
string emoji = "😁"; // 2 char emoji
bool eIsSurrogatePair = CharUtils.IsSurrogatePair(emoji);
Console.WriteLine(eIsSurrogatePair); // True

string s = "abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓";
bool sHasSurrogatePair = StringUtils.HasSurrogatePair(s);
int sCountSurrogatePairs = StringUtils.CountSurrogatePairs(s);
int sIndexOfSurrogatePair = StringUtils.IndexOfSurrogatePair(s);
int sLastIndexOfSurrogatePair = StringUtils.LastIndexOfSurrogatePair(s);
Console.WriteLine(sHasSurrogatePair); // True
Console.WriteLine(sCountSurrogatePairs); // 3
Console.WriteLine("Visible chars: " + (s.Length - sCountSurrogatePairs)); // Visible chars: 23
Console.WriteLine(sIndexOfSurrogatePair); // 20
Console.WriteLine(sLastIndexOfSurrogatePair); // 24
```
