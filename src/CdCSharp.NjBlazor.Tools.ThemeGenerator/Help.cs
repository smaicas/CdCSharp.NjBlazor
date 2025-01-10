using CdCSharp.NjBlazor.Tools.ThemeGenerator.Cli;

namespace CdCSharp.NjBlazor.Tools.ThemeGenerator;

/// <summary>
/// Contains helper methods and functions for internal use.
/// </summary>
internal static class Help
{
    /// <summary>
    /// Displays help information for the command line options.
    /// </summary>
    /// <param name="commandLinePipe">The command line pipe object.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    internal static Task ShowHelp(CommandLinePipe commandLinePipe)
    {
        Dictionary<string, string> parametersHelp = new(){
    {"-h, --help", "Shows help" },
    {"-i, --icons", "Icons class generation" },
    {"-v, --variables <options>", "Generate Variables CSS" },
    {"-t, --theme <options>", "Generate Theme CSS" },
    };

        Console.WriteLine();
        foreach (KeyValuePair<string, string> parameter in parametersHelp)
        {
            Console.WriteLine($"{parameter.Key,-40}{parameter.Value}");
        }
        Console.WriteLine();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Displays help information for the available command line parameters related to icons.
    /// </summary>
    /// <param name="commandLinePipe">The command line pipe object.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    internal static Task ShowIconsHelp(CommandLinePipe commandLinePipe)
    {
        Dictionary<string, string> parametersHelp = new(){
    {"-h, --help", "Shows help" },
    {"-i, --icons", "Icons generation" }
    };

        Console.WriteLine();
        foreach (KeyValuePair<string, string> parameter in parametersHelp)
        {
            Console.WriteLine($"{parameter.Key,-40}{parameter.Value}");
        }
        Console.WriteLine();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Displays help information about the theme command line parameters.
    /// </summary>
    /// <param name="commandLinePipe">The command line pipe object.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    internal static Task ShowThemeHelp(CommandLinePipe commandLinePipe)
    {
        Dictionary<string, string> parametersHelp = new(){
                {"-h, --help", "Shows help" },
                {"-p, --path", "Root folder (default .)" },
                {"-o, --output", "Output folder (default wwwroot)" },
                {"-f, --file", "Output filename (default theme.css)" },
        };

        Console.WriteLine();
        foreach (KeyValuePair<string, string> parameter in parametersHelp)
            Console.WriteLine($"{parameter.Key,-40}{parameter.Value}");

        Console.WriteLine("Use: --variables -p . -o wwwroot -f theme.css");

        return Task.CompletedTask;
    }

    /// <summary>
    /// Displays help information about the available variables and their usage.
    /// </summary>
    /// <param name="commandLinePipe">The command line pipe used for communication.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    internal static Task ShowVariablesHelp(CommandLinePipe commandLinePipe)
    {
        Dictionary<string, string> parametersHelp = new(){
                {"-h, --help", "Shows help" },
                {"-p, --path", "Root folder (default .)" },
                {"-o, --output", "Output folder (default wwwroot)" },
                {"-f, --file", "Output filename (default variables.css)" },
        };

        Console.WriteLine();
        foreach (KeyValuePair<string, string> parameter in parametersHelp)
            Console.WriteLine($"{parameter.Key,-40}{parameter.Value}");

        Console.WriteLine("Use: --variables -p . -o wwwroot -f variables.css");

        return Task.CompletedTask;
    }
}