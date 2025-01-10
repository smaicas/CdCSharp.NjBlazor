using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;

namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Engines;

public interface IEngine
{
    string Highlight(Definition definition, string input);
}