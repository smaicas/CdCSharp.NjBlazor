using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;

namespace CdCSharp.SyntaxHighlight.Languages;

public partial class Definitions
{
    public static Definition RubyDefinition = new(
        name: "Ruby",
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
            beginsWith: "#",
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
                "yield",
                "while",
                "when",
                "until",
                "unless",
                "undef",
                "true",
                "then",
                "super",
                "self",
                "return",
                "retry",
                "rescue",
                "redo",
                "or",
                "not",
                "nil",
                "next",
                "module",
                "in",
                "if",
                "for",
                "false",
                "ensure",
                "END",
                "end",
                "elsif",
                "else",
                "do",
                "defined?",
                "def",
                "class",
                "case",
                "break",
                "BEGIN",
                "begin",
                "and",
                "alias",
                "__LINE__",
                "__FILE__",
            ]
        )
},
{
    "WordGroup02", new WordPattern(
            name: "WordGroup02",
            style: new Style(
                new ColorPair(
                    foreColor: Color.FromName("darkblue"),
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
                "untrace_var",
                "trap",
                "trace_var",
                "throw",
                "test",
                "system",
                "syscall",
                "sub",
                "sub",
                "String",
                "srand",
                "sprintf",
                "split",
                "sleep",
                "select",
                "require",
                "readlines",
                "readline",
                "rand",
                "raise",
                "puts",
                "putc",
                "proc",
                "printf",
                "print",
                "p",
                "open",
                "loop",
                "local_variables",
                "load",
                "lamda",
                "iterator",
                "Integer",
                "import",
                "hex",
                "gt",
                "gsub",
                "gsub",
                "goto",
                "gmtime",
                "global_variables",
                "glob",
                "gets",
                "format",
                "fork",
                "Float",
                "fail",
                "exp",
                "exit",
                "exit",
                "exists",
                "exec",
                "eval",
                "chop",
                "chop",
                "chomp",
                "chomp",
                "catch",
                "caller",
                "binding",
                "autoload",
                "at_exit",
                "Array",
            ]
        )
},
{
    "WordGroup03", new WordPattern(
            name: "WordGroup03",
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
                "ZeroDivisionError",
                "TypeError",
                "Time",
                "ThreadError",
                "SystemStackError",
                "SystemExit",
                "SystemCallError",
                "SyntaxError",
                "Struct",
                "String",
                "StandardError",
                "SignalException",
                "SecurityError",
                "RuntimeError",
                "Regexp",
                "Range",
                "Proc",
                "Object",
                "Numeric",
                "NotImplementError",
                "NilClass",
                "NameError",
                "Module",
                "MatchingData",
                "LocalJumpError",
                "LoadError",
                "IOError",
                "IO",
                "Interrupt",
                "Integer",
                "IndexError",
                "Hash",
                "Float",
                "Fixnum",
                "File",
                "fatal",
                "Exception",
                "EOFError",
                "Dir",
                "Data",
                "Class",
                "Bignum",
                "Array",
                "ArgumentError",
            ]
        )
},
    });
}