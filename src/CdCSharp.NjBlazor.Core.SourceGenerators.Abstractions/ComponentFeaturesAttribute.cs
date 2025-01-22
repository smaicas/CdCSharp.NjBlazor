
using System;

namespace CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]

public class ComponentFeaturesAttribute : Attribute
{
    public ComponentFeaturesAttribute(params Type[] features) => Features = features;

    public Type[] Features { get; }
}