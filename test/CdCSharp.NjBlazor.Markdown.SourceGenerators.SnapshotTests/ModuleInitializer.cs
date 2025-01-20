using CdCSharp.NjBlazor.Markdown.SourceGenerators.Abstractions;
using System.Runtime.CompilerServices;

namespace CdCSharp.NjBlazor.Markdown.SourceGenerators.SnapshotTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        // Force Assembly initialization (!! important !!)
        Type a = typeof(MarkdownResourcesAllAttribute);
        Type b = typeof(MarkdownToBlazorAllGenerator);
        VerifySourceGenerators.Initialize();
    }
}