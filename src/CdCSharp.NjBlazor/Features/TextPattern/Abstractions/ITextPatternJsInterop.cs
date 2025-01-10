using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.TextPattern.Abstractions;

/// <summary>Represents an interface for interacting with text patterns in JavaScript.</summary>
public interface ITextPatternJsInterop
{
    /// <summary>
    /// Adds dynamic text patterns to the specified content box element.
    /// </summary>
    /// <param name="contentBox">The reference to the content box element.</param>
    /// <param name="elements">The collection of dynamic text patterns to add.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask TextPatternAddDynamic(
        ElementReference contentBox,
        IEnumerable<ElementPattern> elements,
        object? dotnetReference,
        string notifyChangedTextCallback,
        string validatePartialCallback);
}
