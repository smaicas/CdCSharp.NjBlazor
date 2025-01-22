using System.Text;
using System.Text.RegularExpressions;

namespace CdCSharp.NjBlazor.Core.Strings;

public static partial class StringExtensions
{
    public interface IStringTransformer
    {
        string Transform(string source);
    }

    /// <summary>
    /// Transforms a string input into a regular expression pattern by processing it with specific
    /// regex rules.
    /// </summary>
    /// <param name="input">
    /// The input string to be transformed into a regex pattern.
    /// </param>
    /// <returns>
    /// A regular expression pattern string that matches the processed version of the input string.
    /// The resulting pattern ensures that it starts with "^", ends with "$", and substitutes
    /// certain substrings with regex symbols (\w for word characters, \d for digits).
    /// </returns>
    /// <example>
    /// Given an input string "abc123", this method returns a pattern like: ^\w\w\w\d\d\d$
    /// </example>
    public static string ExtractRegex(this string input)
    {
        string output = WordOrDigitGroup().Replace(input, "($1)");
        output = NotBetweenParentheses().Replace(output, "($1)");
        output = Word().Replace(output, @"\w");
        output = Digit().Replace(output, @"\d");
        return $"^{output}$";
    }

    /// <summary> Combines a collection of strings into a single string, separating each non-empty
    /// element with the specified separator. </summary> <param name="strings"> A collection of
    /// strings to be joined. Strings that are <c>null</c>, empty, or consist only of whitespace
    /// will be excluded. </param> <param name="separator"> A separator to place between non-empty
    /// elements. The default value is a single space (" "). </param> <returns> A string containing
    /// the non-empty elements of the original collection, joined by the specified separator. If all
    /// elements in the collection are empty, null, or whitespace, an empty string is returned.
    /// </returns> <remarks> This method uses a <see cref="StringBuilder"/> to optimize string
    /// concatenation. </remarks> <example> Example usage: <code> IReadOnlyCollection<string>
    /// strings = new List<string> { "Hello", "", "World", " ", null, "!" }; string result =
    /// strings.NotEmptyJoin(", "); // Result: "Hello, World, !" </code> </example>
    public static string NotEmptyJoin(this IReadOnlyCollection<string?> strings, string separator = " ")
    {
        StringBuilder builder = new();
        bool first = true;

        foreach (string? s in strings)
            if (!string.IsNullOrWhiteSpace(s))
            {
                if (!first)
                    builder.Append(separator);
                builder.Append(s);
                first = false;
            }

        return builder.ToString();
    }

    /// <summary> Transforms the input string using a specified string transformer. </summary>
    /// <typeparam name="TStringTransformer"> The type of the transformer that implements the <see
    /// cref="IStringTransformer"/> interface. This type must have a parameterless constructor.
    /// </typeparam> <param name="source">The input string to be transformed.</param> <returns>The
    /// transformed string as defined by the implementation of <see
    /// cref="IStringTransformer"/>.</returns> <exception cref="MissingMethodException"> Thrown if
    /// <typeparamref name="TStringTransformer"/> does not have a parameterless constructor.
    /// </exception> <exception cref="InvalidOperationException"> Thrown if an instance of
    /// <typeparamref name="TStringTransformer"/> cannot be created. </exception> <example> This
    /// example demonstrates how to use the <c>Transform</c> method: <code> public class
    /// UppercaseTransformer : IStringTransformer { public string Transform(string input) =>
    /// input.ToUpper(); }
    ///
    /// string input = "hello world"; string result = input.Transform<UppercaseTransformer>(); //
    /// result is "HELLO WORLD" </code> </example>
    public static string Transform<TStringTransformer>(this string source)
        where TStringTransformer : IStringTransformer
    {
        IStringTransformer transformer = Activator.CreateInstance<TStringTransformer>();
        return transformer.Transform(source);
    }

    /// <summary>
    /// Matches individual numeric digits (0-9) in the input string.
    /// </summary>
    /// <returns>
    /// A regex that identifies single digit characters.
    /// </returns>
    [GeneratedRegex("[0-9]")]
    private static partial Regex Digit();

    /// <summary>
    /// Matches substrings that are not enclosed within parentheses. This regex is designed to
    /// exclude content that is inside balanced parentheses.
    /// </summary>
    /// <returns>
    /// A regex that identifies text outside parentheses.
    /// </returns>
    [GeneratedRegex(@"((?<!\([^)]*)[^()]+(?![^(]*\)))")]
    private static partial Regex NotBetweenParentheses();

    /// <summary>
    /// Matches individual alphabetic characters (A-Z or a-z) in the input string.
    /// </summary>
    /// <returns>
    /// A regex that identifies single word characters.
    /// </returns>
    [GeneratedRegex("[a-zA-Z]")]
    private static partial Regex Word();

    /// <summary>
    /// Matches groups of word or digit characters (letters or digits) in the input string.
    /// </summary>
    /// <returns>
    /// A regex that identifies sequences of letters and digits.
    /// </returns>
    [GeneratedRegex("([a-zA-Z0-9]+)")]
    private static partial Regex WordOrDigitGroup();

    public class CamelCaseStringTransformer : IStringTransformer
    {
        public string Transform(string source) => ConvertToCamelCase(source);

        private string ConvertToCamelCase(string source)
        {
            if (string.IsNullOrEmpty(source)) return source;

            string[] words = Regex.Split(source, @"[^\w+]|[_+]").Where(w => !string.IsNullOrEmpty(w)).ToArray();

            // Convert the first word to lowercase entirely
            string camelCaseText = words[0].ToLower();

            // Convert subsequent words to PascalCase and append them
            for (int i = 1; i < words.Length; i++)
            {
                string word = words[i].ToLower();
                camelCaseText += char.ToUpper(word[0]) + word[1..];
            }

            return camelCaseText;
        }
    }

    public class AbbreviateCamelCaseTransformer : IStringTransformer
    {
        public string Transform(string source) => Abbreviate(source);

        private string Abbreviate(string source)
        {
            if (string.IsNullOrEmpty(source)) return source;

            return string.Join("", Regex.Split(source, @"(?<!^)(?=[A-Z])").Select(v => v.Take(2)));
        }
    }
}