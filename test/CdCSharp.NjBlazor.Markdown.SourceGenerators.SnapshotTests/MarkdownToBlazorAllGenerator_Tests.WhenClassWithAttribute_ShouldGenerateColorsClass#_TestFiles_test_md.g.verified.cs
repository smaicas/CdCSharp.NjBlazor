//HintName: _TestFiles_test_md.g.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using CdCSharp.NjBlazor.Features.Markdown;

namespace TestApp;
public partial class _TestFiles_test_md : ComponentBase
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, MarkdownToRenderFragmentParser.ParseText(@"# Test Header
This is a test markdown file.
## Section
Some content here."));
    }
}