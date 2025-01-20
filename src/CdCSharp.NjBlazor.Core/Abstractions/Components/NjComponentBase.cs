using CdCSharp.NjBlazor.Core.Strings;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace CdCSharp.NjBlazor.Core.Abstractions.Components;

/// <summary>
/// Base class for Nj components that provides common functionality.
/// </summary>
public abstract class NjComponentBase : ComponentBase
{
    private readonly string[] IgnoredAdditionalAttributes = ["class"];
    /// <summary>
    /// Gets or sets a collection of additional attributes that will be applied to the created element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = [];

    public IReadOnlyDictionary<string, object>? FilteredAdditionalAttributes => AdditionalAttributes?.Where(aa => !IgnoredAdditionalAttributes.Contains(aa.Key.ToLowerInvariant())).ToDictionary();

    /// <summary>
    /// Represents a reference to the main element.
    /// </summary>
    /// <remarks>
    /// This reference may not be null.
    /// </remarks>
    [DisallowNull]
    protected ElementReference? MainElementReference;

    /// <summary>
    /// Gets or sets the reference ID of the main element.
    /// </summary>
    public string? MainElementReferenceId;

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            Dictionary<string, object> additionals = GetAdditionalAttributes();
            bool hasChanged = false;
            if (additionals.Count > 0)
            {
                // Priorize existing AdditionalAttributes
                AdditionalAttributes = AdditionalAttributes.Concat(additionals)
                    .GroupBy(pair => pair.Key)
                    .ToDictionary(group => group.Key, group => group.First().Value);
                hasChanged = true;
            };

            if (MainElementReference != null)
            {
                MainElementReferenceId = ((ElementReference)MainElementReference).Id;
                hasChanged = true;
            }
            if (hasChanged) { StateHasChanged(); }

        }

    }

    private Dictionary<string, object> GetAdditionalAttributes()
    {
        AdditionalAttributes ??= [];

        Dictionary<string, string> styles = GetInlineStyles();

        if (styles.Count > 0)
        {
            if (AdditionalAttributes.ContainsKey("style"))
            {
                string stylesValue = (string)AdditionalAttributes["style"];

                foreach (KeyValuePair<string, string> style in styles)
                {
                    if (stylesValue.Contains($"{style.Key}:")) { continue; }
                    stylesValue.Concat($"{style.Key}:{style.Value}");
                }
                AdditionalAttributes["style"] = stylesValue;
            }
            else
            {
                AdditionalAttributes.Add("style", styles.Select(s => $"{s.Key}:{s.Value}"));
            }
        }
        return [];
    }

    public virtual Dictionary<string, string> GetInlineStyles() => [];

    /// <summary>
    /// Gets the css class from the additional attributes.
    /// </summary>
    public string Class
    {
        get
        {
            if (AdditionalAttributes == null) return string.Empty;
            AdditionalAttributes.TryGetValue("class", out object? className);
            if (className == null) return string.Empty;
            return (string)className;
        }
    }

    /// <summary>
    /// Concatenates an array of strings into a single string, separated by a space. Ignores empty strings.
    /// </summary>
    /// <param name="classes">
    /// An array of strings representing classes.
    /// </param>
    /// <returns>
    /// A single string containing the concatenated classes separated by a space.
    /// </returns>
    protected string AsClass(params string[] classes) => classes.NotEmptyJoin();

    /// <summary>
    /// Creates a debounced action for an event handler.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the event argument.
    /// </typeparam>
    /// <param name="action">
    /// The action to be debounced.
    /// </param>
    /// <param name="interval">
    /// The time interval for debouncing.
    /// </param>
    /// <returns>
    /// A debounced action for the event handler.
    /// </returns>
    protected virtual Action<T> DebounceEvent<T>(Action<T> action, TimeSpan interval)
    {
        return NjEvents.Debounce<T>(arg => InvokeAsync(() =>
            {
                action(arg);
                StateHasChanged();
            }), interval);
    }

    /// <summary>
    /// Throttles an event action to limit the rate at which it is invoked.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the event argument.
    /// </typeparam>
    /// <param name="action">
    /// The action to be throttled.
    /// </param>
    /// <param name="interval">
    /// The time interval to throttle the action.
    /// </param>
    /// <returns>
    /// A throttled action that will be invoked at a limited rate.
    /// </returns>
    protected virtual Action<T> ThrottleEvent<T>(Action<T> action, TimeSpan interval)
    {
        return NjEvents.Throttle<T>(arg => InvokeAsync(() =>
            {
                action(arg);
                StateHasChanged();
            }), interval);
    }
}