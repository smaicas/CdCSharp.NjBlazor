using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using System.Runtime.CompilerServices;

namespace CdCSharp.NjBlazor.Core.SourceGenerators.SnapshotTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        // Force Assembly initialization (!! important !!)
        Type a = typeof(AutogenerateCssColorsAttribute);
        VerifySourceGenerators.Initialize();
    }
}