using CdCSharp.FluentCli;
using CdCSharp.FluentCli.Abstractions;
using CdCSharp.NjBlazor.Tools.ThemeGenerator.Generators;

public class ThemeArgs
{
    [Arg("path", "Root folder", "p")]
    public string Path { get; set; } = ".";

    [Arg("output", "Output folder", "o")]
    public string Output { get; set; } = "wwwroot";

    [Arg("file", "Output filename", "f")]
    public string File { get; set; } = "theme.css";
}

public class VariablesArgs
{
    [Arg("path", "Root folder", "p")]
    public string Path { get; set; } = ".";

    [Arg("output", "Output folder", "o")]
    public string Output { get; set; } = "wwwroot";

    [Arg("file", "Output filename", "f")]
    public string File { get; set; } = "variables.css";
}

public class ResourcesArgs
{
    [Arg("path", "Root folder", "p")]
    public string Path { get; set; } = ".";
}

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            FCli cli = new FCli()
               .WithDescription("Theme Generator CLI Tool")
               .WithErrorHandler(ex => Console.WriteLine(ex.Message))
               .Command<ThemeArgs>("theme")
                   .WithAlias("t")
                   .WithDescription("Generate Theme CSS")
                   .OnExecute(async args =>
                       await ThemeCssGenerator.BuildThemeCssFile(
                           rootPath: args.Path,
                           outputFolder: args.Output,
                           outputFile: args.File))
               .Command<VariablesArgs>("variables")
                   .WithAlias("v")
                   .WithDescription("Generate Variables CSS")
                   .OnExecute(async args =>
                       await ThemeCssGenerator.BuildVariablesCssFile(
                           rootPath: args.Path,
                           outputFolder: args.Output,
                           outputFile: args.File))
               .Command("icons")
                   .WithAlias("i")
                   .WithDescription("Icons class generation")
                   .OnExecute(async _ =>
                       await IconsClassGenerator.BuildIconsFile())
               .Command<ResourcesArgs>("resources")
                   .WithAlias("r")
                   .WithDescription("Generate resource files")
                   .OnExecute(async (args) => await ResourceFilesGenerator.GenerateResourceFiles(args.Path));

            await cli.ExecuteAsync(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}