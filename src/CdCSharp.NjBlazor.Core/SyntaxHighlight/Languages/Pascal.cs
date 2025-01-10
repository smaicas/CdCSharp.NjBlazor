using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;

namespace CdCSharp.SyntaxHighlight.Languages;

public partial class Definitions
{
    public static Definition PascalDefinition = new(
        name: "Pascal",
        caseSensitive: true,
        style: new Style(
            new ColorPair(
                foreColor: Color.FromName("black"),
                backColor: Color.FromName("transparent")
            ),
            new Font(
                name: "monospace",
                size: 16f,
                style: FontStyle.Regular
            )
        ),
        patterns: new Dictionary<string, Pattern>()
    {
{
    "Comment", new BlockPattern(
            name: "Comment",
            style: new Style(
                new ColorPair(
                    foreColor: Color.FromName("green"),
                    backColor: Color.FromName("transparent")
                ),
                new Font(
                    name: "monospace",
                    size: 16f,
                    style: FontStyle.Regular
                )
            ),
            beginsWith: "//",
            endsWith: "\n",
            escapesWith: ""
        )
},
{
    "MultiLineComment", new BlockPattern(
            name: "MultiLineComment",
            style: new Style(
                new ColorPair(
                    foreColor: Color.FromName("green"),
                    backColor: Color.FromName("transparent")
                ),
                new Font(
                    name: "monospace",
                    size: 16f,
                    style: FontStyle.Regular
                )
            ),
            beginsWith: "/*",
            endsWith: "*/",
            escapesWith: ""
        )
},
{
    "String", new BlockPattern(
            name: "String",
            style: new Style(
                new ColorPair(
                    foreColor: Color.FromName("darkred"),
                    backColor: Color.FromName("transparent")
                ),
                new Font(
                    name: "monospace",
                    size: 16f,
                    style: FontStyle.Regular
                )
            ),
            beginsWith: "\"",
            endsWith: "\"",
            escapesWith: "\\"
        )
},
{
    "WordGroup01", new WordPattern(
            name: "WordGroup01",
            style: new Style(
                new ColorPair(
                    foreColor: Color.FromName("blue"),
                    backColor: Color.FromName("transparent")
                ),
                new Font(
                    name: "monospace",
                    size: 16f,
                    style: FontStyle.Regular
                )
            ),
            words:
            [
                "xor",
                "with",
                "while",
                "virtual",
                "var",
                "uses",
                "until",
                "unit",
                "type",
                "to",
                "then",
                "string",
                "stdcall",
                "shr",
                "shl",
                "set",
                "repeat",
                "record",
                "program",
                "procedure",
                "packed",
                "overload",
                "os2call",
                "or",
                "of",
                "object",
                "not",
                "nil",
                "name",
                "mod",
                "library",
                "label",
                "interface",
                "inline",
                "inherited",
                "Index",
                "in",
                "implementation",
                "if",
                "goto",
                "function",
                "Forward",
                "for",
                "file",
                "external",
                "exports",
                "Export",
                "end",
                "else",
                "downto",
                "do",
                "div",
                "destructor",
                "Declare",
                "declare",
                "Conv",
                "constructor",
                "const",
                "Code",
                "Cdecl",
                "case",
                "begin",
                "Assembler",
                "asm",
                "array",
                "and",
                "Absolute",
            ]
        )
},
    });
}