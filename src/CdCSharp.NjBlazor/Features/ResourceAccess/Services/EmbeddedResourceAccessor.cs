using CdCSharp.NjBlazor.Core.Cache;
using CdCSharp.NjBlazor.Features.ResourceAccess.Abstractions;
using System.Reflection;
using System.Text;

namespace CdCSharp.NjBlazor.Features.ResourceAccess.Services;

/// <summary>
/// Provides access to embedded resources.
/// </summary>
public class EmbeddedResourceAccessor : IEmbeddedResourceAccessor
{
    private readonly ICacheService<EmbeddedResourceAccessor, string> _cacheService;

    /// <summary>
    /// Initializes a new instance of the EmbeddedResourceAccessor class.
    /// </summary>
    /// <param name="cacheService">
    /// The cache service to use for accessing embedded resources.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the cache service is null.
    /// </exception>
    public EmbeddedResourceAccessor(ICacheService<EmbeddedResourceAccessor, string> cacheService) => _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting
    /// resources asynchronously.
    /// </summary>
    /// <returns>
    /// A <see cref="ValueTask" /> that represents the asynchronous dispose operation.
    /// </returns>
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    /// <summary>
    /// Retrieves the content of a resource file asynchronously.
    /// </summary>
    /// <param name="filePath">
    /// The path to the resource file.
    /// </param>
    /// <param name="encoding">
    /// The encoding to be used for reading the file (default is UTF-8).
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the content of
    /// the resource file as a string.
    /// </returns>
    /// <exception cref="ApplicationException">
    /// Thrown when the entry assembly is not found.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the specified resource file is not found.
    /// </exception>
    public Task<string> GetResourceContentAsync(string filePath, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        if (_cacheService.TryGet(filePath, out string? cachedContent) && cachedContent != null)
            return Task.FromResult(cachedContent);

        string resourcePart = string.Join(".", filePath.Split("/").Where(f => !string.IsNullOrWhiteSpace(f)));
        Assembly assembly = Assembly.GetEntryAssembly() ?? throw new ApplicationException("ReadFileStreamAsync requires an entry assembly");
        string resourceName = $"{assembly.GetName().Name}.{resourcePart}";
        Stream resourceStream = assembly.GetManifestResourceStream(resourceName) ?? throw new ArgumentException($"Resource '{resourceName}' not found.");

        using StreamReader reader = new(resourceStream);
        string content = reader.ReadToEnd();
        resourceStream.Dispose();
        _cacheService.Set(filePath, content);
        return Task.FromResult(content);
    }
}