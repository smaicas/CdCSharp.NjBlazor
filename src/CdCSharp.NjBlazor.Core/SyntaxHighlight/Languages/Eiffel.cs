using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;

namespace CdCSharp.SyntaxHighlight.Languages;

public partial class Definitions
{
    public static Definition EiffelDefinition = new(
        name: "Eiffel",
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
                "when",
                "variant",
                "until",
                "Unique",
                "undefine",
                "True",
                "then",
                "Strip",
                "separate",
                "select",
                "retry",
                "rescue",
                "require",
                "rename",
                "redefine",
                "prefix",
                "or",
                "once",
                "old",
                "obsolete",
                "not",
                "loop",
                "local",
                "like",
                "is",
                "invariant",
                "inspect",
                "inherit",
                "infix",
                "indexing",
                "implies",
                "if",
                "frozen",
                "from",
                "feature",
                "False",
                "external",
                "export",
                "expanded",
                "ensure",
                "end",
                "elseif",
                "else",
                "do",
                "deferred",
                "debug",
                "creation",
                "class",
                "check",
                "as",
                "and",
                "all",
                "alias",
            ]
        )
},
    });
}