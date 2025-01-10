using CdCSharp.NjBlazor.Tools.ThemeGenerator.Cli;
using CdCSharp.NjBlazor.Tools.ThemeGenerator.Generators;

namespace CdCSharp.NjBlazor.Tools.ThemeGenerator;

/// <summary>
/// Contains a collection of internal static methods related to commands.
/// </summary>
internal static class Commands
{
    /// <summary>
    /// Generates icons using the provided command line pipe and builds an icons file.
    /// </summary>
    /// <param name="commandParser">The command line pipe used for generation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    internal static async Task GenerateIcons(CommandLinePipe commandParser) => await IconsClassGenerator.BuildIconsFile();

    /// <summary>
    /// Generates a theme CSS file based on the provided command line arguments.
    /// </summary>
    /// <param name="commandParser">The command line parser containing the arguments.</param>
    /// <returns>An asynchronous task representing the generation of the theme CSS file.</returns>
    internal static async Task GenerateTheme(CommandLinePipe commandParser)
    {
        string? rootPath = commandParser.GetArgumentWithRequiredValueOrDefault("-p", "--path");
        string? outputFolder = commandParser.GetArgumentWithRequiredValueOrDefault("-o", "--output");
        string? outputFile = commandParser.GetArgumentWithRequiredValueOrDefault("-f", "--file");

        rootPath ??= ".";
        outputFolder ??= "wwwroot";
        outputFile ??= "theme.css";

        await ThemeCssGenerator.BuildThemeCssFile(rootPath: rootPath, outputFolder: outputFolder, outputFile: outputFile);
    }

    /// <summary>
    /// Generates variables for the CSS file based on the command line arguments.
    /// </summary>
    /// <param name="commandParser">The command line parser.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    internal static async Task GenerateVariables(CommandLinePipe commandParser)
    {
        string? rootPath = commandParser.GetArgumentWithRequiredValueOrDefault("-p", "--path");
        string? outputFolder = commandParser.GetArgumentWithRequiredValueOrDefault("-o", "--output");
        string? outputFile = commandParser.GetArgumentWithRequiredValueOrDefault("-f", "--file");

        rootPath ??= ".";
        outputFolder ??= "wwwroot";
        outputFile ??= "variables.css";

        await ThemeCssGenerator.BuildVariablesCssFile(rootPath: rootPath, outputFolder: outputFolder, outputFile: outputFile);
    }
}

