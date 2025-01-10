using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;

namespace CdCSharp.SyntaxHighlight.Languages;

public partial class Definitions
{
    public static Definition CPlusPlusDefinition = new(
        name: "CPlusPlus",
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
                "while",
                "volatile",
                "void",
                "virtual",
                "using",
                "unsigned",
                "union",
                "u8",
                "u64",
                "u32",
                "u16",
                "u128",
                "typename",
                "typedef",
                "try",
                "true",
                "throw",
                "this",
                "template",
                "switch",
                "struct",
                "string",
                "static_cast",
                "static",
                "sizeof",
                "signed",
                "short",
                "s8",
                "s64",
                "s32",
                "s16",
                "s128",
                "return",
                "reinterpret_cast",
                "register",
                "public",
                "protected",
                "private",
                "or",
                "operator",
                "not",
                "new",
                "near",
                "namespace",
                "mutable",
                "long",
                "int",
                "inline",
                "if",
                "huge",
                "goto",
                "friend",
                "for",
                "float",
                "fixed",
                "finally",
                "far",
                "false",
                "f32",
                "extern",
                "explicit",
                "except",
                "enum",
                "else",
                "dynamic_cast",
                "double",
                "do",
                "delete",
                "default",
                "continue",
                "const_cast",
                "const",
                "class",
                "char",
                "catch",
                "case",
                "break",
                "bool",
                "auto",
                "asm",
                "and",
                "#warning",
                "#undef",
                "#pragma",
                "#line",
                "#include",
                "#ifndef",
                "#ifdef",
                "#if",
                "#error",
                "#endif",
                "#else",
                "#elif",
                "#define",
            ]
        )
},
    });
}