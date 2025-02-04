using CdCSharp.SequentialGenerator;
using Microsoft.CodeAnalysis;

namespace CdCSharp.NjBlazor.Core.SourceGenerators;

[Generator]
public class NjBlazorGeneratorsSequence : SequentialGeneratorBase, IIncrementalGenerator
{
    public NjBlazorGeneratorsSequence()
    {
        RegisterGenerator(new ComponentFeaturesGenerator());
        RegisterGenerator(new ComponentDeMuxGenerator());
    }
}
