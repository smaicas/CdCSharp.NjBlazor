using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.ThemeMode.Components.ModeSelector;

/// <summary>
/// Represents a theme mode selector for the NjThemeModeSelectorBase class.
/// De-multiplexer for NjThemeModeSelectorVariant
/// </summary>
[ComponentDeMux<NjThemeModeSelectorVariant>]
public partial class NjThemeModeSelector : NjThemeModeSelectorBase
{
}