using CdCSharp.NjBlazor.Core.Strings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;

namespace CdCSharp.NjBlazor.Core.Abstractions.Components;

/// <summary>
/// Base class for input components that handle user input of type TValue.
/// </summary>
/// <typeparam name="TValue">The type of the input value.</typeparam>
public abstract class NjInputBase<TValue> : InputBase<TValue>
{
    /// <summary>
    /// Gets or sets the associated <see cref="ElementReference"/>.
    /// <para>May be <see langword="null"/> if accessed before the component is rendered.</para>
    /// </summary>
    [DisallowNull]
    protected ElementReference? InputReference { get; set; }

    /// <summary>Gets or sets the input reference ID.</summary>
    /// <value>The input reference ID.</value>
    protected string? InputReferenceId { get; set; }

    private bool _readOnly;

    /// <summary>Gets or sets the CSS class for the component.</summary>
    /// <value>The CSS class for the component.</value>
    [Parameter]
    public string Class { get; set; } = string.Empty;

    /// <summary>Gets or sets a value indicating whether the component is disabled.</summary>
    /// <value>True if the component is disabled; otherwise, false.</value>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>Gets the field identifier.</summary>
    /// <value>The field identifier.</value>
    public new FieldIdentifier FieldIdentifier => base.FieldIdentifier;

    /// <summary>
    /// Concatenates non-empty strings in the 'classes' array to form a single string.
    /// </summary>
    /// <param name="classes">An array of strings representing classes.</param>
    /// <returns>A single string containing non-empty classes separated by a specified delimiter.</returns>
    protected string AsClass(params string[] classes) => classes.NotEmptyJoin();

    /// <summary>Gets or sets a value indicating whether the control is a form control.</summary>
    /// <value>True if the control is a form control; otherwise, false.</value>
    [Parameter]
    public bool FormControl { get; set; }

    /// <summary>Gets or sets a value indicating whether the control is in read-only mode.</summary>
    /// <value>True if the control is read-only; otherwise, false.</value>
    [Parameter]
    public bool ReadOnly { get => Disabled || _readOnly; set => _readOnly = value; }

    /// <summary>
    /// Gets the CSS class for an empty class based on the IsEmpty property.
    /// </summary>
    /// <remarks>
    /// If the IsEmpty property is true, returns the CSS class reference for an empty class;
    /// otherwise, returns an empty string.
    /// </remarks>
    protected string EmptyClass => IsEmpty ? CssClassReferences.Empty : string.Empty;

    /// <summary>
    /// Gets the CSS class for focusing based on the focus state.
    /// </summary>
    /// <value>
    /// The CSS class for focusing if the element is focused; otherwise, an empty string.
    /// </value>
    protected string FocusClass => IsFocused ? CssClassReferences.Focus : string.Empty;

    /// <summary>Determines if the current value is empty.</summary>
    /// <value>True if the current value is null, otherwise false.</value>
    protected virtual bool IsEmpty => CurrentValue == null;

    /// <summary>Gets or sets a value indicating whether the element is focused.</summary>
    /// <value>True if the element is focused; otherwise, false.</value>
    private bool IsFocused { get; set; }

    /// <summary>
    /// Method called after the component has been rendered.
    /// </summary>
    /// <param name="firstRender">A boolean value indicating if this is the first render of the component.</param>
    /// <remarks>
    /// This method sets the InputReferenceId to the Id of the InputReference if it is not null and triggers a re-render of the component.
    /// </remarks>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender && InputReference != null)
        {
            InputReferenceId = InputReference.Value.Id;
            StateHasChanged();
        }
    }

    /// <summary>
    /// Handles the focus event asynchronously.
    /// </summary>
    /// <param name="focusEventArgs">The focus event arguments.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected virtual Task OnFocusAsync(FocusEventArgs? focusEventArgs)
    {
        IsFocused = true;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles the asynchronous event when focus moves out from the element.
    /// </summary>
    /// <param name="focusEventArgs">The event arguments related to the focus change.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected virtual Task OnFocusOutAsync(FocusEventArgs? focusEventArgs)
    {
        IsFocused = false;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles input asynchronously.
    /// </summary>
    /// <param name="changeEventArgs">The event arguments for the input change.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected virtual Task OnInputAsync(ChangeEventArgs changeEventArgs) => Task.CompletedTask;

    /// <summary>
    /// Tries to parse a value from a string representation.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed value if the parsing succeeded, otherwise the default value of the type.</param>
    /// <param name="validationErrorMessage">When this method returns, contains an error message if the parsing failed, otherwise null.</param>
    /// <returns>True if the parsing was successful; otherwise, false.</returns>
    /// <exception cref="NotImplementedException">Thrown to indicate that the method implementation is not provided.</exception>
    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result,
        [NotNullWhen(false)] out string? validationErrorMessage) => throw new NotImplementedException();
}