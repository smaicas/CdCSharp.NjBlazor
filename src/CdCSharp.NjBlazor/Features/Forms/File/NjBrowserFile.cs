using Microsoft.AspNetCore.Components.Forms;

namespace CdCSharp.NjBlazor.Features.Forms.File;

/// <summary>
/// Represents a file uploaded via a browser.
/// </summary>
internal sealed class NjBrowserFile : IBrowserFile
{
    private long _size;

    /// <summary>
    /// Gets or sets the content type.
    /// </summary>
    /// <value>
    /// The content type.
    /// </value>
    public string ContentType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the object was last modified.
    /// </summary>
    public DateTimeOffset LastModified { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the relative path.
    /// </summary>
    /// <value>
    /// The relative path.
    /// </value>
    public string? RelativePath { get; set; }

    /// <summary>
    /// Gets or sets the size of an object.
    /// </summary>
    /// <value>
    /// The size of the object.
    /// </value>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the provided size is negative.
    /// </exception>
    public long Size
    {
        get => _size;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(
                    "Size",
                    $"Size must be a non-negative value. Value provided: {value}."
                );

            _size = value;
        }
    }

    /// <summary>
    /// Gets or sets the owner of the input file.
    /// </summary>
    internal NjInputFileBase Owner { get; set; }

    /// <summary>
    /// Opens a read stream for the file.
    /// </summary>
    /// <param name="maxAllowedSize">
    /// The maximum allowed size for the file (default is 512000 bytes).
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel the operation (default is none).
    /// </param>
    /// <returns>
    /// A stream for reading the file.
    /// </returns>
    /// <exception cref="IOException">
    /// Thrown when the file size exceeds the maximum allowed size.
    /// </exception>
    public Stream OpenReadStream(
        long maxAllowedSize = 512000,
        CancellationToken cancellationToken = default
    )
    {
        if (Size > maxAllowedSize)
            throw new IOException(
                $"Supplied file with size {Size} bytes exceeds the maximum of {maxAllowedSize} bytes."
            );

        return Owner.OpenReadStream(this, maxAllowedSize, cancellationToken);
    }
}