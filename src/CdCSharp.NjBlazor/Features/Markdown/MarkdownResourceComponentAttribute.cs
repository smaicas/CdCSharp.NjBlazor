namespace CdCSharp.NjBlazor.Features.Markdown;

/// <summary>
/// Specifies that a class is a Markdown resource component.
/// </summary>
/// <remarks>
/// This attribute can be applied to classes to indicate that they represent Markdown resource components.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MarkdownResourceComponentAttribute : Attribute
{
    /// <summary>Gets or sets the name of the resource.</summary>
    /// <value>The name of the resource.</value>
    public string ResourceName { get; set; }

    /// <summary>Gets or sets the name of the assembly.</summary>
    public string? AssemblyName { get; set; }

    /// <summary>
    /// Initializes a new instance of the MarkdownResourceComponentAttribute class.
    /// </summary>
    /// <param name="resourceName">The name of the resource.</param>
    /// <param name="assemblyName">The name of the assembly containing the resource (optional).</param>
    public MarkdownResourceComponentAttribute(string resourceName, string? assemblyName = null)
    {
        ResourceName = resourceName;
        AssemblyName = assemblyName;
    }
}