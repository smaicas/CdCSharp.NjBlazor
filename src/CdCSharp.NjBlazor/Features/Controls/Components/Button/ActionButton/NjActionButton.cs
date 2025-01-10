using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Button.ActionButton;

/// <summary>
/// Represents a custom action button that inherits from the base action button class.
/// De-multiplexer for NjActionButtonVariant.
/// </summary>
[ComponentDeMux<NjActionButtonVariant>]
public partial class NjActionButton : NjActionButtonBase { }
