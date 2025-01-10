namespace CdCSharp.NjBlazor.Tools.ThemeGenerator.Cli;

/// <summary>
/// Represents a command line pipe that parses command line arguments.
/// </summary>
public class CommandLinePipe
{
    private readonly Dictionary<string, string?> _arguments;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandLinePipe" /> class with the specified arguments.
    /// </summary>
    /// <param name="args">
    /// The command line arguments to parse.
    /// </param>
    public CommandLinePipe(string[] args) => _arguments = ParseArgs(args);

    /// <summary>
    /// Executes the first action asynchronously based on the specified parameters.
    /// </summary>
    /// <param name="parameterProcess">
    /// A dictionary of parameter names and corresponding actions to execute.
    /// </param>
    /// <param name="argumentExceptionMessage">
    /// The message to include in the exception if no action can be executed.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    public async Task ExecuteFirstAsync(Dictionary<string, Func<CommandLinePipe, Task>> parameterProcess, string argumentExceptionMessage = "")
    {
        parameterProcess.TryGetValue(string.Empty, out Func<CommandLinePipe, Task>? noArgsProcess);

        foreach (KeyValuePair<string, Func<CommandLinePipe, Task>> item in parameterProcess)
        {
            if (string.IsNullOrEmpty(item.Key)) continue;

            if (HasArgument(item.Key))
            {
                await item.Value.Invoke(this);
                return;
            }
        }
        if (noArgsProcess != null)
            await noArgsProcess.Invoke(this);
        else
            throw new ArgumentException(argumentExceptionMessage);
    }

    /// <summary>
    /// Gets the value of the specified argument, or null if the argument is not found.
    /// </summary>
    /// <param name="key">
    /// The key of the argument.
    /// </param>
    /// <returns>
    /// The value of the argument, or null if not found.
    /// </returns>
    public string? GetArgumentOrDefault(string key) => _arguments.ContainsKey(key) ? _arguments[key] : null;

    /// <summary>
    /// Gets the value of the specified argument. When the argument is not required but requires a
    /// value when it is specified.
    /// </summary>
    /// <param name="keys">
    /// The keys of the argument.
    /// </param>
    /// <returns>
    /// The value of the argument, or null if argument is not specified.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the argument has not value.
    /// </exception>

    public string? GetArgumentWithRequiredValueOrDefault(params string[] keys)
    {
        foreach (string key in _arguments.Keys)
            if (keys.Contains(key)) return _arguments[key] ?? throw new ArgumentException($"Required value for {key} argument");

        return null;
    }

    /// <summary>
    /// Gets the value of the specified required argument. When argument is required and value is required
    /// </summary>
    /// <param name="key">
    /// The key of the argument.
    /// </param>
    /// <returns>
    /// The value of the required argument.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the required argument is not found.
    /// </exception>
    public string? GetRequiredArgument(string key) => _arguments.ContainsKey(key) ? _arguments[key] : throw new ArgumentException($"Required argument {key}");

    /// <summary>
    /// Gets the value of the specified required argument with a required value. When the argument
    /// is required and requires a value too.
    /// </summary>
    /// <param name="keys">
    /// The keys of the arguments.
    /// </param>
    /// <returns>
    /// The value of the required argument.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the required argument is not found.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the required value is not found.
    /// </exception>
    public string GetRequiredArgumentWithRequiredValue(params string[] keys)
    {
        foreach (string key in _arguments.Keys)
            if (keys.Contains(key)) return _arguments[key] ?? throw new ArgumentException($"Required value for {key} argument");

        throw new ArgumentException($"Required argument {string.Join(",", keys)}");
    }

    /// <summary>
    /// Determines whether the pipe has any of the specified arguments.
    /// </summary>
    /// <param name="keys">
    /// The keys of the arguments.
    /// </param>
    /// <returns>
    /// True if the pipe has any of the specified arguments; otherwise, false.
    /// </returns>
    public bool HasAnyArgument(params string[] keys) => _arguments.Keys.Any(k => keys.Contains(k));

    /// <summary>
    /// Determines whether the pipe has the specified argument.
    /// </summary>
    /// <param name="key">
    /// The key of the argument.
    /// </param>
    /// <returns>
    /// True if the pipe has the specified argument; otherwise, false.
    /// </returns>
    public bool HasArgument(string key) => _arguments.ContainsKey(key);

    private Dictionary<string, string?> ParseArgs(string[] args)
    {
        Dictionary<string, string?> parsedArgs = [];

        for (int i = 0; i < args.Length; i++)
            if (args[i].StartsWith("-"))
            {
                string key = args[i];
                string? value = i + 1 < args.Length && !args[i + 1].StartsWith("-") ? args[i + 1] : null;
                parsedArgs[key] = value;
            }

        return parsedArgs;
    }
}