using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Media.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Forms.Checkbox;

/// <summary>
/// Base class for a checkbox input component that binds to a nullable boolean value.
/// </summary>
public abstract class NjInputCheckboxBase : NjInputBase<bool?>
{
    /// <summary>
    /// The reference ID for the input.
    /// </summary>
    protected string? _inputReferenceId;

    /// <summary>
    /// Gets or sets the content of the component.
    /// </summary>
    /// <remarks>
    /// This property allows rendering child content within the component.
    /// </remarks>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the size of the input checkbox.
    /// </summary>
    /// <value>
    /// The size of the input checkbox.
    /// </value>
    [Parameter]
    public NjInputCheckboxSize Size { get; set; } = NjInputCheckboxSize.Medium;

    /// <summary>
    /// Gets or sets a value indicating whether the control supports three states.
    /// </summary>
    /// <value>
    /// True if the control supports three states; otherwise, false.
    /// </value>
    [Parameter]
    public bool TriState { get; set; }

    /// <summary>
    /// Calculates the icon based on the current value.
    /// </summary>
    /// <returns>
    /// The icon corresponding to the current value:
    /// - If the current value is true, returns the checked box icon.
    /// - If the current value is false, returns the unchecked box icon.
    /// - If the current value is neither true nor false, returns the indeterminate box icon.
    /// </returns>
    protected string CalculateIcon()
    {
        if (CurrentValue == true)
            return Media.Icons.NjIcons.Materials.MaterialIcons.i_check_box;
        if (CurrentValue == false)
            return Media.Icons.NjIcons.Materials.MaterialIcons.i_check_box_outline_blank;
        return Media.Icons.NjIcons.Materials.MaterialIcons.i_indeterminate_check_box;
    }

    /// <summary>
    /// Calculates the corresponding icon size based on the input checkbox size.
    /// </summary>
    /// <returns>
    /// The calculated icon size based on the input checkbox size.
    /// </returns>
    protected NjSvgIconSize CalculateIconSize()
    {
        return Size switch
        {
            NjInputCheckboxSize.Small => NjSvgIconSize.Small,
            NjInputCheckboxSize.Medium => NjSvgIconSize.Medium,
            _ => NjSvgIconSize.Large,
        };
    }

    /// <summary>
    /// Method called after the component has been rendered.
    /// </summary>
    /// <param name="firstRender">
    /// A boolean value indicating if this is the first render of the component.
    /// </param>
    /// <remarks>
    /// If it is the first render, the method checks if the InputReference is not null and assigns
    /// the Id to _inputReferenceId. If TriState is false and CurrentValue is null, CurrentValue is
    /// set to false. Finally, the method calls StateHasChanged to notify the component that its
    /// state has changed.
    /// </remarks>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender)
        {
            if (InputReference != null)
            {
                _inputReferenceId = InputReference.Value.Id;
            }

            if (!TriState && CurrentValue == null)
            {
                CurrentValue = false;
            }
            StateHasChanged();
        }
    }

    /// <summary>
    /// Handles the asynchronous event when a checkbox is changed.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected Task OnCheckboxChangedAsync()
    {
        if (TriState && CurrentValue == false)
        {
            CurrentValue = null;
            return Task.CompletedTask;
        }
        if (CurrentValue == null)
        {
            CurrentValue = true;
            return Task.CompletedTask;
        }

        CurrentValue = !CurrentValue;
        return Task.CompletedTask;
    }
}