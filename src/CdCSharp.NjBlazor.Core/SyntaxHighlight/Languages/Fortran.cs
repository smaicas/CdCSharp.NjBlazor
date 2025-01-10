using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;

namespace CdCSharp.SyntaxHighlight.Languages;

public partial class Definitions
{
    public static Definition FortranDefinition = new(
        name: "Fortran",
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
                "WRITE",
                "WHILE",
                "UNION",
                "THEN",
                "SUBROUTINE",
                "STRUCTURE",
                "STOP",
                "SELECT",
                "SAVE",
                "REWIND",
                "RETURN",
                "RECORD",
                "REAL",
                "READ",
                "PROGRAM",
                "PRINT",
                "PRECISION",
                "PAUSE",
                "PARAMETER",
                "OPEN",
                "NAMELIST",
                "MAP",
                "LOGICAL",
                "LOCKING",
                "INTRINSIC",
                "INTERFACE TO",
                "INTEGER",
                "INQUIRE",
                "INCLUDE",
                "IMPLICIT",
                "IF",
                "GOTO",
                "FUNCTION",
                "FORMAT",
                "EXTERNAL",
                "EXIT",
                "EQUIVALENCE",
                "ENTRY",
                "ENDFILE",
                "END",
                "END",
                "END",
                "ELSE",
                "DOUBLE",
                "DO",
                "DO",
                "DIMENSION",
                "DEALLOCATE",
                "DATA",
                "DATA",
                "CYCLE",
                "CONTINUE",
                "COMPLEX",
                "COMPLEX",
                "COMMON",
                "CLOSE",
                "CHARACTER",
                "CASE",
                "CASE",
                "CALL",
                "BYTE",
                "BLOCK",
                "BACKSPACE",
                "AUTOMATIC",
                "ASSIGN",
                "ALLOCATE",
            ]
        )
},
    });
}