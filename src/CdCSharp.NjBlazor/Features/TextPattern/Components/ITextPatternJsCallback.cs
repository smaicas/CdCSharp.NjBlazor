namespace CdCSharp.NjBlazor.Features.TextPattern.Components;

public interface ITextPatternJsCallback
{
    Task NotifyTextChanged(string text);

    Task<bool> ValidatePartial(int index, string text);
}