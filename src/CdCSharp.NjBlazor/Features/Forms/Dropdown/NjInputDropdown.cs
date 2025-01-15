using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Forms.Dropdown;

/// <summary>
/// Represents a dropdown input control for selecting a value of type TValue. De-multiplexer for NjInputDropdownVariant
/// </summary>
/// <typeparam name="TValue">
/// The type of value that can be selected in the dropdown.
/// </typeparam>
[ComponentDeMux<NjInputDropdownVariant>]
public partial class NjInputDropdown<TValue> : NjInputDropdownBase<TValue>
{
}