# Net.Laceous.Utilities

This currently contains char and string utilities targeting [.NET Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

## Char and string escaping

### C#

```csharp
Console.OutputEncoding = Encoding.UTF8; // use a terminal that supports emojis

CharEscapeOptions ceOptions = new CharEscapeOptions(escapeLanguage: CharEscapeLanguage.CSharp, escapeLetter: CharEscapeLetter.LowerCaseU4, escapeLetterFallback: CharEscapeLetter.LowerCaseU4, surrogatePairEscapeLetter: CharEscapeLetter.UpperCaseU8, surrogatePairEscapeLetterFallback: CharEscapeLetter.UpperCaseU8, useLowerCaseHex: false, useShortEscape: true);
StringEscapeOptions seOptions = new StringEscapeOptions(escapeKind: StringEscapeKind.EscapeNonAscii, escapeSurrogatePairs: true);
CharUnescapeOptions uOptions = new CharUnescapeOptions(escapeLanguage: CharEscapeLanguage.CSharp, isUnrecognizedEscapeVerbatim: true);

char cOriginal = 'Ä';
string cEscaped = CharUtils.Escape(cOriginal, escapeOptions: ceOptions, addQuotes: true);
char cUnescaped = CharUtils.Unescape(cEscaped, unescapeOptions: uOptions, removeQuotes: true);
Console.WriteLine(cEscaped);   // '\u00C4'
Console.WriteLine(cUnescaped); // Ä

string eOriginal = "😁"; // 2 char emoji
string eEscaped = CharUtils.EscapeSurrogatePair(eOriginal, escapeOptions: ceOptions, addQuotes: true);
string eUnescaped = CharUtils.UnescapeSurrogatePair(eEscaped, unescapeOptions: uOptions, removeQuotes: true);
Console.WriteLine(eEscaped);   // "\U0001F601"
Console.WriteLine(eUnescaped); // 😁

string sOriginal = "abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓";
string sEscaped = StringUtils.Escape(sOriginal, stringEscapeOptions: seOptions, charEscapeOptions: ceOptions, addQuotes: true);
string sUnescaped = StringUtils.Unescape(sEscaped, unescapeOptions: uOptions, removeQuotes: true);
Console.WriteLine(sEscaped);   // "abc ABC 123 \u00C4\u00D6\u00DC \u3131\u3134\u3137 \U0001F601\U0001F603\U0001F613"
Console.WriteLine(sUnescaped); // abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
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

Supported quote types:
* `'Char'`
* `"String"`

### F#

```fsharp
Console.OutputEncoding <- Encoding.UTF8 // use a terminal that supports emojis

let ceOptions = new CharEscapeOptions(escapeLanguage = CharEscapeLanguage.FSharp, escapeLetter = CharEscapeLetter.LowerCaseU4, escapeLetterFallback = CharEscapeLetter.LowerCaseU4, surrogatePairEscapeLetter = CharEscapeLetter.UpperCaseU8, surrogatePairEscapeLetterFallback = CharEscapeLetter.UpperCaseU8, useLowerCaseHex = false, useShortEscape = true)
let seOptions = new StringEscapeOptions(escapeKind = StringEscapeKind.EscapeNonAscii, escapeSurrogatePairs = true)
let uOptions = new CharUnescapeOptions(escapeLanguage = CharEscapeLanguage.FSharp, isUnrecognizedEscapeVerbatim = true)

let cOriginal = 'Ä'
let cEscaped = CharUtils.Escape(cOriginal, escapeOptions = ceOptions, addQuotes = true)
let cUnescaped = CharUtils.Unescape(cEscaped, unescapeOptions = uOptions, removeQuotes = true)
printfn "%s" cEscaped   // '\u00C4'
printfn "%c" cUnescaped // Ä

let eOriginal = "😁"; // 2 char emoji
let eEscaped = CharUtils.EscapeSurrogatePair(eOriginal, escapeOptions = ceOptions, addQuotes = true)
let eUnescaped = CharUtils.UnescapeSurrogatePair(eEscaped, unescapeOptions = uOptions, removeQuotes = true)
printfn "%s" eEscaped   // "\U0001F601"
printfn "%s" eUnescaped // 😁

let sOriginal = "abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓"
let sEscaped = StringUtils.Escape(sOriginal, stringEscapeOptions = seOptions, charEscapeOptions = ceOptions, addQuotes = true)
let sUnescaped = StringUtils.Unescape(sEscaped, unescapeOptions = uOptions, removeQuotes = true)
printfn "%s" sEscaped   // "abc ABC 123 \u00C4\u00D6\u00DC \u3131\u3134\u3137 \U0001F601\U0001F603\U0001F613"
printfn "%s" sUnescaped // abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
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

Supported quote types:
* `'Char'`
* `"String"`

### PowerShell

```powershell
Add-Type -Path '/path/to/Net.Laceous.Utilities.dll'

$ceOptions = [Net.Laceous.Utilities.CharEscapeOptions]::New('PowerShell', 'LowerCaseU4', 'LowerCaseU4', 'LowerCaseU5', 'LowerCaseU5', $false, $true)
$seOptions = [Net.Laceous.Utilities.StringEscapeOptions]::New('EscapeNonAscii', $true)
$uOptions = [Net.Laceous.Utilities.CharUnescapeOptions]::New('PowerShell', $true)

$cOriginal = "Ä"
$cEscaped = [Net.Laceous.Utilities.CharUtils]::Escape($cOriginal, $ceOptions, $true)
$cUnescaped = [Net.Laceous.Utilities.CharUtils]::Unescape($cEscaped, $uOptions, $true)
Write-Host $cEscaped   # "`u{00C4}"
Write-Host $cUnescaped # Ä

$eOriginal = "😁" # 2 char emoji
$eEscaped = [Net.Laceous.Utilities.CharUtils]::EscapeSurrogatePair($eOriginal, $ceOptions, $true)
$eUnescaped = [Net.Laceous.Utilities.CharUtils]::UnescapeSurrogatePair($eEscaped, $uOptions, $true)
Write-Host $eEscaped   # "`u{1F601}"
Write-Host $eUnescaped # 😁

$sOriginal = '$abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓'
$sEscaped = [Net.Laceous.Utilities.StringUtils]::Escape($sOriginal, $seOptions, $ceOptions, $true)
$sUnescaped = [Net.Laceous.Utilities.StringUtils]::Unescape($sEscaped, $uOptions, $true)
Write-Host $sEscaped   # "abc ABC 123 `u{00C4}`u{00D6}`u{00DC} `u{3131}`u{3134}`u{3137} `u{1F601}`u{1F603}`u{1F613}"
Write-Host $sUnescaped # abc ABC 123 ÄÖÜ ㄱㄴㄷ 😁😃😓
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
* ``` `` ``` (Backtick - listed [here](https://docs.microsoft.com/en-us/powershell/scripting/developer/cmdlet/supporting-wildcard-characters-in-cmdlet-parameters?view=powershell-7.1))
* `` `" `` (Double quote - listed [here](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_quoting_rules?view=powershell-7.1))
* `` `u{H} `` or `` `u{HH} `` or `` `u{HHH} `` or `` `u{HHHH} `` or `` `u{HHHHH} `` or `` `u{HHHHHH} `` (Variable length unicode escape sequence)

Supported quote types:
* `"String"` - interpolation isn't supported
