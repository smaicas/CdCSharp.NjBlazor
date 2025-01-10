using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;

namespace CdCSharp.SyntaxHighlight.Languages;

public partial class Definitions
{
    public static Definition VBNETDefinition = new(
        name: "VBNET",
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
                "Alias",
                "Ansi",
                "As",
                "Assembly",
                "Auto",
                "ByRef",
                "ByVal",
                "Case",
                "Default",
                "DirectCast",
                "Each",
                "Else",
                "ElseIf",
                "End",
                "Error",
                "Explicit",
                "False",
                "For",
                "Friend",
                "Handles",
                "Implements",
                "In",
                "Is",
                "Lib",
                "Loop",
                "Me",
                "Module",
                "MustInherit",
                "MustOverride",
                "MyBase",
                "MyClass",
                "New",
                "Next",
                "Nothing",
                "NotInheritable",
                "NotOverridable",
                "Off",
                "On",
                "Option",
                "Optional",
                "Overloads",
                "Overridable",
                "Overrides",
                "ParamArray",
                "Preserve",
                "Private",
                "Protected",
                "Public",
                "ReadOnly",
                "Resume",
                "Shadows",
                "Shared",
                "Static",
                "Step",
                "Strict",
                "Then",
                "To",
                "True",
                "TypeOf",
                "Unicode",
                "Until",
                "When",
                "While",
                "WithEvents",
                "WriteOnly",
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
                "AddHandler",
                "Call",
                "Class",
                "Const",
                "Declare",
                "Delegate",
                "Dim",
                "Do",
                "Loop",
                "End",
                "Enum",
                "Erase",
                "Error",
                "Event",
                "Exit",
                "For",
                "For",
                "Next",
                "Function",
                "Get",
                "GoTo",
                "If",
                "Then",
                "Else",
                "Implements",
                "Imports",
                "Inherits",
                "Interface",
                "Mid",
                "Module",
                "Namespace",
                "On",
                "Option",
                "Property",
                "RaiseEvent",
                "Randomize",
                "ReDim",
                "REM",
                "RemoveHandler",
                "Resume",
                "Return",
                "Select",
                "Case",
                "Set",
                "Stop",
                "Structure",
                "Sub",
                "SyncLock",
                "Throw",
                "Try",
                "Catch",
                "Finally",
                "While",
                "End",
                "With",
                "End",
            ]
        )
},
{
    "Property", new WordPattern(
            name: "Property",
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
                "Count",
                "DateString",
                "Description",
                "Erl",
                "HelpContext",
                "HelpFile",
                "Item",
                "LastDLLError",
                "Now",
                "Number",
                "ScriptEngine",
                "ScriptEngineBuildVersion",
                "ScriptEngineMajorVersion",
                "ScriptEngineMinorVersion",
                "Source",
                "TimeOfDay",
                "Timer",
                "TimeString",
                "Today",
            ]
        )
},
{
    "Object", new WordPattern(
            name: "Object",
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
                "Err",
            ]
        )
},
{
    "Method", new WordPattern(
            name: "Method",
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
                "Add",
                "Clear",
                "Raise",
                "Remove",
            ]
        )
},
{
    "Function", new WordPattern(
            name: "Function",
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
                "Abs",
                "AppActivate",
                "Asc",
                "AscW",
                "Atn",
                "Beep",
                "CallByName",
                "CBool",
                "CBtye",
                "CChar",
                "CDate",
                "CDbl",
                "CDec",
                "ChDir",
                "ChDrive",
                "Choose",
                "Chr",
                "ChrW",
                "CInt",
                "CLng",
                "CObj",
                "Command",
                "Conversion",
                "Cos",
                "CreateObject",
                "CShort",
                "CSng",
                "CStr",
                "CType",
                "CurDir",
                "DateAdd",
                "DateDiff",
                "DatePart",
                "DateSerial",
                "DateValue",
                "Day",
                "DDB",
                "DeleteSetting",
                "Dir",
                "Environ",
                "EOF",
                "ErrorToString",
                "Exp",
                "FileAttr",
                "FileClose",
                "FileCopy",
                "FileDateTime",
                "FileGet",
                "FileGetObject",
                "FileLen",
                "FileOpen",
                "FilePut",
                "FilePutObject",
                "FileWidth",
                "Filter",
                "Fix",
                "Format",
                "FormatCurrency",
                "FormatDateTime",
                "FormatNumber",
                "FormatPercent",
                "FreeFile",
                "FV",
                "GetAllSettings",
                "GetAttr",
                "GetChar",
                "GetException",
                "GetObject",
                "GetSetting",
                "Hex",
                "Hour",
                "IIf",
                "Input",
                "InputBox",
                "InputString",
                "InStr",
                "InStrRev",
                "Int",
                "IPmt",
                "IRR",
                "IsArray",
                "IsDate",
                "IsDBNull",
                "IsError",
                "IsNothing",
                "IsNumeric",
                "IsReference",
                "Join",
                "Kill",
                "LBound",
                "LCase",
                "Left",
                "Len",
                "LineInput",
                "Loc",
                "Lock",
                "LOF",
                "Log",
                "LSet",
                "LTrim",
                "Mid",
                "Minute",
                "MIRR",
                "MkDir",
                "Month",
                "MonthName",
                "MsgBox",
                "NPer",
                "NPV",
                "Oct",
                "Partition",
                "Pmt",
                "PPmt",
                "Print",
                "PV",
                "QBColor",
                "Rate",
                "Rename",
                "Replace",
                "Reset",
                "RGB",
                "Right",
                "RmDir",
                "Rnd",
                "Round",
                "RSet",
                "RTrim",
                "SaveSetting",
                "Second",
                "Seek",
                "SetAttr",
                "Sgn",
                "Shell",
                "Sin",
                "SLN",
                "Space",
                "SPC",
                "Split",
                "Sqr",
                "Str",
                "StrComp",
                "StrConv",
                "StrDup",
                "StrReverse",
                "Switch",
                "SYD",
                "SystemTypeName",
                "TAB",
                "Tan",
                "TimeSerial",
                "TimeValue",
                "Trim",
                "TypeName",
                "UBound",
                "UCase",
                "Unlock",
                "Val",
                "VarType",
                "VbTypeName",
                "Weekday",
                "WeekdayName",
                "Write",
                "Year",
            ]
        )
},
{
    "DataType", new WordPattern(
            name: "DataType",
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
                "Boolean",
                "Byte",
                "Char",
                "Date",
                "Decimal",
                "Double",
                "Integer",
                "Long",
                "Object",
                "Short",
                "Single",
                "String",
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
                "AddressOf",
                "And",
                "AndAlso",
                "GetType",
                "Is",
                "Like",
                "Mod",
                "Not",
                "Or",
                "OrElse",
                "Xor",
            ]
        )
},
    });
}