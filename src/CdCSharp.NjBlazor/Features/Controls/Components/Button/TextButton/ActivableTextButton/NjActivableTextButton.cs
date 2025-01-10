using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Button.TextButton.ActivableTextButton;

/// <summary>
/// Represents a custom activable text button control that extends the functionality of the base activable text button.
/// De-multiplexer for NjActivableTextButtonVariant
/// </summary>
[ComponentDeMux<NjActivableTextButtonVariant>]
public partial class NjActivableTextButton : NjActivableTextButtonBase
{
}

