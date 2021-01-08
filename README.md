# Net.Laceous.Utilities

This currently contains char and string utilities targeting [.NET Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

## Char and string escaping

### C#

```csharp
Console.OutputEncoding = Encoding.UTF8; // use a terminal that supports emojis

CharEscapeOptions ceOptions = new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.CSharp, escapeLetter: CharEscapeLetter.LowerCaseU4, escapeLetterFallback: CharEscapeLetter.LowerCaseU4, useLowerCaseHex: false, useShortEscape: false);
CharUnescapeOptions cuOptions = new CharUnescapeOptions(escapeLanguage: CharEscapeLanguage.CSharp);
StringEscapeOptions seOptions = new StringEscapeOptions(escapeType: StringEscapeType.EscapeNonAscii, escapeSurrogatePairs: true, escapeLetterSurrogatePairs: CharEscapeLetter.UpperCaseU8);
StringUnescapeOptions suOptions = new StringUnescapeOptions(isUnrecognizedEscapeVerbatim: true);

char cOriginal = 'Ä';
string cEscaped = CharUtils.Escape(cOriginal, escapeOptions: ceOptions);
char cUnescaped = CharUtils.Unescape(cEscaped, unescapeOptions: cuOptions);
Console.WriteLine("\'{0}\'", cEscaped); // '\u00C4'
Console.WriteLine(cUnescaped);          // Ä

string eOriginal = "😁"; // 2 char emoji
string eEscaped = CharUtils.EscapeSurrogatePair(eOriginal, escapeOptions: ceOptions);
string eUnescaped = CharUtils.UnescapeSurrogatePair(eEscaped, unescapeOptions: cuOptions);
Console.WriteLine("\"{0}\"", eEscaped); // "\U0001F601"
Console.WriteLine(eUnescaped);          // 😁

string sOriginal = "abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓";
string sEscaped = StringUtils.Escape(sOriginal, stringEscapeOptions: seOptions, charEscapeOptions: ceOptions);
string sUnescaped = StringUtils.Unescape(sEscaped, stringUnescapeOptions: suOptions, charUnescapeOptions: cuOptions);
Console.WriteLine("\"{0}\"", sEscaped); // "abc ABC 123 \u00C4\u00D6\u00DC \u3131\u3134\u3137 \U0001F601\U0001F603\U0001F613"
Console.WriteLine(sUnescaped);          // abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
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

### F#

```fsharp
Console.OutputEncoding <- Encoding.UTF8 // use a terminal that supports emojis

let ceOptions = new CharEscapeOptions(escapeLanguage = CharEscapeLanguage.FSharp, escapeLetter = CharEscapeLetter.LowerCaseU4, escapeLetterFallback = CharEscapeLetter.LowerCaseU4, useLowerCaseHex = false, useShortEscape = false)
let cuOptions = new CharUnescapeOptions(escapeLanguage = CharEscapeLanguage.FSharp)
let seOptions = new StringEscapeOptions(escapeType = StringEscapeType.EscapeNonAscii, escapeSurrogatePairs = true, escapeLetterSurrogatePairs = CharEscapeLetter.UpperCaseU8)
let suOptions = new StringUnescapeOptions(isUnrecognizedEscapeVerbatim = true)

let cOriginal = 'Ä'
let cEscaped = CharUtils.Escape(cOriginal, escapeOptions = ceOptions)
let cUnescaped = CharUtils.Unescape(cEscaped, unescapeOptions = cuOptions)
printfn "\'%s\'" (cEscaped) // '\u00C4'
printfn "%c" (cUnescaped)   // Ä

let eOriginal = "😁"; // 2 char emoji
let eEscaped = CharUtils.EscapeSurrogatePair(eOriginal, escapeOptions = ceOptions)
let eUnescaped = CharUtils.UnescapeSurrogatePair(eEscaped, unescapeOptions = cuOptions)
printfn "\"%s\"" (eEscaped) // "\U0001F601"
printfn "%s" (eUnescaped)   // 😁

let sOriginal = "abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓"
let sEscaped = StringUtils.Escape(sOriginal, stringEscapeOptions = seOptions, charEscapeOptions = ceOptions)
let sUnescaped = StringUtils.Unescape(sEscaped, stringUnescapeOptions = suOptions, charUnescapeOptions = cuOptions)
printfn "\"%s\"" (sEscaped) // "abc ABC 123 \u00C4\u00D6\u00DC \u3131\u3134\u3137 \U0001F601\U0001F603\U0001F613"
printfn "%s" (sUnescaped)   // abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
```

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

### PowerShell

```powershell
Add-Type -Path '/path/to/Net.Laceous.Utilities.dll'

$ceOptions = [Net.Laceous.Utilities.CharEscapeOptions]::New('PowerShell', 'LowerCaseU4', 'LowerCaseU4', $false, $false)
$cuOptions = [Net.Laceous.Utilities.CharUnescapeOptions]::New('PowerShell')
$seOptions = [Net.Laceous.Utilities.StringEscapeOptions]::New('EscapeNonAscii', $true, 'LowerCaseU5')
$suOptions = [Net.Laceous.Utilities.StringUnescapeOptions]::New($true)

$cOriginal = 'Ä'
$cEscaped = [Net.Laceous.Utilities.CharUtils]::Escape($cOriginal, $ceOptions)
$cUnescaped = [Net.Laceous.Utilities.CharUtils]::Unescape($cEscaped, $cuOptions)
Write-Host "`"$cEscaped`"" # "`u{00C4}"
Write-Host $cUnescaped     # Ä

$eOriginal = '😁' # 2 char emoji
$eEscaped = [Net.Laceous.Utilities.CharUtils]::EscapeSurrogatePair($eOriginal, $ceOptions)
$eUnescaped = [Net.Laceous.Utilities.CharUtils]::UnescapeSurrogatePair($eEscaped, $cuOptions)
Write-Host "`"$eEscaped`"" # // "`u{1F601}"
Write-Host $eUnescaped     # 😁

$sOriginal = 'abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓'
$sEscaped = [Net.Laceous.Utilities.StringUtils]::Escape($sOriginal, $seOptions, $ceOptions)
$sUnescaped = [Net.Laceous.Utilities.StringUtils]::Unescape($sEscaped, $suOptions, $cuOptions)
Write-Host "`"$sEscaped`"" # "abc ABC 123 `u{00C4}`u{00D6}`u{00DC} `u{3131}`u{3134}`u{3137} `u{1F601}`u{1F603}`u{1F613}"
Write-Host $sUnescaped     # abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
```

Supported [PowerShell escape sequences](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_special_characters?view=powershell-7.1):
* `` `0 `` (Null)
* `` `a `` (Alert)
* `` `b `` (Backspace)
* `` `e `` (Escape)
* `` `f `` (Form feed)
* `` `n `` (New line)
* `` `r `` (Carriage return)
* `` `t `` (Horizontal tab)
* `` `v `` (Vertical tab)
* `` `u{H} `` or `` `u{HH} `` or `` `u{HHH} `` or `` `u{HHHH} `` or `` `u{HHHHH} `` or `` `u{HHHHHH} `` (Variable length unicode escape sequence)

### Python

```python
import sys
import clr
# path to Net.Laceous.Utilities.dll and UnicodeInformation.dll
sys.path.append('/path/to/Net.Laceous.Utilities')
clr.AddReference('Net.Laceous.Utilities')
from Net.Laceous.Utilities import CharUtils
from Net.Laceous.Utilities import CharEscapeOptions
from Net.Laceous.Utilities import CharEscapeLanguage
from Net.Laceous.Utilities import CharEscapeLetter
from Net.Laceous.Utilities import CharUnescapeOptions
from Net.Laceous.Utilities import StringUtils
from Net.Laceous.Utilities import StringEscapeOptions
from Net.Laceous.Utilities import StringEscapeType
from Net.Laceous.Utilities import StringUnescapeOptions

# work around the following error:
# UnicodeEncodeError: 'utf-8' codec can't encode characters in position x-x: surrogates not allowed
def sp(s):
    return s.encode('utf-16', 'surrogatepass').decode('utf-16', 'replace')

ceOptions = CharEscapeOptions(escapeLanguage = CharEscapeLanguage.Python, escapeLetter = CharEscapeLetter.LowerCaseU4, escapeLetterSurrogatePair = CharEscapeLetter.UpperCaseU8, useLowerCaseHex = False, useShortEscape = False)
cuOptions = CharUnescapeOptions(escapeLanguage = CharEscapeLanguage.Python)
seOptions = StringEscapeOptions(escapeType = StringEscapeType.EscapeNonAscii, escapeSurrogatePairs = True)
suOptions = StringUnescapeOptions(isUnrecognizedEscapeVerbatim = True)

cOriginal = 'Ä'
cEscaped = CharUtils.Escape(cOriginal, ceOptions)
cUnescaped = sp(CharUtils.Unescape(cEscaped, cuOptions))
print(f"\"{cEscaped}\"") # "\u00C4"
print(cUnescaped)        # Ä

sOriginal = 'abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓'
sEscaped = StringUtils.Escape(sOriginal, seOptions, ceOptions)
sUnescaped = sp(StringUtils.Unescape(sEscaped, suOptions, cuOptions))
print(f"\"{sEscaped}\"") # "abc ABC 123 \u00C4\u00D6\u00DC \u3131\u3134\u3137 \U0001F601\U0001F603\U0001F613"
print(sUnescaped)        # abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓

# this requires UnicodeInformation.dll
ceOptions.EscapeLetterSurrogatePair = CharEscapeLetter.UpperCaseN1

eOriginal = '😁'
eEscaped = CharUtils.EscapeSurrogatePair(eOriginal, ceOptions)
eUnescaped = sp(CharUtils.UnescapeSurrogatePair(eEscaped, cuOptions))
print(f"\"{eEscaped}\"") # "\N{Grinning Face With Smiling Eyes}"
print(eUnescaped)        # 😁

# two_char_emoji_1 = '\uD83D\uDE01' # 😁
# two_char_emoji_2 = '\U0001F601'   # 😁
# print(two_char_emoji_1)     # fail
# print(sp(two_char_emoji_1)) # success
# print(two_char_emoji_2)     # success
#
# single_surrogate = '\uD83D' # illegal
# print(single_surrogate)     # fail
# print(sp(single_surrogate)) # success as \uFFFD
#
# python -> dotnet : a single surrogate (e.g. \uD83D) will pass through as the replacement character (\uFFFD)
# python -> dotnet : a surrogate pair (e.g. \uD83D\uDE01 or \U0001F601) will pass through correctly
# dotnet -> python : a single surrogate (e.g. \uD83D) will pass through as the replacement character (\uFFFD)
# dotnet -> python : a surrogate pair will pass through individually (e.g. \uD83D\uDE01) so we need the sp function to deal with it
```

The above was tested with Python 3.8 and [Python.NET](http://pythonnet.github.io/)

Supported [Python escape sequences](https://docs.python.org/3/reference/lexical_analysis.html#string-and-bytes-literals):
* `\\` (Backslash)
* `\'` (Single quote)
* `\"` (Double quote)
* `\a` (Bell)
* `\b` (Backspace)
* `\f` (Formfeed)
* `\n` (Linefeed)
* `\r` (Carriage return)
* `\t` (Horizontal tab)
* `\v` (Vertical tab)
* `\O` or `\OO` or `\OOO` (Variable length octal escape sequence; 0-777)
* `\uHHHH` (16-bit hex value)
* `\UHHHHHHHH` (32-bit hex value)
* `\N{name}` (Character named *name* in the Unicode database)
  * This requires [UnicodeInformation](https://www.nuget.org/packages/UnicodeInformation/)
