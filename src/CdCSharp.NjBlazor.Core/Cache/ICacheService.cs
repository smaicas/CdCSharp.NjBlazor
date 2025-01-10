namespace CdCSharp.NjBlazor.Core.Cache;

/// <summary>
/// Interface for a cache service that stores values of type TValue with a root key of type TRoot.
/// </summary>
/// <typeparam name="TRoot">
/// The type of the root key.
/// </typeparam>
/// <typeparam name="TValue">
/// The type of the stored values.
/// </typeparam>
public interface ICacheService<TRoot, TValue>
{
    /// <summary>
    /// Sets a value in the dictionary with the specified key.
    /// </summary>
    /// <param name="key">
    /// The key of the value to set.
    /// </param>
    /// <param name="value">
    /// The value to set.
    /// </param>
    void Set(string key, TValue value);

    /// <summary>
    /// Tries to retrieve a value associated with the specified key.
    /// </summary>
    /// <param name="key">
    /// The key to look up.
    /// </param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key, if the key
    /// is found; otherwise, the default value for the type of the value parameter. This parameter
    /// is passed uninitialized.
    /// </param>
    /// <returns>
    /// True if the key was found and the associated value was successfully retrieved; otherwise, false.
    /// </returns>
    bool TryGet(string key, out TValue? value);
}

/// <summary>
/// Represents a cache service interface for a specific type of pointer.
/// </summary>
/// <typeparam name="TPointer">
/// The type of pointer.
/// </typeparam>
public interface ICacheService<TPointer> : ICacheService<TPointer, object>
{
}

/// <summary>
/// Interface for a cache service with a default cache root and object type.
/// </summary>
public interface ICacheService : ICacheService<DefaultCacheRoot, object>
{
}