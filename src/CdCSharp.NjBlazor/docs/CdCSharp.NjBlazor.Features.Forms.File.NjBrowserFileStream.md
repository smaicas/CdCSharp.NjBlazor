# NjBrowserFileStream

*Namespace:* CdCSharp.NjBlazor.Features.Forms.File
*Assembly:* CdCSharp.NjBlazor
*Source:* NjBrowserFileStream.cs



    Represents a stream for handling browser file operations.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IDOMJsInterop domJs, ElementReference inputFileElement, NjBrowserFile file, Int64 maxAllowedSize, CancellationToken cancellationToken)`


    Initializes a new instance of the NjBrowserFileStream class.
    



**Property:** `CanRead` (Public)

Determines if the object can be read.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* 


**Property:** `CanSeek` (Public)

Determines if seeking is supported.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* 


**Property:** `CanWrite` (Public)

Determines if writing is allowed.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* 


**Property:** `Length` (Public)

Gets the length of the file.

*Property Type:* `Int64`
*Nullable:* False
*Attributes:* 


**Property:** `Position` (Public)

Gets or sets the position within the stream.

*Property Type:* `Int64`
*Nullable:* False
*Attributes:* 


**Method:** `Flush`
*Method Signature:* `Void Flush()`

Flushes the stream, but this operation is not supported.



**Method:** `Read`
*Method Signature:* `Int32 Read( buffer, Int32 offset, Int32 count)`



**Method:** `ReadAsync`
*Method Signature:* `Task ReadAsync( buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)`



**Method:** `ReadAsync`
*Method Signature:* `ValueTask ReadAsync(Memory buffer, CancellationToken cancellationToken)`


    Asynchronously reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    



**Method:** `Seek`
*Method Signature:* `Int64 Seek(Int64 offset, SeekOrigin origin)`

Throws a NotSupportedException when attempting to seek.



**Method:** `SetLength`
*Method Signature:* `Void SetLength(Int64 value)`

Sets the length of the current stream.



**Method:** `Write`
*Method Signature:* `Void Write( buffer, Int32 offset, Int32 count)`


    Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
    This operation is not supported and will throw a NotSupportedException.
    



**Method:** `Dispose`
*Method Signature:* `Void Dispose(Boolean disposing)`


    Disposes of the resources used by the object.
    


---
## Inherited from Stream

**Summary:**
Provides a generic view of a sequence of bytes. This is an abstract class.
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor()`

Initializes a new instance of the [T:System.IO.Stream] class.



**Method:** `BeginRead`
*Method Signature:* `IAsyncResult BeginRead( buffer, Int32 offset, Int32 count, AsyncCallback callback, Object state)`

Begins an asynchronous read operation. (Consider using [M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)] instead.)



**Method:** `BeginWrite`
*Method Signature:* `IAsyncResult BeginWrite( buffer, Int32 offset, Int32 count, AsyncCallback callback, Object state)`

Begins an asynchronous write operation. (Consider using [M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)] instead.)



**Method:** `Close`
*Method Signature:* `Void Close()`

Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream. Instead of calling this method, ensure that the stream is properly disposed.



**Method:** `CopyTo`
*Method Signature:* `Void CopyTo(Stream destination)`

Reads the bytes from the current stream and writes them to another stream. Both streams positions are advanced by the number of bytes copied.



**Method:** `CopyTo`
*Method Signature:* `Void CopyTo(Stream destination, Int32 bufferSize)`

Reads the bytes from the current stream and writes them to another stream, using a specified buffer size. Both streams positions are advanced by the number of bytes copied.



**Method:** `CopyToAsync`
*Method Signature:* `Task CopyToAsync(Stream destination)`

Asynchronously reads the bytes from the current stream and writes them to another stream. Both streams positions are advanced by the number of bytes copied.



**Method:** `CopyToAsync`
*Method Signature:* `Task CopyToAsync(Stream destination, Int32 bufferSize)`

Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified buffer size. Both streams positions are advanced by the number of bytes copied.



**Method:** `CopyToAsync`
*Method Signature:* `Task CopyToAsync(Stream destination, Int32 bufferSize, CancellationToken cancellationToken)`

Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified buffer size and cancellation token. Both streams positions are advanced by the number of bytes copied.



**Method:** `CopyToAsync`
*Method Signature:* `Task CopyToAsync(Stream destination, CancellationToken cancellationToken)`

Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified cancellation token. Both streams positions are advanced by the number of bytes copied.



**Method:** `CreateWaitHandle`
*Method Signature:* `WaitHandle CreateWaitHandle()`

Allocates a [T:System.Threading.WaitHandle] object.



**Method:** `Dispose`
*Method Signature:* `Void Dispose()`

Releases all resources used by the [T:System.IO.Stream].



**Method:** `Dispose`
*Method Signature:* `Void Dispose(Boolean disposing)`

Releases the unmanaged resources used by the [T:System.IO.Stream] and optionally releases the managed resources.



**Method:** `DisposeAsync`
*Method Signature:* `ValueTask DisposeAsync()`

Asynchronously releases the unmanaged resources used by the [T:System.IO.Stream].



**Method:** `EndRead`
*Method Signature:* `Int32 EndRead(IAsyncResult asyncResult)`

Waits for the pending asynchronous read to complete. (Consider using [M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)] instead.)



**Method:** `EndWrite`
*Method Signature:* `Void EndWrite(IAsyncResult asyncResult)`

Ends an asynchronous write operation. (Consider using [M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)] instead.)



**Method:** `Flush`
*Method Signature:* `Void Flush()`

When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.



**Method:** `FlushAsync`
*Method Signature:* `Task FlushAsync()`

Asynchronously clears all buffers for this stream and causes any buffered data to be written to the underlying device.



**Method:** `FlushAsync`
*Method Signature:* `Task FlushAsync(CancellationToken cancellationToken)`

Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and monitors cancellation requests.



**Method:** `ObjectInvariant`
*Method Signature:* `Void ObjectInvariant()`

Provides support for a [T:System.Diagnostics.Contracts.Contract].



**Method:** `Read`
*Method Signature:* `Int32 Read( buffer, Int32 offset, Int32 count)`

When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.



**Method:** `Read`
*Method Signature:* `Int32 Read(Span buffer)`

When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.



**Method:** `ReadAsync`
*Method Signature:* `Task ReadAsync( buffer, Int32 offset, Int32 count)`

Asynchronously reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.



**Method:** `ReadAsync`
*Method Signature:* `Task ReadAsync( buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)`

Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.



**Method:** `ReadAsync`
*Method Signature:* `ValueTask ReadAsync(Memory buffer, CancellationToken cancellationToken)`

Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.



**Method:** `ReadAtLeast`
*Method Signature:* `Int32 ReadAtLeast(Span buffer, Int32 minimumBytes, Boolean throwOnEndOfStream)`

Reads at least a minimum number of bytes from the current stream and advances the position within the stream by the number of bytes read.



**Method:** `ReadAtLeastAsync`
*Method Signature:* `ValueTask ReadAtLeastAsync(Memory buffer, Int32 minimumBytes, Boolean throwOnEndOfStream, CancellationToken cancellationToken)`

Asynchronously reads at least a minimum number of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.



**Method:** `ReadByte`
*Method Signature:* `Int32 ReadByte()`

Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.



**Method:** `ReadExactly`
*Method Signature:* `Void ReadExactly( buffer, Int32 offset, Int32 count)`

Reads  number of bytes from the current stream and advances the position within the stream.



**Method:** `ReadExactly`
*Method Signature:* `Void ReadExactly(Span buffer)`

Reads bytes from the current stream and advances the position within the stream until the  is filled.



**Method:** `ReadExactlyAsync`
*Method Signature:* `ValueTask ReadExactlyAsync( buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)`

Asynchronously reads  number of bytes from the current stream, advances the position within the stream, and monitors cancellation requests.



**Method:** `ReadExactlyAsync`
*Method Signature:* `ValueTask ReadExactlyAsync(Memory buffer, CancellationToken cancellationToken)`

Asynchronously reads bytes from the current stream, advances the position within the stream until the  is filled, and monitors cancellation requests.



**Method:** `Seek`
*Method Signature:* `Int64 Seek(Int64 offset, SeekOrigin origin)`

When overridden in a derived class, sets the position within the current stream.



**Method:** `SetLength`
*Method Signature:* `Void SetLength(Int64 value)`

When overridden in a derived class, sets the length of the current stream.



**Method:** `Synchronized`
*Method Signature:* `Stream Synchronized(Stream stream)`

Creates a thread-safe (synchronized) wrapper around the specified [T:System.IO.Stream] object.



**Method:** `ValidateBufferArguments`
*Method Signature:* `Void ValidateBufferArguments( buffer, Int32 offset, Int32 count)`

Validates arguments provided to reading and writing methods on [T:System.IO.Stream].



**Method:** `ValidateCopyToArguments`
*Method Signature:* `Void ValidateCopyToArguments(Stream destination, Int32 bufferSize)`

Validates arguments provided to the [M:System.IO.Stream.CopyTo(System.IO.Stream,System.Int32)] or [M:System.IO.Stream.CopyToAsync(System.IO.Stream,System.Int32,System.Threading.CancellationToken)] methods.



**Method:** `Write`
*Method Signature:* `Void Write( buffer, Int32 offset, Int32 count)`

When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.



**Method:** `Write`
*Method Signature:* `Void Write(ReadOnlySpan buffer)`

When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.



**Method:** `WriteAsync`
*Method Signature:* `Task WriteAsync( buffer, Int32 offset, Int32 count)`

Asynchronously writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.



**Method:** `WriteAsync`
*Method Signature:* `Task WriteAsync( buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)`

Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.



**Method:** `WriteAsync`
*Method Signature:* `ValueTask WriteAsync(ReadOnlyMemory buffer, CancellationToken cancellationToken)`

Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.



**Method:** `WriteByte`
*Method Signature:* `Void WriteByte(Byte value)`

Writes a byte to the current position in the stream and advances the position within the stream by one byte.



**Property:** `CanRead` (Public)

When overridden in a derived class, gets a value indicating whether the current stream supports reading.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* 


**Property:** `CanSeek` (Public)

When overridden in a derived class, gets a value indicating whether the current stream supports seeking.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* 


**Property:** `CanTimeout` (Public)

Gets a value that determines whether the current stream can time out.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* 


**Property:** `CanWrite` (Public)

When overridden in a derived class, gets a value indicating whether the current stream supports writing.

*Property Type:* `Boolean`
*Nullable:* False
*Attributes:* 


**Property:** `Length` (Public)

When overridden in a derived class, gets the length in bytes of the stream.

*Property Type:* `Int64`
*Nullable:* False
*Attributes:* 


**Property:** `Position` (Public)

When overridden in a derived class, gets or sets the position within the current stream.

*Property Type:* `Int64`
*Nullable:* False
*Attributes:* 


**Property:** `ReadTimeout` (Public)

Gets or sets a value, in milliseconds, that determines how long the stream will attempt to read before timing out.

*Property Type:* `Int32`
*Nullable:* False
*Attributes:* 


**Property:** `WriteTimeout` (Public)

Gets or sets a value, in milliseconds, that determines how long the stream will attempt to write before timing out.

*Property Type:* `Int32`
*Nullable:* False
*Attributes:* 

---
## Inherited from MarshalByRefObject

**Summary:**
Enables access to objects across application domain boundaries in applications that support remoting.
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor()`

Initializes a new instance of the [T:System.MarshalByRefObject] class.



**Method:** `GetLifetimeService`
*Method Signature:* `Object GetLifetimeService()`

Retrieves the current lifetime service object that controls the lifetime policy for this instance.



**Method:** `InitializeLifetimeService`
*Method Signature:* `Object InitializeLifetimeService()`

Obtains a lifetime service object to control the lifetime policy for this instance.



**Method:** `MemberwiseClone`
*Method Signature:* `MarshalByRefObject MemberwiseClone(Boolean cloneIdentity)`

Creates a shallow copy of the current [T:System.MarshalByRefObject] object.


