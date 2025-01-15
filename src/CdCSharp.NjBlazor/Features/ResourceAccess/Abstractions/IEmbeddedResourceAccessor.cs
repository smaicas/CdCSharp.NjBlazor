using System.Text;

namespace CdCSharp.NjBlazor.Features.ResourceAccess.Abstractions;

/// <summary>
/// Interface for accessing embedded resources asynchronously and supporting disposal.
/// </summary>
/// <seealso cref="IAsyncDisposable" />
public interface IEmbeddedResourceAccessor : IAsyncDisposable
{
    /// <summary>
    /// Asynchronously retrieves the content of a resource file specified by the file path.
    /// </summary>
    /// <param name="filePath">
    /// The path to the resource file.
    /// </param>
    /// <param name="encoding">
    /// The encoding used to read the file (default is null for auto-detect).
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the content of
    /// the resource file as a string.
    /// </returns>
    Task<string> GetResourceContentAsync(string filePath, Encoding? encoding = null);
}