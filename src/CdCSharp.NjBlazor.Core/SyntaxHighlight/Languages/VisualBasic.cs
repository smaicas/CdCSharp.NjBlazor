using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;

namespace CdCSharp.SyntaxHighlight.Languages;

public partial class Definitions
{
    public static Definition VisualBasicDefinition = new(
        name: "VisualBasic",
        caseSensitive: false,
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
            beginsWith: "\"",
            endsWith: "\"",
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
            beginsWith: "'",
            endsWith: "\n",
            escapesWith: ""
        )
},
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
                "Xor",
                "WriteOnly",
                "WithEvents",
                "With",
                "Widening",
                "While",
                "When",
                "Wend",
                "Variant",
                "Using",
                "UShort",
                "ULong",
                "UInteger",
                "TypeOf",
                "TryCast",
                "Try",
                "True",
                "To",
                "Throw",
                "Then",
                "SyncLock",
                "Sub",
                "Structure",
                "String",
                "Stop",
                "Step",
                "Static",
                "Single",
                "Short",
                "Shared",
                "Shadows",
                "Set",
                "Select",
                "SByte",
                "Return",
                "Resume",
                "RemoveHandler",
                "REM",
                "ReDim",
                "ReadOnly",
                "RaiseEvent",
                "Public",
                "Protected",
                "Property",
                "Private",
                "Partial",
                "ParamArray",
                "Overrides",
                "Overridable",
                "Overloads",
                "OrElse",
                "Or",
                "Optional",
                "Option",
                "Operator",
                "On",
                "Of",
                "Object",
                "NotOverridable",
                "NotInheritable",
                "Nothing",
                "Not",
                "Next",
                "New",
                "Narrowing",
                "Namespace",
                "MyClass",
                "MyBase",
                "MustOverride",
                "MustInherit",
                "Module",
                "Mod",
                "Me",
                "Loop",
                "Long",
                "Like",
                "Lib",
                "Let",
                "IsNot",
                "Is",
                "Interface",
                "Integer",
                "Inherits",
                "In",
                "Imports",
                "Implements",
                "If",
                "Handles",
                "GoTo",
                "GoSub",
                "Global",
                "GetType",
                "Get",
                "Function",
                "Friend",
                "For",
                "Finally",
                "False",
                "Exit",
                "Event",
                "Error",
                "Erase",
                "Enum",
                "EndIf",
                "End",
                "ElseIf",
                "Else",
                "Each",
                "Double",
                "Do",
                "DirectCast",
                "Dim",
                "Delegate",
                "Default",
                "Declare",
                "Decimal",
                "Date",
                "CUShort",
                "CULng",
                "CUInt",
                "CType",
                "CStr",
                "CSng",
                "CShort",
                "CSByte",
                "Continue",
                "Const",
                "CObj",
                "CLng",
                "Class",
                "CInt",
                "Char",
                "CDec",
                "CDbl",
                "CDate",
                "CChar",
                "CByte",
                "CBool",
                "Catch",
                "Case",
                "Call",
                "ByVal",
                "Byte",
                "ByRef",
                "Boolean",
                "As",
                "AndAlso",
                "And",
                "Alias",
                "AddressOf",
                "AddHandler",
            ]
        )
},
    });
}