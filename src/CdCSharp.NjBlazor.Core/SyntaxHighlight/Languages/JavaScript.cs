using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;

namespace CdCSharp.SyntaxHighlight.Languages;

public partial class Definitions
{
    public static Definition JavaScriptDefinition = new(
        name: "JavaScript",
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
    "Function", new WordPattern(
            name: "Function",
            style: new Style(
                new ColorPair(
                    foreColor: Color.FromName("purple"),
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
                "GetObject",
                "ScriptEngine",
                "ScriptEngineBuildVersion",
                "ScriptEngineMajorVersion",
                "ScriptEngineMinorVersion",
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
                "abs",
                "acos",
                "anchor",
                "apply",
                "asin",
                "atan",
                "atan2",
                "atEnd",
                "big",
                "blink",
                "bold",
                "call",
                "ceil",
                "charAt",
                "charCodeAt",
                "compile",
                "concat",
                "cos",
                "decodeURI",
                "decodeURIComponent",
                "dimensions",
                "encodeURI",
                "encodeURIComponent",
                "escape",
                "eval",
                "exec",
                "exp",
                "fixed",
                "floor",
                "fontcolor",
                "fontsize",
                "fromCharCode",
                "getDate",
                "getDay",
                "getFullYear",
                "getHours",
                "getItem",
                "getMilliseconds",
                "getMinutes",
                "getMonth",
                "getSeconds",
                "getTime",
                "getTimezoneOffset",
                "getUTCDate",
                "getUTCDay",
                "getUTCFullYear",
                "getUTCHours",
                "getUTCMilliseconds",
                "getUTCMinutes",
                "getUTCMonth",
                "getUTCSeconds",
                "getVarDate",
                "getYear",
                "hasOwnProperty",
                "indexOf",
                "isFinite",
                "isNaN",
                "isPrototypeOf",
                "italics",
                "item",
                "join",
                "lastIndexOf",
                "lbound",
                "link",
                "localeCompare",
                "log",
                "match",
                "max",
                "min",
                "moveFirst",
                "moveNext",
                "parse",
                "parseFloat",
                "parseInt",
                "pop",
                "pow",
                "push",
                "random",
                "replace",
                "reverse",
                "round",
                "search",
                "setDate",
                "setFullYear",
                "setHours",
                "setMilliseconds",
                "setMinutes",
                "setMonth",
                "setSeconds",
                "setTime",
                "setUTCDate",
                "setUTCFullYear",
                "setUTCHours",
                "setUTCMilliseconds",
                "setUTCMinutes",
                "setUTCMonth",
                "setUTCSeconds",
                "setYear",
                "shift",
                "sin",
                "slice",
                "small",
                "sort",
                "splice",
                "split",
                "sqrt",
                "strike",
                "sub",
                "substr",
                "substring",
                "sup",
                "tan",
                "test",
                "toArray",
                "toDateString",
                "toExponential",
                "toFixed",
                "toGMTString",
                "toLocaleDateString",
                "toLocaleLowerCase",
                "toLocaleString",
                "toLocaleTimeString",
                "toLocaleUpperCase",
                "toLowerCase",
                "toPrecision",
                "toString",
                "toTimeString",
                "toUpperCase",
                "toUTCString",
                "ubound",
                "unescape",
                "unshift",
                "UTC",
                "valueOf",
                "write",
                "writeln",
            ]
        )
},
{
    "Object", new WordPattern(
            name: "Object",
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
                "ActiveXObject",
                "Array",
                "arguments",
                "Boolean",
                "Date",
                "Debug",
                "Enumerator",
                "Error",
                "Function",
                "Global",
                "Math",
                "Number",
                "Object",
                "RegExp",
                "String",
                "VBArray",
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
                "function",
                "in",
                "while",
                "break",
                "continue",
                "return",
                "try",
                "throw",
                "catch",
                "finally",
                "checked",
                "unchecked",
                "fixed",
                "lock",
                "this",
                "var",
                "with",
                "@cc_on",
                "@if",
                "@set",
                "@elif",
                "@else",
                "@end",
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
    "Property", new WordPattern(
            name: "Property",
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
                "arguments",
                "callee",
                "caller",
                "constructor",
                "description",
                "E",
                "global",
                "ignoreCase",
                "index",
                "Infinity",
                "input",
                "lastIndex",
                "lastParent",
                "leftContext",
                "length",
                "LN2",
                "LN10",
                "LOG2E",
                "LOG10E",
                "MAX",
                "message",
                "MIN",
                "multiline",
                "name",
                "NaN",
                "NEGATIVE_INFINITY",
                "number",
                "PI",
                "POSITIVE_INFINITY",
                "propertyIsEnumerable",
                "prototype",
                "rightContext",
                "source",
                "SQRT1_2",
                "SQRT2",
                "undefined",
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
            escapesWith: "\\"
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
    });
}