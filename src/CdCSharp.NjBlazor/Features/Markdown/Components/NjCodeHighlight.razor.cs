using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.SyntaxHighlight;
using CdCSharp.NjBlazor.Core.SyntaxHighlight.Engines;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Markdown.Components;

/// <summary>
/// Represents a component for code highlighting in a NJ application.
/// </summary>
/// <seealso cref="NjComponentBase" />
public partial class NjCodeHighlight : NjComponentBase
{
    private RenderFragment? _childContent;

    /// <summary>
    /// Gets or sets the child content to be rendered.
    /// </summary>
    /// <remarks>
    /// If the 'Code' property is not null, the content will be highlighted using the specified language.
    /// </remarks>
    /// <value>
    /// The child content to be rendered.
    /// </value>
    [Parameter]
    public RenderFragment? ChildContent
    {
        get
        {
            if (Code != null)
            {
                Highlighter highlighter = new(new HtmlEngine());
                Code = highlighter.Highlight(Language?.ToString(), Code);
                return builder => builder.AddMarkupContent(0, Code);
            }
            return _childContent;
        }
        set => _childContent = value;
    }

    /// <summary>
    /// Gets or sets the code parameter.
    /// </summary>
    /// <value>
    /// The code parameter.
    /// </value>
    [Parameter]
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets the syntax highlighting language for the code.
    /// </summary>
    /// <value>
    /// A nullable SyntaxHighlightLanguage enum value representing the language for syntax highlighting.
    /// </value>
    [Parameter]
    public SyntaxHighlightLanguage? Language { get; set; }
}