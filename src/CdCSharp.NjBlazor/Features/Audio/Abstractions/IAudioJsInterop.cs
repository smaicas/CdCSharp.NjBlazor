using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Audio.Abstractions;

/// <summary>
/// Interface for JavaScript interop related to audio functionality.
/// </summary>
public interface IAudioJsInterop
{
    /// <summary>
    /// Sets the audio source asynchronously for the specified element.
    /// </summary>
    /// <param name="elRef">The reference to the element for which the audio source needs to be set.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, with the result being the audio source as a string.</returns>
    ValueTask<string> SetAudioSourceAsync(ElementReference elRef);

    /// <summary>Starts an asynchronous operation to begin recording.</summary>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask StartRecordingAsync();

    /// <summary>
    /// Asynchronously stops the recording process.
    /// </summary>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask<object> StopRecordingAsync();

    /// <summary>
    /// Asynchronously generates a visualization of a canvas element.
    /// </summary>
    /// <param name="canvasElementReference">A reference to the canvas element to visualize.</param>
    /// <returns>A ValueTask representing the asynchronous operation that yields the visualization as a string.</returns>
    ValueTask<string> VisualizeCanvasAsync(ElementReference canvasElementReference);
}