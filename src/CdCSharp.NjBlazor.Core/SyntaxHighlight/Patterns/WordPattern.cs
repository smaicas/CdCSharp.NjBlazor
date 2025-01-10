using System.Text.RegularExpressions;

namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;

/// <summary>
/// Represents a word pattern that extends a base pattern class.
/// </summary>
public sealed class WordPattern : Pattern
{
    /// <summary>
    /// Initializes a new instance of the WordPattern class with the specified name, style, and words.
    /// </summary>
    /// <param name="name">
    /// The name of the WordPattern.
    /// </param>
    /// <param name="style">
    /// The style of the WordPattern.
    /// </param>
    /// <param name="words">
    /// An enumerable collection of strings representing words in the pattern.
    /// </param>
    public WordPattern(string name, Style style, IEnumerable<string> words)
        : base(name, style) => Words = words;

    /// <summary>
    /// Gets the collection of words.
    /// </summary>
    /// <value>
    /// The collection of words.
    /// </value>
    public IEnumerable<string> Words { get; private set; }

    /// <summary>
    /// Generates a regex pattern based on the words provided.
    /// </summary>
    /// <returns>
    /// A regex pattern that matches the specified words.
    /// </returns>
    public override string GetRegexPattern()
    {
        string str = string.Empty;
        if (Words.Count() > 0)
        {
            string nonWords = GetNonWords();
            str = string.Format(@"(?<![\w{0}])(?=[\w{0}])({1})(?<=[\w{0}])(?![\w{0}])", nonWords, string.Join("|", Words.Select(w => Regex.Escape(w)).ToArray()));
        }

        return str;
    }

    private string GetNonWords()
    {
        string input = string.Join("", Words.ToArray());
        List<string> list = [];
        foreach (Match? match in Regex.Matches(input, @"\W").Cast<Match>().Where(x => !list.Contains(x.Value)))
            list.Add(match.Value);

        return string.Join("", list.ToArray());
    }
}