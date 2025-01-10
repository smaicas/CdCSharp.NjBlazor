namespace CdCSharp.NjBlazor.Features.DeviceManager.Components;

/// <summary>
/// Represents a callback interface for notifying a resize event.
/// </summary>
public interface IResizeJsCallback
{
    /// <summary>
    /// Notifies the callback about a window resize event.
    /// </summary>
    /// <param name="windowWidth">The new width of the window.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task NotifyResize(int windowWidth);
}