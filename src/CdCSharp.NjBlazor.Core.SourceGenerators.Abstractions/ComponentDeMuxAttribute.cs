using System;

namespace CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ComponentDeMuxAttribute<TVariantsEnum> : Attribute where TVariantsEnum : Enum
{
}