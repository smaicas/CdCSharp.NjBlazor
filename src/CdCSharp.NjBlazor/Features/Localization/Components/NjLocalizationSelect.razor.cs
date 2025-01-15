using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Forms.Dropdown;
using CdCSharp.NjBlazor.Features.Localization.Abstractions;
using CdCSharp.NjBlazor.Features.Media.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace CdCSharp.NjBlazor.Features.Localization.Components;

/// <summary>
/// Represents a partial class for selecting localization in the NjComponentBase.
/// </summary>
public partial class NjLocalizationSelect : NjComponentBase
{
    /// <summary>
    /// Gets or sets the current culture used for localization.
    /// </summary>
    /// <value>
    /// The current culture.
    /// </value>
    /// <exception cref="ArgumentNullException">
    /// Thrown when setting a null culture.
    /// </exception>
    public CultureInfo? CurrentCulture
    {
        get => CultureInfo.CurrentCulture;
        set
        {
            if (value == null) return;
            if (CultureInfo.CurrentCulture != value)
            {
                LocalizationJs.SetCultureAsync(value);
                UpdateCulture(value);
                Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the path to the icon file.
    /// </summary>
    /// <value>
    /// The path to the icon file.
    /// </value>
    [Parameter]
    public string? OnlyIcon { get; set; }

    /// <summary>
    /// Gets or sets the size of the icon.
    /// </summary>
    /// <value>
    /// The size of the icon.
    /// </value>
    [Parameter]
    public NjSvgIconSize OnlyIconSize { get; set; }

    /// <summary>
    /// Gets or sets the position of the dropdown options.
    /// </summary>
    /// <value>
    /// The position of the dropdown options.
    /// </value>
    [Parameter]
    public NjDropdownOptionsPosition OptionsPosition { get; set; } = NjDropdownOptionsPosition.TopRight;

    /// <summary>
    /// Gets or sets the ILocalizationJsInterop for handling JavaScript interop related to localization.
    /// </summary>
    [Inject] private ILocalizationJsInterop LocalizationJs { get; set; } = default!;

    /// <summary>
    /// Injects the LocalizationSettings options.
    /// </summary>
    /// <remarks>
    /// This attribute is used to inject an instance of the LocalizationSettings options into the
    /// property named LocalizationSettings.
    /// </remarks>
    [Inject] private IOptions<LocalizationSettings> LocalizationSettings { get; set; }

    /// <summary>
    /// Gets or sets the NavigationManager for navigating between pages.
    /// </summary>
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    /// <summary>
    /// Called when the element is initialized.
    /// </summary>
    /// <remarks>
    /// This method is called after the element is initialized, prior to rendering.
    /// </remarks>
    protected override void OnInitialized() => base.OnInitialized();

    private void UpdateCulture(CultureInfo value)
    {
        if (Equals(CultureInfo.CurrentCulture, value)) return;

        CultureInfo.CurrentCulture = value;
        CultureInfo.CurrentUICulture = value;
        CultureInfo.DefaultThreadCurrentCulture = value;
        CultureInfo.DefaultThreadCurrentUICulture = value;
        Navigation.NavigateTo(Navigation.Uri, true);
    }
}