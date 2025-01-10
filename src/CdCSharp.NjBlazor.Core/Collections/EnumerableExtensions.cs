namespace CdCSharp.NjBlazor.Core.Collections;

public static class EnumerableExtensions
{
    /// <summary>
    /// Enumerates a sequence and returns each element of the sequence along with its index.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the source sequence.
    /// </typeparam>
    /// <param name="source">
    /// The sequence to enumerate.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}" /> of tuples, where each tuple contains an element of the
    /// source sequence and its zero-based index.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <paramref name="source" /> sequence is <c> null </c>.
    /// </exception>
    /// <example>
    /// Example usage:
    /// <code>
    ///var items = new[] { "a", "b", "c" };
    ///foreach (var (value, index) in items.WithIndex())
    ///{
    ///Console.WriteLine($"Value: {value}, Index: {index}");
    ///}
    /// </code>
    /// Output:
    /// Value: a, Index: 0
    /// Value: b, Index: 1
    /// Value: c, Index: 2
    /// </example>
    public static IEnumerable<(T value, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        int i = 0;
        foreach (T o in source)
            yield return (o, i++);
    }
}