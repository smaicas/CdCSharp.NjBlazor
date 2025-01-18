using CdCSharp.NjBlazor.Tools.ThemeGenerator;
using CdCSharp.NjBlazor.Tools.ThemeGenerator.Cli;

try
{
    if (args.Length == 0) throw new ArgumentException("Required arguments");

    CommandLinePipe commandPipe = new(args);

    Dictionary<string, Func<CommandLinePipe, Task>> parameterProcess = new()
    {
        { "--t", ThemeCommand},
        { "--theme", ThemeCommand},
        { "--v", VariablesCommand},
        { "--variables", VariablesCommand},
        { "--i", IconsCommand},
        { "--icons", IconsCommand},
        { "--r", ResourcesCommand},
        { "--resources", ResourcesCommand},
        { "-h", Help.ShowHelp},
        { "--help", Help.ShowHelp},
    };

    await commandPipe.ExecuteFirstAsync(parameterProcess, "Required command arguments. Use theme-generator --help to see help.");
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}

async Task ThemeCommand(CommandLinePipe commandPipe)
{
    Dictionary<string, Func<CommandLinePipe, Task>> commandParameters = new()
    {
        {"", Commands.GenerateTheme },
        { "-h", Help.ShowThemeHelp},
        { "--help", Help.ShowThemeHelp},
    };

    await commandPipe.ExecuteFirstAsync(commandParameters);
}

async Task VariablesCommand(CommandLinePipe commandPipe)
{
    Dictionary<string, Func<CommandLinePipe, Task>> commandParameters = new()
    {
        {"", Commands.GenerateVariables },
        { "-h", Help.ShowVariablesHelp},
        { "--help", Help.ShowVariablesHelp},
    };

    await commandPipe.ExecuteFirstAsync(commandParameters);
}

async Task IconsCommand(CommandLinePipe commandPipe)
{
    Dictionary<string, Func<CommandLinePipe, Task>> commandParameters = new()
    {
        {"", Commands.GenerateIcons },
        { "-h", Help.ShowIconsHelp},
        { "--help", Help.ShowIconsHelp},
    };

    await commandPipe.ExecuteFirstAsync(commandParameters);
}
async Task ResourcesCommand(CommandLinePipe commandPipe)
{
    Dictionary<string, Func<CommandLinePipe, Task>> commandParameters = new()
    {
        {"", Commands.GenerateResourceFiles },
    };

    await commandPipe.ExecuteFirstAsync(commandParameters);
}