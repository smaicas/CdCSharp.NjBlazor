using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Forms.Radio;

/// <summary>
/// Base class for radio input components that handle string values.
/// </summary>
public abstract class NjInputRadioBase : NjInputBase<string>
{
    /// <summary>
    /// Gets or sets the content to be rendered as a child component.
    /// </summary>
    /// <value>
    /// The content to be rendered as a child component.
    /// </value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the name property.
    /// </summary>
    /// <value>
    /// The name property.
    /// </value>
    [Parameter]
    public string Name { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets the list of radio options for the input.
    /// </summary>
    /// <value>
    /// The list of radio options.
    /// </value>
    protected List<NjInputRadioOption> Options { get; } = [];

    /// <summary>
    /// Gets or sets the selected radio option.
    /// </summary>
    /// <value>
    /// The selected radio option, or null if no option is selected.
    /// </value>
    protected NjInputRadioOption? SelectedOption { get; private set; }

    /// <summary>
    /// Asynchronously sets the parameters for the component.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (!string.IsNullOrEmpty(CurrentValue))
        {
            NjInputRadioOption? option = Options.FirstOrDefault(o => o.Value == CurrentValue);
            if (option != null && SelectedOption != option)
            {
                SelectedOption = option;
            }
        }
    }

    /// <summary>
    /// Selects an option asynchronously.
    /// </summary>
    /// <param name="valueOption">
    /// The option to select.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected Task SelectOptionAsync(NjInputRadioOption valueOption)
    {
        if (ReadOnly || SelectedOption == valueOption)
            return Task.CompletedTask;

        SelectedOption = valueOption;
        CurrentValue = valueOption.Value;
        return Task.CompletedTask;
    }
}