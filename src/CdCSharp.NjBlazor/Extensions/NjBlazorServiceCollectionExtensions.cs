using CdCSharp.NjBlazor.Features.Audio.Abstractions;
using CdCSharp.NjBlazor.Features.ColorPicker.Abstractions;
using CdCSharp.NjBlazor.Features.Controls.Abstractions;
using CdCSharp.NjBlazor.Features.Forms.Checkbox.Abstractions;
using CdCSharp.NjBlazor.Features.Forms.Color.Abstractions;
using CdCSharp.NjBlazor.Features.Forms.Date.Abstractions;
using CdCSharp.NjBlazor.Features.Forms.Dropdown.Abstractions;
using CdCSharp.NjBlazor.Features.Forms.File.Abstractions;
using CdCSharp.NjBlazor.Features.Forms.Number.Abstractions;
using CdCSharp.NjBlazor.Features.Forms.Radio.Abstractions;
using CdCSharp.NjBlazor.Features.Forms.Range.Abstractions;
using CdCSharp.NjBlazor.Features.Forms.Text.Abstractions;
using CdCSharp.NjBlazor.Features.Layout.Abstractions;
using CdCSharp.NjBlazor.Features.Localization.Abstractions;
using CdCSharp.NjBlazor.Features.Markdown.Abstractions;
using CdCSharp.NjBlazor.Features.Media.Abstractions;
using CdCSharp.NjBlazor.Features.ResourceAccess.Abstractions;
using CdCSharp.NjBlazor.Features.ThemeMode.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static partial class NjBlazorServiceCollectionExtensions
{
    /// <summary>
    /// Adds NjBlazor services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection" /> to add the services to.
    /// </param>
    /// <param name="options">
    /// An optional action to configure the <see cref="NjBlazorSettings" />.
    /// </param>
    /// <remarks>
    /// This method adds various NjBlazor services to the service collection based on the provided settings.
    /// </remarks>
    public static void AddNjBlazor(this IServiceCollection services, Action<NjBlazorSettings>? options = null)
    {
        NjBlazorSettings njSettings = new();

        options?.Invoke(njSettings);

        //services.AddNjBlazorCssInclude(njSettings.CssIncludeSettings);

        services.AddNjBlazorAudio(njSettings.AudioSettings);
        services.AddNjBlazorColorPicker(njSettings.ColorPickerSettings);
        services.AddNjBlazorLayout(njSettings.LayoutSettings);
        services.AddNjBlazorControls(njSettings.ControlsSettings);
        services.AddNjBlazorDeviceManager();
        services.AddNjBlazorDom();
        services.AddNjBlazorDraggable();
        services.AddNjBlazorFocusHolder();
        services.AddNjBlazorLocalization(njSettings.LocalizationSettings);
        services.AddNjBlazorResourceAccess(njSettings.ResourceAccessSettings);
        services.AddNjBlazorMarkdown(njSettings.MarkdownSettings);
        services.AddNjBlazorMedia(njSettings.MediaSettings);
        services.AddNjBlazorTextPattern();
        services.AddNjBlazorThemeMode(njSettings.ThemeModeSettings);

        services.AddNjBlazorFormsCheckbox(njSettings.FormsCheckboxSettings);
        services.AddNjBlazorFormsColor(njSettings.FormsColorSettings);
        services.AddNjBlazorFormsDate(njSettings.FormsDateSettings);
        services.AddNjBlazorFormsDropdown(njSettings.FormsDropdownSettings);
        services.AddNjBlazorFormsFile(njSettings.FormsFileSettings);
        services.AddNjBlazorFormsNumber(njSettings.FormsNumberSettings);
        services.AddNjBlazorFormsRadio(njSettings.FormsRadioSettings);
        services.AddNjBlazorFormsRange(njSettings.FormsRangeSettings);
        services.AddNjBlazorFormsText(njSettings.FormsTextSettings);
    }

    /// <summary>
    /// Represents the settings for a Nj Blazor framework.
    /// </summary>
    public sealed record NjBlazorSettings
    {
        /// <summary>
        /// Gets or sets the localization settings for the application.
        /// </summary>
        public LocalizationSettings LocalizationSettings { get; set; } = new();

        ///// <summary>Gets or sets the CSS include settings for the application.</summary>
        //public CssIncludeSettings CssIncludeSettings { get; set; } = new();

        public NjAudioSettings AudioSettings { get; set; } = new();
        public NjColorPickerSettings ColorPickerSettings { get; set; } = new();
        public NjControlsSettings ControlsSettings { get; set; } = new();
        public NjFormsCheckboxSettings FormsCheckboxSettings { get; set; } = new();
        public NjFormsColorSettings FormsColorSettings { get; set; } = new();
        public NjFormsDateSettings FormsDateSettings { get; set; } = new();
        public NjFormsDropdownSettings FormsDropdownSettings { get; set; } = new();
        public NjFormsFileSettings FormsFileSettings { get; set; } = new();
        public NjFormsNumberSettings FormsNumberSettings { get; set; } = new();
        public NjFormsRadioSettings FormsRadioSettings { get; set; } = new();
        public NjFormsRangeSettings FormsRangeSettings { get; set; } = new();
        public NjFormsTextSettings FormsTextSettings { get; set; } = new();
        public NjLayoutSettings LayoutSettings { get; set; } = new();
        public NjMarkdownSettings MarkdownSettings { get; set; } = new();
        public NjMediaSettings MediaSettings { get; set; } = new();
        public NjResourceAccessSettings ResourceAccessSettings { get; set; } = new();
        public NjThemeModeSettings ThemeModeSettings { get; set; } = new();
    }
}