using System.Collections.Concurrent;

namespace CdCSharp.NjBlazor.Core.Collections;

public static class EnumExtensions
{
    /// <summary>
    /// Retrieves the next value in the enumeration, wrapping around to the first value if the
    /// current value is the last.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the enumeration. Must be a struct and an <see cref="Enum" />.
    /// </typeparam>
    /// <param name="src">
    /// The current enumeration value.
    /// </param>
    /// <returns>
    /// The next enumeration value. If the current value is the last in the enumeration, the method
    /// returns the first value.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <typeparamref name="T" /> is not an enumeration type.
    /// </exception>
    public static T Next<T>(this T src) where T : struct, Enum
    {
        T[] values = EnumCache.GetValues<T>();
        int index = Array.IndexOf(values, src);
        return values[(index + 1) % values.Length];
    }

    /// <summary>
    /// Retrieves the previous value in the enumeration, wrapping around to the last value if the
    /// current value is the first.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the enumeration. Must be a struct and an <see cref="Enum" />.
    /// </typeparam>
    /// <param name="src">
    /// The current enumeration value.
    /// </param>
    /// <returns>
    /// The previous enumeration value. If the current value is the first in the enumeration, the
    /// method returns the last value.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <typeparamref name="T" /> is not an enumeration type.
    /// </exception>
    public static T Prev<T>(this T src) where T : struct, Enum
    {
        T[] values = EnumCache.GetValues<T>();
        int index = Array.IndexOf(values, src);
        return values[(index - 1 + values.Length) % values.Length];
    }

    private static class EnumCache
    {
        private static readonly ConcurrentDictionary<Type, Array> _cache = new();

        public static T[] GetValues<T>() where T : struct, Enum => (T[])_cache.GetOrAdd(typeof(T), Enum.GetValues);
    }
}