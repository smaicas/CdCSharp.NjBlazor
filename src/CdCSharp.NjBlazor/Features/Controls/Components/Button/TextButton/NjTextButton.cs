using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Button.TextButton;

/// <summary>
/// Represents a custom text button control that inherits from NjTextButtonBase. De-multiplexer for NjTextButtonVariant
/// </summary>
[ComponentDeMux<NjTextButtonVariant>]
public partial class NjTextButton : NjTextButtonBase
{
}