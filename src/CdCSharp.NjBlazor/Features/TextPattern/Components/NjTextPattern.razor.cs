using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Strings;
using CdCSharp.NjBlazor.Features.DeviceManager.Components;
using CdCSharp.NjBlazor.Features.TextPattern.Abstractions;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace CdCSharp.NjBlazor.Features.TextPattern.Components;

/// <summary>
/// Represents a text pattern for NjComponentBase.
/// </summary>
public partial class NjTextPattern : NjComponentBase, ITextPatternJsCallback
{
    [DisallowNull]
    private ElementReference _containerBox;

    private TextPatternCallbacksRelay? _jsCallbacksRelay;
    private string? _pattern;
    private string? _prevFormat;
    private string? _prevText;
    private string _text = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether date formats are allowed.
    /// </summary>
    /// <value>
    /// True if date formats are allowed; otherwise, false.
    /// </value>
    [Parameter]
    public bool AllowDateFormats { get; set; } = false;

    /// <summary>
    /// Gets or sets the default text value.
    /// </summary>
    /// <value>
    /// The default text value.
    /// </value>
    [Parameter]
    [EditorRequired]
    public string DefaultText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the content is editable.
    /// </summary>
    /// <value>
    /// True if the content is editable; otherwise, false.
    /// </value>
    [Parameter]
    public bool Editable { get; set; }

    /// <summary>
    /// Gets or sets the format of the data.
    /// </summary>
    /// <value>
    /// The format of the data.
    /// </value>
    [Parameter]
    [EditorRequired]
    public string Format { get; set; } = string.Empty;

    /// <summary>
    /// Additional validation function before set the value when entered manually.
    /// </summary>
    [Parameter]
    public Func<string, bool>? IsValidFunction { get; set; }

    /// <summary>
    /// From 0 to N determine partial validations for the pattern.
    /// Example: For pattern 2025-1-23 0 = 2025 1 = - 2 = 1 3 = - 4 = 23
    /// </summary>
    [Parameter]
    public Dictionary<int, Func<string, bool>>? PartialValidations { get; set; }

    /// <summary>
    /// Gets or sets the text value.
    /// </summary>
    /// <value>
    /// The text value.
    /// </value>
    [Parameter]
    [EditorRequired]
    public string Text
    {
        get
        {
            if (string.IsNullOrEmpty(_text)) return DefaultText;
            return _text;
        }
        set
        {
            if (_text == value) return;
            _text = value;
            InvokeAsync(RefreshPattern);
            TextChanged.InvokeAsync(_text);
        }
    }

    /// <summary>
    /// Gets or sets the event callback for text changes.
    /// </summary>
    /// <value>
    /// The event callback for text changes.
    /// </value>
    [Parameter]
    public EventCallback<string> TextChanged { get; set; }

    private string Pattern
    {
        get
        {
            if (_pattern == null)
            {
                _pattern = Format.ExtractRegex();
            }
            return _pattern;
        }
    }

    /// <summary>
    /// Gets or sets the TextPatternJsInterop for interacting with text patterns.
    /// </summary>
    /// <value>
    /// The TextPatternJsInterop instance.
    /// </value>
    [Inject]
    private ITextPatternJsInterop TextPatternJs { get; set; } = default!;

    public Task NotifyTextChanged(string text)
    {
        // Valid pattern and valid function, sets de value
        if (Regex.Match(text, Pattern).Success && (IsValidFunction == null || IsValidFunction(text)))
        {
            Text = text;
        }
        return Task.CompletedTask;
    }

    public Task<bool> ValidatePartial(int index, string text)
    {
        if (PartialValidations == null) return Task.FromResult(true);

        if (PartialValidations.TryGetValue(index, out Func<string, bool>? partialValidationFunction))
        {
            return Task.FromResult(partialValidationFunction(text));
        }
        return Task.FromResult(true);
    }

    /// <summary>
    /// Asynchronously triggers a refresh of the pattern after rendering.
    /// </summary>
    /// <param name="firstRender">
    /// A boolean value indicating if this is the first render.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsCallbacksRelay = new TextPatternCallbacksRelay(this);
            await RefreshPattern();
        }
    }

    /// <summary>
    /// Determines whether the control should be rendered based on changes in text or format.
    /// </summary>
    /// <returns>
    /// True if the control should be rendered; otherwise, false.
    /// </returns>
    protected override bool ShouldRender()
    {
        bool should = _prevText != Text || _prevFormat != Format;
        _prevText = Text;
        _prevFormat = Format;
        return should;
    }

    private List<ElementPattern> PreparePatterns()
    {
        List<ElementPattern> result = [];
        if (Pattern == null) return result;

        string[] patternParts = Pattern.Split(")").Select(p => $"{p})").ToArray();
        Match match = Regex.Match(Text, AllowDateFormats ? Pattern.Replace("d", "w") : Pattern);
        bool success = match.Success;

        if (success)
        {
            int currentCharIndex = 0;
            for (int i = 1; i < match.Groups.Count; i++)
            {
                string value = match.Groups[i].Value;
                string currentDefaultValue = DefaultText.Substring(currentCharIndex, value.Length);
                currentCharIndex += value.Length;
                bool isSeparator = true;
                bool isEditable = false;
                if (Regex.Match(value, @"^[a-zA-Z0-9]+$").Success)
                {
                    isSeparator = false;
                    if (Editable)
                    {
                        isEditable = true;
                    }
                }
                result.Add(new(patternParts[i - 1], value, value.Length, currentDefaultValue, isSeparator, isEditable));
            }
        }
        return result;
    }

    private async Task RefreshPattern()
    {
        List<ElementPattern> patterns = PreparePatterns();
        await TextPatternJs.TextPatternAddDynamic(
            _containerBox,
            patterns,
            _jsCallbacksRelay?.DotNetReference,
            nameof(NotifyTextChanged),
            nameof(ValidatePartial));
    }
}