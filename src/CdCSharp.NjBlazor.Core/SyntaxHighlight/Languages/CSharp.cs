using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;

namespace CdCSharp.SyntaxHighlight.Languages;

public partial class Definitions
{
    public static Definition CSharpDefinition = new(
        name: "CSharp",
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
    "Keyword", new WordPattern(
            name: "Keyword",
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
                "as",
                "is",
                "new",
                "sizeof",
                "typeof",
                "true",
                "false",
                "stackalloc",
                "explicit",
                "implicit",
                "operator",
                "base",
                "this",
                "null",
            ]
        )
},
{
    "Namespace", new WordPattern(
            name: "Namespace",
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
                "namespace",
                "using",
            ]
        )
},
{
    "MethodParameter", new WordPattern(
            name: "MethodParameter",
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
                "params",
                "ref",
                "out",
            ]
        )
},
{
    "Statement", new WordPattern(
            name: "Statement",
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
                "if",
                "else",
                "switch",
                "case",
                "do",
                "for",
                "foreach",
                "in",
                "while",
                "break",
                "continue",
                "goto",
                "return",
                "try",
                "throw",
                "catch",
                "finally",
                "checked",
                "unchecked",
                "fixed",
                "lock",
            ]
        )
},
{
    "Modifier", new WordPattern(
            name: "Modifier",
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
                "internal",
                "private",
                "protected",
                "public",
                "abstract",
                "const",
                "default",
                "event",
                "extern",
                "override",
                "readonly",
                "sealed",
                "static",
                "unsafe",
                "virtual",
                "volatile",
            ]
        )
},
{
    "ValueType", new WordPattern(
            name: "ValueType",
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
                "void",
                "bool",
                "byte",
                "char",
                "decimal",
                "double",
                "enum",
                "float",
                "int",
                "long",
                "sbyte",
                "short",
                "struct",
                "uint",
                "ulong",
                "ushort",
            ]
        )
},
{
    "ReferenceType", new WordPattern(
            name: "ReferenceType",
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
                "class",
                "interface",
                "delegate",
                "object",
                "string",
            ]
        )
},
{
    "Operator", new WordPattern(
            name: "Operator",
            style: new Style(
                new ColorPair(
                    foreColor: Color.FromName("red"),
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
                "+",
                "-",
                "=",
                "%",
                "*",
                "/",
                "\\s&\\s",
                "|",
            ]
        )
},
{
    "String", new BlockPattern(
            name: "String",
            style: new Style(
                new ColorPair(
                    foreColor: Color.FromName("#666666"),
                    backColor: Color.FromName("transparent")
                ),
                new Font(
                    name: "monospace",
                    size: 16f,
                    style: FontStyle.Regular
                )
            ),
            beginsWith: "&quot;",
            endsWith: "&quot;",
            escapesWith: "\\"
        )
},
{
    "VerbatimString", new BlockPattern(
            name: "VerbatimString",
            style: new Style(
                new ColorPair(
                    foreColor: Color.FromName("#666666"),
                    backColor: Color.FromName("transparent")
                ),
                new Font(
                    name: "monospace",
                    size: 16f,
                    style: FontStyle.Regular
                )
            ),
            beginsWith: "@&quot;",
            endsWith: "&quot;",
            escapesWith: ""
        )
},
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
    "PreprocessorDirective", new WordPattern(
            name: "PreprocessorDirective",
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
                "#if",
                "#else",
                "#elif",
                "#endif",
                "#define",
                "#undef",
                "#warning",
                "#error",
                "#line",
                "#region",
                "#endregion",
                "#pragma",
            ]
        )
},
    });
}