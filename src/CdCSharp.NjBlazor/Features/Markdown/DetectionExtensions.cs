namespace CdCSharp.NjBlazor.Features.Markdown;

/// <summary>
/// Contains extension methods for detection-related functionalities.
/// </summary>
internal static partial class DetectionExtensions
{
    /// <summary>
    /// Determines if a given string represents a block quote.
    /// </summary>
    /// <param name="line">
    /// The string to check for block quote formatting.
    /// </param>
    /// <returns>
    /// True if the string represents a block quote; otherwise, false.
    /// </returns>
    internal static bool IsBlockQuote(this string line) => line.TrimStart().StartsWith("> ");

    /// <summary>
    /// Checks if a given string represents a code block.
    /// </summary>
    /// <param name="line">
    /// The string to check.
    /// </param>
    /// <returns>
    /// True if the string represents a code block, false otherwise.
    /// </returns>
    internal static bool IsCodeBlock(this string line) => line.TrimStart().StartsWith("```");

    /// <summary>
    /// Checks if a given string represents a header.
    /// </summary>
    /// <param name="line">
    /// The string to be checked.
    /// </param>
    /// <returns>
    /// True if the string represents a header, false otherwise.
    /// </returns>
    internal static bool IsHeader(this string line)
    {
        return line.TrimStart().StartsWith("# ")
                || line.TrimStart().StartsWith("## ")
                || line.TrimStart().StartsWith("### ")
                || line.TrimStart().StartsWith("#### ")
                || line.TrimStart().StartsWith("##### ")
                || line.TrimStart().StartsWith("###### ");
    }

    /// <summary>
    /// Checks if a given string represents a line separator.
    /// </summary>
    /// <param name="line">
    /// The string to check.
    /// </param>
    /// <returns>
    /// True if the string represents a line separator, false otherwise.
    /// </returns>
    internal static bool IsLineSeparator(this string line) => line.Trim().Equals("***") || line.Trim().Equals("---") || line.Trim().Equals("___");

    /// <summary>
    /// Checks if a string represents a paragraph.
    /// </summary>
    /// <param name="line">
    /// The string to check.
    /// </param>
    /// <returns>
    /// True if the string represents a paragraph, false otherwise.
    /// </returns>
    internal static bool IsParagraph(this string line) => !line.IsHeader() && !line.IsUnorderedList() && !line.IsBlockQuote() && !line.IsLineSeparator() && !line.IsCodeBlock();

    /// <summary>
    /// Checks if a string represents an unordered list item.
    /// </summary>
    /// <param name="line">
    /// The string to check.
    /// </param>
    /// <returns>
    /// True if the string represents an unordered list item, false otherwise.
    /// </returns>
    internal static bool IsUnorderedList(this string line)
    {
        return line.TrimStart().StartsWith("* ")
            || line.TrimStart().StartsWith("- ")
            || line.TrimStart().StartsWith("+ ");
    }
}