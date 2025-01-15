using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Button.LinkButton;

/// <summary>
/// Represents a custom link button control that inherits from NjLinkButtonBase. De-multiplexer for NjLinkButtonVariant
/// </summary>
[ComponentDeMux<NjLinkButtonVariant>]
public partial class NjLinkButton : NjLinkButtonBase
{
}