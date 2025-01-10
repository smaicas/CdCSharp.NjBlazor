namespace CdCSharp.NjBlazor.Features.Controls.Components.Search;

/// <summary>
/// Contains a collection of algorithms for searching and filtering data.
/// </summary>
public static class SearchFilterAlgorithms
{
    /// <summary>
    /// Determines if two strings are a fuzzy match within a specified tolerance.
    /// </summary>
    /// <param name="source">The source string to compare.</param>
    /// <param name="target">The target string to compare against.</param>
    /// <param name="tolerance">The maximum number of allowed differences between the strings (default is 1).</param>
    /// <returns>True if the strings are a fuzzy match within the tolerance, false otherwise.</returns>
    public static bool IsFuzzyMatch(string source, string target, int tolerance = 1)
    {
        int mismatchCount = 0;
        int minLength = Math.Min(source.Length, target.Length);

        for (int i = 0; i < minLength; i++)
            if (source[i] != target[i])
            {
                mismatchCount++;
                if (mismatchCount > tolerance)
                    return false;
            }

        return true;
    }

    /// <summary>
    /// Calculates the Levenshtein distance between two strings.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="target">The target string.</param>
    /// <returns>The Levenshtein distance between the two strings.</returns>
    public static int LevenshteinDistance(string source, string target)
    {
        if (string.IsNullOrEmpty(source))
            return target.Length;

        if (string.IsNullOrEmpty(target))
            return source.Length;

        int[,] distance = new int[source.Length + 1, target.Length + 1];

        for (int i = 0; i <= source.Length; i++)
            distance[i, 0] = i;

        for (int j = 0; j <= target.Length; j++)
            distance[0, j] = j;

        for (int i = 1; i <= source.Length; i++)
            for (int j = 1; j <= target.Length; j++)
            {
                int cost = source[i - 1] == target[j - 1] ? 0 : 1;

                distance[i, j] = Math.Min(
                    Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                    distance[i - 1, j - 1] + cost
                );
            }

        return distance[source.Length, target.Length];
    }
}
