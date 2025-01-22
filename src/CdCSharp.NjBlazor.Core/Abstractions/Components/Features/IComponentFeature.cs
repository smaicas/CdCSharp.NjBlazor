namespace CdCSharp.NjBlazor.Core.Abstractions.Components.Features;

public interface IComponentFeature<T> where T : new()
{
    public T Feature { get; }

}