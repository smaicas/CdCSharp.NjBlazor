using System.Text.RegularExpressions;

using System.Xml.Linq;

/// <summary>
/// Generates resource files from Razor components that use IStringLocalizer
/// </summary>
public static class ResourceFilesGenerator
{
    private const string ResourcesDirectory = "Resources";
    private const string DefaultLanguage = "en";
    private const string TargetLanguage = "es";

    private static readonly Dictionary<string, HashSet<string>> _resourceStrings = [];

    public static async Task GenerateResourceFiles(string projectPath)
    {
        try
        {
            if (string.IsNullOrEmpty(projectPath))
                throw new ArgumentException("Project path cannot be null or empty", nameof(projectPath));

            if (!Directory.Exists(projectPath))
                throw new DirectoryNotFoundException($"Project directory not found: {projectPath}");

            Console.WriteLine($"Starting resource file generation from project: {projectPath}");

            // Find all .razor files
            IEnumerable<string> razorFiles;
            try
            {
                razorFiles = Directory.EnumerateFiles(projectPath, "*.razor", SearchOption.AllDirectories);
                int totalFiles = razorFiles.Count();
                Console.WriteLine($"Found {totalFiles} Razor files to process");
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException($"Access denied while searching for Razor files in {projectPath}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error while searching for Razor files in {projectPath}", ex);
            }

            int processedFiles = 0;
            int filesWithLocalizer = 0;

            Dictionary<string, string> pathsByClassName = [];

            foreach (string razorFile in razorFiles)
            {
                try
                {
                    processedFiles++;
                    Console.WriteLine($"Processing file ({processedFiles}/{razorFiles.Count()}): {Path.GetFileName(razorFile)}");
                    bool hasLocalizer = ProcessRazorFile(razorFile);
                    if (hasLocalizer)
                    {
                        string relativeDirectoryName = Path.GetDirectoryName(razorFile)?
                            .Replace(projectPath, "") ?? string.Empty;
                        if (relativeDirectoryName.StartsWith("\\"))
                        {
                            relativeDirectoryName = relativeDirectoryName.Substring(1);
                        }

                        pathsByClassName.TryAdd(Path.GetFileNameWithoutExtension(razorFile), relativeDirectoryName);
                        filesWithLocalizer++;
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {razorFile}: {ex.Message}");
                    Console.WriteLine("Continuing with next file...");
                }
            }

            Console.WriteLine($"\nFound {filesWithLocalizer} files using IStringLocalizer");
            Console.WriteLine($"Discovered {_resourceStrings.Count} unique resource classes:");
            foreach (KeyValuePair<string, HashSet<string>> resourceClass in _resourceStrings)
            {
                Console.WriteLine($"- {resourceClass.Key}: {resourceClass.Value.Count} strings");
            }

            // Generate resource files
            Console.WriteLine($"\nGenerating resource files in {ResourcesDirectory} directory");
            string resourcePath = Path.Combine(projectPath, ResourcesDirectory);
            try
            {
                Directory.CreateDirectory(resourcePath);
                Console.WriteLine($"Created directory: {resourcePath}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create resources directory at {resourcePath}", ex);
            }

            foreach (KeyValuePair<string, HashSet<string>> resourceGroup in _resourceStrings)
            {
                try
                {
                    string filePath = Path.Combine(resourcePath, $"{pathsByClassName[resourceGroup.Key]}{Path.DirectorySeparatorChar}{resourceGroup.Key}.{TargetLanguage}.resx");

                    Console.WriteLine($"\nGenerating resource file: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"Adding {resourceGroup.Value.Count} string resources");
                    GenerateResxFile(filePath, Path.Combine(resourcePath, $"{pathsByClassName[resourceGroup.Key]}"), resourceGroup.Value);
                    Console.WriteLine($"Successfully generated: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating resource file for {resourceGroup.Key}: {ex.Message}");
                    Console.WriteLine("Continuing with next resource group...");
                }
            }

            Console.WriteLine("\nResource file generation completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Critical error during resource generation: {ex.Message}");
            throw;
        }
    }

    private static bool ProcessRazorFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Razor file not found: {filePath}");
        }

        string content;
        try
        {
            content = File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            throw new IOException($"Error reading file {filePath}", ex);
        }

        Match localizerMatch;
        try
        {
            // Extract IStringLocalizer injection
            localizerMatch = Regex.Match(content, @"@inject\s+IStringLocalizer<(\w+)>\s+(\w+)");
        }
        catch (RegexMatchTimeoutException ex)
        {
            throw new InvalidOperationException($"Regex timeout while processing {filePath}", ex);
        }

        if (!localizerMatch.Success)
        {
            Console.WriteLine($"  - No IStringLocalizer found in {Path.GetFileName(filePath)}");
            return false;
        }

        string resourceClass = localizerMatch.Groups[1].Value;
        string localizerVar = localizerMatch.Groups[2].Value;

        if (string.IsNullOrEmpty(resourceClass) || string.IsNullOrEmpty(localizerVar))
        {
            throw new InvalidOperationException($"Invalid IStringLocalizer pattern found in {filePath}");
        }

        Console.WriteLine($"  - Found IStringLocalizer<{resourceClass}> as {localizerVar}");

        int stringsFound = 0;

        // Find all localizer usage patterns
        string[] localizerUsages = new[]
        {
            $@"{localizerVar}\[""([^""]+)""\]",
            $@"{localizerVar}\[@([^]]+)\]",
            $@"L\[""([^""]+)""\]"
        };

        foreach (string pattern in localizerUsages)
        {
            try
            {
                MatchCollection matches = Regex.Matches(content, pattern);
                foreach (Match match in matches.Cast<Match>())
                {
                    string key = match.Groups[1].Value;
                    if (string.IsNullOrEmpty(key))
                    {
                        Console.WriteLine($"Warning: Empty key found in {Path.GetFileName(filePath)}");
                        continue;
                    }

                    if (!_resourceStrings.ContainsKey(resourceClass))
                    {
                        _resourceStrings[resourceClass] = [];
                        Console.WriteLine($"  - Created new resource group for {resourceClass}");
                    }

                    if (_resourceStrings[resourceClass].Add(key))
                    {
                        stringsFound++;
                        Console.WriteLine($"    + Added new string: {key}");
                    }
                }
            }
            catch (RegexMatchTimeoutException ex)
            {
                throw new InvalidOperationException($"Regex timeout while processing pattern '{pattern}' in {filePath}", ex);
            }
        }

        Console.WriteLine($"  - Found {stringsFound} new localizable strings");
        return true;
    }

    private static void GenerateResxFile(string filePath, string relativePath, HashSet<string> strings)
    {
        if (strings == null || !strings.Any())
        {
            throw new ArgumentException("No strings provided for resource file generation", nameof(strings));
        }

        Console.WriteLine($"  Creating RESX structure with {strings.Count} entries");

        // Definición de espacios de nombres
        XNamespace ns_xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
        XNamespace ns_msdata = XNamespace.Get("urn:schemas-microsoft-com:xml-msdata");

        XDocument doc;
        try
        {
            if (File.Exists(filePath))
            {
                doc = XDocument.Load(filePath);
            }
            else
            {
                // Si no hay archivo existente, crear uno nuevo
                doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    new XElement("root",
                        new XElement("schema",
                            new XAttribute("id", "root"),
                            new XAttribute(XNamespace.Xmlns + "xsd", ns_xsd),
                            new XAttribute(XNamespace.Xmlns + "msdata", ns_msdata),
                            new XAttribute(ns_msdata + "IsDataSet", "true")
                        ),
                        new XElement("resheader",
                            new XElement("value", "text/microsoft-resx"),
                            new XAttribute("name", "resmimetype")
                        ),
                        new XElement("resheader",
                            new XElement("value", "2.0"),
                            new XAttribute("name", "version")
                        ),
                        new XElement("resheader",
                            new XElement("value",
                                "System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                            new XAttribute("name", "reader")
                        ),
                        new XElement("resheader",
                            new XElement("value",
                                "System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                            new XAttribute("name", "writer")
                        )
                    )
                );
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error creating or loading RESX XML structure", ex);
        }

        Dictionary<string, string> processedKeys = doc.Descendants("data")
            .ToDictionary(d => (string)d.Attribute("name"), d => (string)d.Element("value"));

        int processedStrings = 0;

        foreach (string key in strings)
        {
            try
            {
                string sanitizedKey = key;
                string originalValue = key;

                // Si la clave sanitizada ya existe, añadir un sufijo numérico
                string uniqueKey = sanitizedKey;

                if (processedKeys.ContainsKey(uniqueKey)) { continue; }

                processedKeys[uniqueKey] = originalValue;

                doc.Root!.Add(
                    new XElement("data",
                        new XAttribute("name", uniqueKey),
                        new XElement("value", $"[{TargetLanguage}] {originalValue}")
                    )
                );

                if (sanitizedKey != key)
                {
                    Console.WriteLine($"    Warning: Key '{key}' was sanitized to '{uniqueKey}' for XML compatibility");
                }

                processedStrings++;
                if (processedStrings % 50 == 0)
                {
                    Console.WriteLine($"    Processed {processedStrings}/{strings.Count} strings");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to add key '{key}' to resource file: {ex.Message}");
            }
        }

        try
        {
            if (!Directory.Exists(relativePath)) { Directory.CreateDirectory(relativePath); }

            doc.Save(filePath);
            Console.WriteLine($"    Completed processing all {strings.Count} strings");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error saving resource file to {filePath}", ex);
        }
    }
}