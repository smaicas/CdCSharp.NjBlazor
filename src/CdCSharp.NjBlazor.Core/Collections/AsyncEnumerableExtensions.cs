namespace CdCSharp.NjBlazor.Core.Collections;

public static class AsyncEnumerableExtensions
{
    /// <summary> Converts an <see cref="IAsyncEnumerable{T}"/> into a <see cref="List{T}"/>
    /// asynchronously. </summary> <typeparam name="T">The type of elements in the source
    /// sequence.</typeparam> <param name="source">The asynchronous enumerable sequence to
    /// convert.</param> <returns> A <see cref="Task{TResult}"/> representing the asynchronous
    /// operation, with the result being a <see cref="List{T}"/> containing all the elements from
    /// the source sequence. </returns> <exception cref="ArgumentNullException"> Thrown if the
    /// <paramref name="source"/> is <c>null</c>. </exception> <example> The following example
    /// demonstrates how to use the <c>ToListAsync</c> method: <code> IAsyncEnumerable<int>
    /// asyncNumbers = GetNumbersAsync(); List<int> numbers = await asyncNumbers.ToListAsync();
    /// </code> </example>
    public static Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);

        return ExecuteAsync();

        async Task<List<T>> ExecuteAsync()
        {
            List<T> list = [];

            await foreach (T? element in source.WithCancellation(cancellationToken))
                list.Add(element);

            return list;
        }
    }
}