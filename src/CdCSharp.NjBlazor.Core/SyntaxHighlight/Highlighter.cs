using CdCSharp.NjBlazor.Core.SyntaxHighlight.Engines;
using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using CdCSharp.SyntaxHighlight.Languages;

namespace CdCSharp.NjBlazor.Core.SyntaxHighlight;

/// <summary>
/// Represents the Highlighter class.
/// </summary>
public class Highlighter
{
    /// <summary>
    /// Initializes a new instance of the Highlighter class with the specified engine.
    /// </summary>
    /// <param name="engine">
    /// The engine used for highlighting.
    /// </param>
    public Highlighter(IEngine engine) => Engine = engine;

    /// <summary>
    /// Gets or sets the engine associated with this object.
    /// </summary>
    public IEngine Engine { get; set; }

    /// <summary>
    /// Highlights the input text based on the specified definition.
    /// </summary>
    /// <param name="definitionName">
    /// The name of the definition to use for highlighting.
    /// </param>
    /// <param name="input">
    /// The input text to be highlighted.
    /// </param>
    /// <returns>
    /// The highlighted text if a valid definition is found; otherwise, returns the original input text.
    /// </returns>
    public string Highlight(string? definitionName, string input)
    {
        ArgumentNullException.ThrowIfNull(definitionName, nameof(definitionName));

        Definition? definition = GetDefinition(definitionName);

        return definition == null
            ? throw new ArgumentException("Parameter does not match any language definition", nameof(definitionName))
            : Engine.Highlight(definition, input);
    }

    private Definition? GetDefinition(string definitionName)
    {
        return definitionName.ToLower() switch
        {
            "aspx" => Definitions.AspxDefinition,
            "c" => Definitions.CDefinition,
            "cobol" => Definitions.CobolDefinition,
            "c++" or "cplusplus" => Definitions.CPlusPlusDefinition,
            "csharp" or "c#" => Definitions.CSharpDefinition,
            "eiffel" => Definitions.EiffelDefinition,
            "fortran" => Definitions.FortranDefinition,
            "haskell" => Definitions.HaskellDefinition,
            "html" => Definitions.HtmlDefinition,
            "java" => Definitions.JavaDefinition,
            "javascript" or "js" => Definitions.JavaScriptDefinition,
            "mercury" => Definitions.MercuryDefinition,
            "msil" => Definitions.MsilDefinition,
            "pascal" => Definitions.PascalDefinition,
            "perl" => Definitions.PerlDefinition,
            "php" => Definitions.PhpDefinition,
            "python" or "py" => Definitions.PythonDefinition,
            "ruby" => Definitions.RubyDefinition,
            "sql" => Definitions.SqlDefinition,
            "vbnet" or "visualbasicnet" => Definitions.VBNETDefinition,
            "vbscript" or "visualbasicscript" => Definitions.VBScriptDefinition,
            "vb" or "visualbasic" => Definitions.VisualBasicDefinition,
            "xml" => Definitions.XmlDefinition,
            _ => Definitions.CSharpDefinition,
        };
    }
}