using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Features.Forms.File;

/// <summary>
/// Represents a stream for handling browser file operations.
/// </summary>
internal sealed class NjBrowserFileStream : Stream
{
    private readonly IDOMJsInterop _domJs;
    private readonly NjBrowserFile _file;
    private readonly ElementReference _inputFileElement;
    private readonly long _maxAllowedSize;
    private readonly CancellationTokenSource _openReadStreamCts;
    private readonly Task<Stream> OpenReadStreamTask;
    private CancellationTokenSource _copyFileDataCts;
    private bool _isDisposed;
    private IJSStreamReference _jsStreamReference;
    private long _position;

    /// <summary>
    /// Initializes a new instance of the NjBrowserFileStream class.
    /// </summary>
    /// <param name="domJs">The DOM JavaScript interop service.</param>
    /// <param name="inputFileElement">The reference to the input file element.</param>
    /// <param name="file">The browser file to read from.</param>
    /// <param name="maxAllowedSize">The maximum allowed size for the file.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operations.</param>
    public NjBrowserFileStream(
        IDOMJsInterop domJs,
        ElementReference inputFileElement,
        NjBrowserFile file,
        long maxAllowedSize,
        CancellationToken cancellationToken
    )
    {
        _domJs = domJs;
        _inputFileElement = inputFileElement;
        _file = file;
        _maxAllowedSize = maxAllowedSize;
        _openReadStreamCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        OpenReadStreamTask = OpenReadStreamAsync(_openReadStreamCts.Token);
    }

    /// <summary>Determines if the object can be read.</summary>
    /// <value>Always returns true.</value>
    public override bool CanRead => true;

    /// <summary>Determines if seeking is supported.</summary>
    /// <value>Always returns false, indicating that seeking is not supported.</value>
    public override bool CanSeek => false;

    /// <summary>Determines if writing is allowed.</summary>
    /// <value>Always returns false, indicating that writing is not allowed.</value>
    public override bool CanWrite => false;

    /// <summary>Gets the length of the file.</summary>
    /// <value>The size of the file in bytes.</value>
    public override long Length => _file.Size;

    /// <summary>Gets or sets the position within the stream.</summary>
    /// <exception cref="NotSupportedException">Thrown when setting the position is not supported.</exception>
    public override long Position
    {
        get => _position;
        set => throw new NotSupportedException();
    }

    /// <summary>Flushes the stream, but this operation is not supported.</summary>
    /// <exception cref="NotSupportedException">Thrown when the flush operation is not supported.</exception>
    public override void Flush() => throw new NotSupportedException();

    /// <summary>
    /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
    /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
    /// <returns>This method does not return a value. It throws a NotSupportedException indicating that
    public override int Read(byte[] buffer, int offset, int count) =>
        throw new NotSupportedException("Synchronous reads are not supported.");

    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the bytes read from the current source.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous read operation. The value of the task object contains the total number of
    public override Task<int> ReadAsync(
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken
    ) => ReadAsync(new Memory<byte>(buffer, offset, count), cancellationToken).AsTask();

    /// <summary>
    /// Asynchronously reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    /// <param name="buffer">The buffer to write the data into.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation. The value task containing the number of bytes read into the buffer.</returns>
    public override async ValueTask<int> ReadAsync(
        Memory<byte> buffer,
        CancellationToken cancellationToken = default
    )
    {
        int num = (int)Math.Min(Length - Position, buffer.Length);
        if (num <= 0)
            return 0;

        int num2 = await CopyFileDataIntoBuffer(buffer.Slice(0, num), cancellationToken);
        _position += num2;
        return num2;
    }

    /// <summary>Throws a NotSupportedException when attempting to seek.</summary>
    /// <param name="offset">The offset in bytes to seek.</param>
    /// <param name="origin">The reference point used to obtain the new position.</param>
    /// <returns>This method always throws a NotSupportedException.</returns>
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    /// <summary>Sets the length of the current stream.</summary>
    /// <param name="value">The desired length of the stream.</param>
    /// <exception cref="NotSupportedException">Thrown when setting the length is not supported.</exception>
    public override void SetLength(long value) => throw new NotSupportedException();

    /// <summary>
    /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
    /// This operation is not supported and will throw a NotSupportedException.
    /// </summary>
    /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
    /// <param name="count">The number of bytes to be written to the current stream.</param>
    /// <exception cref="NotSupportedException">Thrown when the write operation is not supported.</exception>
    public override void Write(byte[] buffer, int offset, int count) =>
        throw new NotSupportedException();

    /// <summary>
    /// Disposes of the resources used by the object.
    /// </summary>
    /// <param name="disposing">A boolean value indicating whether the method is being called from user code (true) or from a finalizer (false).</param>
    /// <remarks>
    /// This method cancels any ongoing read stream operations and copy file data operations.
    /// It disposes of the JavaScript stream reference asynchronously.
    /// </remarks>
    protected override void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            _openReadStreamCts.Cancel();
            _copyFileDataCts?.Cancel();
            try
            {
                _jsStreamReference?.DisposeAsync().Preserve();
            }
            catch { }

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }

    private async ValueTask<int> CopyFileDataIntoBuffer(
        Memory<byte> destination,
        CancellationToken cancellationToken
    )
    {
        Stream obj = await OpenReadStreamTask;
        _copyFileDataCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        return await obj.ReadAsync(destination, _copyFileDataCts.Token);
    }

    private async Task<Stream> OpenReadStreamAsync(CancellationToken cancellationToken)
    {
        _jsStreamReference = await _domJs.ReadFileDataAsync(
            cancellationToken,
            _inputFileElement,
            _file.Id
        );
        return await _jsStreamReference.OpenReadStreamAsync(_maxAllowedSize, cancellationToken);
    }
}
