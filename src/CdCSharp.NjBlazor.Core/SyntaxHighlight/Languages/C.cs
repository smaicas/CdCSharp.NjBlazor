using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;

namespace CdCSharp.SyntaxHighlight.Languages;

public partial class Definitions
{
    public static Definition CDefinition = new(
        name: "C",
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
                "write",
                "while",
                "volatile",
                "void",
                "unsigned",
                "union",
                "typedef",
                "time",
                "tanh",
                "tan",
                "system",
                "switch",
                "struct",
                "strcpy",
                "strcmp",
                "static",
                "srand",
                "sqrt",
                "sizeof",
                "sinh",
                "sin",
                "signed",
                "signal",
                "short",
                "scanf",
                "return",
                "rename",
                "remove",
                "register",
                "read",
                "rand",
                "putchar",
                "printf",
                "pow",
                "open",
                "malloc",
                "long",
                "log10",
                "log",
                "labs",
                "int",
                "if",
                "goto",
                "getenv",
                "getchar",
                "free",
                "for",
                "floor",
                "float",
                "fabs",
                "extern",
                "exp",
                "exit",
                "enum",
                "else",
                "double",
                "do",
                "div",
                "default",
                "cosh",
                "cos",
                "continue",
                "const",
                "close",
                "clock",
                "char",
                "ceil",
                "case",
                "calloc",
                "break",
                "auto",
                "atol",
                "atoi",
                "atof",
                "atan",
                "asin",
                "acos",
                "abs",
                "abort",
            ]
        )
},
    });
}