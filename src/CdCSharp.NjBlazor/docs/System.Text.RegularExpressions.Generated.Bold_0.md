# Bold_0

*Namespace:* System.Text.RegularExpressions.Generated
*Assembly:* CdCSharp.NjBlazor
*Source:* RegexGenerator.g.cs


Custom [T:System.Text.RegularExpressions.Regex]-derived type for the Bold method.
---
---
## Inherited from Regex

**Summary:**
Represents an immutable regular expression.
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor()`

Initializes a new instance of the [T:System.Text.RegularExpressions.Regex] class.



**Method:** `.ctor`
*Method Signature:* `Void .ctor(SerializationInfo info, StreamingContext context)`

Initializes a new instance of the [T:System.Text.RegularExpressions.Regex] class by using serialized data.



**Method:** `.ctor`
*Method Signature:* `Void .ctor(String pattern)`

Initializes a new instance of the [T:System.Text.RegularExpressions.Regex] class for the specified regular expression.



**Method:** `.ctor`
*Method Signature:* `Void .ctor(String pattern, RegexOptions options)`

Initializes a new instance of the [T:System.Text.RegularExpressions.Regex] class for the specified regular expression, with options that modify the pattern.



**Method:** `.ctor`
*Method Signature:* `Void .ctor(String pattern, RegexOptions options, TimeSpan matchTimeout)`

Initializes a new instance of the [T:System.Text.RegularExpressions.Regex] class for the specified regular expression, with options that modify the pattern and a value that specifies how long a pattern matching method should attempt a match before it times out.



**Method:** `CompileToAssembly`
*Method Signature:* `Void CompileToAssembly( regexinfos, AssemblyName assemblyname)`

Compiles one or more specified [T:System.Text.RegularExpressions.Regex] objects to a named assembly.



**Method:** `CompileToAssembly`
*Method Signature:* `Void CompileToAssembly( regexinfos, AssemblyName assemblyname,  attributes)`

Compiles one or more specified [T:System.Text.RegularExpressions.Regex] objects to a named assembly with the specified attributes.



**Method:** `CompileToAssembly`
*Method Signature:* `Void CompileToAssembly( regexinfos, AssemblyName assemblyname,  attributes, String resourceFile)`

Compiles one or more specified [T:System.Text.RegularExpressions.Regex] objects and a specified resource file to a named assembly with the specified attributes.



**Method:** `Count`
*Method Signature:* `Int32 Count(String input)`

Searches an input string for all occurrences of a regular expression and returns the number of matches.



**Method:** `Count`
*Method Signature:* `Int32 Count(ReadOnlySpan input)`

Searches an input span for all occurrences of a regular expression and returns the number of matches.



**Method:** `Count`
*Method Signature:* `Int32 Count(ReadOnlySpan input, Int32 startat)`

Searches an input span for all occurrences of a regular expression and returns the number of matches.



**Method:** `Count`
*Method Signature:* `Int32 Count(String input, String pattern)`

Searches an input string for all occurrences of a regular expression and returns the number of matches.



**Method:** `Count`
*Method Signature:* `Int32 Count(String input, String pattern, RegexOptions options)`

Searches an input string for all occurrences of a regular expression and returns the number of matches.



**Method:** `Count`
*Method Signature:* `Int32 Count(String input, String pattern, RegexOptions options, TimeSpan matchTimeout)`

Searches an input string for all occurrences of a regular expression and returns the number of matches.



**Method:** `Count`
*Method Signature:* `Int32 Count(ReadOnlySpan input, String pattern)`

Searches an input span for all occurrences of a regular expression and returns the number of matches.



**Method:** `Count`
*Method Signature:* `Int32 Count(ReadOnlySpan input, String pattern, RegexOptions options)`

Searches an input span for all occurrences of a regular expression and returns the number of matches.



**Method:** `Count`
*Method Signature:* `Int32 Count(ReadOnlySpan input, String pattern, RegexOptions options, TimeSpan matchTimeout)`

Searches an input span for all occurrences of a regular expression and returns the number of matches.



**Method:** `Escape`
*Method Signature:* `String Escape(String str)`

Escapes a minimal set of characters (\, *, +, ?, |, {, [, (,), ^, $, ., #, and white space) by replacing them with their escape codes. This instructs the regular expression engine to interpret these characters literally rather than as metacharacters.



**Method:** `EnumerateMatches`
*Method Signature:* `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan input)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueMatchEnumerator] to iterate over the matches.



**Method:** `EnumerateMatches`
*Method Signature:* `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan input, Int32 startat)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueMatchEnumerator] to iterate over the matches.



**Method:** `EnumerateMatches`
*Method Signature:* `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan input, String pattern)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueMatchEnumerator] to iterate over the matches.



**Method:** `EnumerateMatches`
*Method Signature:* `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan input, String pattern, RegexOptions options)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueMatchEnumerator] to iterate over the matches.



**Method:** `EnumerateMatches`
*Method Signature:* `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan input, String pattern, RegexOptions options, TimeSpan matchTimeout)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueMatchEnumerator] to iterate over the matches.



**Method:** `EnumerateSplits`
*Method Signature:* `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan input)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueSplitEnumerator] to iterate over the splits around matches.



**Method:** `EnumerateSplits`
*Method Signature:* `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan input, Int32 count)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueSplitEnumerator] to iterate over the splits around matches.



**Method:** `EnumerateSplits`
*Method Signature:* `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan input, Int32 count, Int32 startat)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueSplitEnumerator] to iterate over the splits around matches.



**Method:** `EnumerateSplits`
*Method Signature:* `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan input, String pattern)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueSplitEnumerator] to iterate over the splits around matches.



**Method:** `EnumerateSplits`
*Method Signature:* `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan input, String pattern, RegexOptions options)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueSplitEnumerator] to iterate over the splits around matches.



**Method:** `EnumerateSplits`
*Method Signature:* `ValueSplitEnumerator EnumerateSplits(ReadOnlySpan input, String pattern, RegexOptions options, TimeSpan matchTimeout)`

Searches an input span for all occurrences of a regular expression and returns a [T:System.Text.RegularExpressions.Regex.ValueSplitEnumerator] to iterate over the splits around matches.



**Method:** `GetGroupNames`
*Method Signature:* ` GetGroupNames()`

Returns an array of capturing group names for the regular expression.



**Method:** `GetGroupNumbers`
*Method Signature:* ` GetGroupNumbers()`

Returns an array of capturing group numbers that correspond to group names in an array.



**Method:** `GroupNameFromNumber`
*Method Signature:* `String GroupNameFromNumber(Int32 i)`

Gets the group name that corresponds to the specified group number.



**Method:** `GroupNumberFromName`
*Method Signature:* `Int32 GroupNumberFromName(String name)`

Returns the group number that corresponds to the specified group name.



**Method:** `InitializeReferences`
*Method Signature:* `Void InitializeReferences()`

Used by a [T:System.Text.RegularExpressions.Regex] object generated by the [Overload:System.Text.RegularExpressions.Regex.CompileToAssembly] method.



**Method:** `IsMatch`
*Method Signature:* `Boolean IsMatch(ReadOnlySpan input)`

Indicates whether the regular expression specified in the Regex constructor finds a match in a specified input span.



**Method:** `IsMatch`
*Method Signature:* `Boolean IsMatch(ReadOnlySpan input, Int32 startat)`

Indicates whether the regular expression specified in the Regex constructor finds a match in a specified input span.



**Method:** `IsMatch`
*Method Signature:* `Boolean IsMatch(ReadOnlySpan input, String pattern)`

Indicates whether the specified regular expression finds a match in the specified input span.



**Method:** `IsMatch`
*Method Signature:* `Boolean IsMatch(ReadOnlySpan input, String pattern, RegexOptions options)`

Indicates whether the specified regular expression finds a match in the specified input span, using the specified matching options.



**Method:** `IsMatch`
*Method Signature:* `Boolean IsMatch(ReadOnlySpan input, String pattern, RegexOptions options, TimeSpan matchTimeout)`

Indicates whether the specified regular expression finds a match in the specified input span, using the specified matching options and time-out interval.



**Method:** `IsMatch`
*Method Signature:* `Boolean IsMatch(String input)`

Indicates whether the regular expression specified in the [T:System.Text.RegularExpressions.Regex] constructor finds a match in a specified input string.



**Method:** `IsMatch`
*Method Signature:* `Boolean IsMatch(String input, Int32 startat)`

Indicates whether the regular expression specified in the [T:System.Text.RegularExpressions.Regex] constructor finds a match in the specified input string, beginning at the specified starting position in the string.



**Method:** `IsMatch`
*Method Signature:* `Boolean IsMatch(String input, String pattern)`

Indicates whether the specified regular expression finds a match in the specified input string.



**Method:** `IsMatch`
*Method Signature:* `Boolean IsMatch(String input, String pattern, RegexOptions options)`

Indicates whether the specified regular expression finds a match in the specified input string, using the specified matching options.



**Method:** `IsMatch`
*Method Signature:* `Boolean IsMatch(String input, String pattern, RegexOptions options, TimeSpan matchTimeout)`

Indicates whether the specified regular expression finds a match in the specified input string, using the specified matching options and time-out interval.



**Method:** `Match`
*Method Signature:* `Match Match(String input)`

Searches the specified input string for the first occurrence of the regular expression specified in the [T:System.Text.RegularExpressions.Regex] constructor.



**Method:** `Match`
*Method Signature:* `Match Match(String input, Int32 startat)`

Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position in the string.



**Method:** `Match`
*Method Signature:* `Match Match(String input, Int32 beginning, Int32 length)`

Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position and searching only the specified number of characters.



**Method:** `Match`
*Method Signature:* `Match Match(String input, String pattern)`

Searches the specified input string for the first occurrence of the specified regular expression.



**Method:** `Match`
*Method Signature:* `Match Match(String input, String pattern, RegexOptions options)`

Searches the input string for the first occurrence of the specified regular expression, using the specified matching options.



**Method:** `Match`
*Method Signature:* `Match Match(String input, String pattern, RegexOptions options, TimeSpan matchTimeout)`

Searches the input string for the first occurrence of the specified regular expression, using the specified matching options and time-out interval.



**Method:** `Matches`
*Method Signature:* `MatchCollection Matches(String input)`

Searches the specified input string for all occurrences of a regular expression.



**Method:** `Matches`
*Method Signature:* `MatchCollection Matches(String input, Int32 startat)`

Searches the specified input string for all occurrences of a regular expression, beginning at the specified starting position in the string.



**Method:** `Matches`
*Method Signature:* `MatchCollection Matches(String input, String pattern)`

Searches the specified input string for all occurrences of a specified regular expression.



**Method:** `Matches`
*Method Signature:* `MatchCollection Matches(String input, String pattern, RegexOptions options)`

Searches the specified input string for all occurrences of a specified regular expression, using the specified matching options.



**Method:** `Matches`
*Method Signature:* `MatchCollection Matches(String input, String pattern, RegexOptions options, TimeSpan matchTimeout)`

Searches the specified input string for all occurrences of a specified regular expression, using the specified matching options and time-out interval.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, String replacement)`

In a specified input string, replaces all strings that match a regular expression pattern with a specified replacement string.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, String replacement, Int32 count)`

In a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, String replacement, Int32 count, Int32 startat)`

In a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, String pattern, String replacement)`

In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, String pattern, String replacement, RegexOptions options)`

In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Specified options modify the matching operation.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, String pattern, String replacement, RegexOptions options, TimeSpan matchTimeout)`

In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, String pattern, MatchEvaluator evaluator)`

In a specified input string, replaces all strings that match a specified regular expression with a string returned by a [T:System.Text.RegularExpressions.MatchEvaluator] delegate.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, String pattern, MatchEvaluator evaluator, RegexOptions options)`

In a specified input string, replaces all strings that match a specified regular expression with a string returned by a [T:System.Text.RegularExpressions.MatchEvaluator] delegate. Specified options modify the matching operation.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, String pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan matchTimeout)`

In a specified input string, replaces all substrings that match a specified regular expression with a string returned by a [T:System.Text.RegularExpressions.MatchEvaluator] delegate. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, MatchEvaluator evaluator)`

In a specified input string, replaces all strings that match a specified regular expression with a string returned by a [T:System.Text.RegularExpressions.MatchEvaluator] delegate.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, MatchEvaluator evaluator, Int32 count)`

In a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a [T:System.Text.RegularExpressions.MatchEvaluator] delegate.



**Method:** `Replace`
*Method Signature:* `String Replace(String input, MatchEvaluator evaluator, Int32 count, Int32 startat)`

In a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a [T:System.Text.RegularExpressions.MatchEvaluator] delegate.



**Method:** `Split`
*Method Signature:* ` Split(String input)`

Splits an input string into an array of substrings at the positions defined by a regular expression pattern specified in the [T:System.Text.RegularExpressions.Regex] constructor.



**Method:** `Split`
*Method Signature:* ` Split(String input, Int32 count)`

Splits an input string a specified maximum number of times into an array of substrings, at the positions defined by a regular expression specified in the [T:System.Text.RegularExpressions.Regex] constructor.



**Method:** `Split`
*Method Signature:* ` Split(String input, Int32 count, Int32 startat)`

Splits an input string a specified maximum number of times into an array of substrings, at the positions defined by a regular expression specified in the [T:System.Text.RegularExpressions.Regex] constructor. The search for the regular expression pattern starts at a specified character position in the input string.



**Method:** `Split`
*Method Signature:* ` Split(String input, String pattern)`

Splits an input string into an array of substrings at the positions defined by a regular expression pattern.



**Method:** `Split`
*Method Signature:* ` Split(String input, String pattern, RegexOptions options)`

Splits an input string into an array of substrings at the positions defined by a specified regular expression pattern. Specified options modify the matching operation.



**Method:** `Split`
*Method Signature:* ` Split(String input, String pattern, RegexOptions options, TimeSpan matchTimeout)`

Splits an input string into an array of substrings at the positions defined by a specified regular expression pattern. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.



**Method:** `ToString`
*Method Signature:* `String ToString()`

Returns the regular expression pattern that was passed into the  constructor.



**Method:** `Unescape`
*Method Signature:* `String Unescape(String str)`

Converts any escaped characters in the input string.



**Method:** `UseOptionC`
*Method Signature:* `Boolean UseOptionC()`

Used by a [T:System.Text.RegularExpressions.Regex] object generated by the [Overload:System.Text.RegularExpressions.Regex.CompileToAssembly] method.



**Method:** `UseOptionR`
*Method Signature:* `Boolean UseOptionR()`

Used by a [T:System.Text.RegularExpressions.Regex] object generated by the [Overload:System.Text.RegularExpressions.Regex.CompileToAssembly] method.



**Method:** `ValidateMatchTimeout`
*Method Signature:* `Void ValidateMatchTimeout(TimeSpan matchTimeout)`

Checks whether a time-out interval is within an acceptable range.



**Property:** `CacheSize` (Public)

Gets or sets the maximum number of entries in the current static cache of compiled regular expressions.

*Property Type:* `Int32`
*Nullable:* False
*Attributes:* 


**Property:** `CapNames` (Protected)

Gets or sets a dictionary that maps named capturing groups to their index values.

*Property Type:* `IDictionary`
*Nullable:* True
*Attributes:* [NullableAttribute], [CLSCompliantAttribute]


**Property:** `Caps` (Protected)

Gets or sets a dictionary that maps numbered capturing groups to their index values.

*Property Type:* `IDictionary`
*Nullable:* True
*Attributes:* [NullableAttribute], [CLSCompliantAttribute]


**Property:** `MatchTimeout` (Public)

Gets the time-out interval of the current instance.

*Property Type:* `TimeSpan`
*Nullable:* False
*Attributes:* 


**Property:** `Options` (Public)

Gets the options that were passed into the [T:System.Text.RegularExpressions.Regex] constructor.

*Property Type:* `RegexOptions`
*Nullable:* False
*Attributes:* 


**Property:** `RightToLeft` (Public)

Gets a value that indicates whether the regular expression searches from right to left.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* 

