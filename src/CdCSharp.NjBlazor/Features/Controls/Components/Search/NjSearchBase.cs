using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Search;

/// <summary>
/// Base class for search functionality.
/// </summary>
/// <typeparam name="TSearchObject">
/// The type of object to search for.
/// </typeparam>
public abstract class NjSearchBase<TSearchObject> : NjControlComponentBase
{
    /// <summary>
    /// Reference to an element.
    /// </summary>
    /// <remarks>
    /// This reference may be null.
    /// </remarks>
    [DisallowNull]
    protected ElementReference? _inputReference;

    /// <summary>
    /// Action to debounce search dynamic changes.
    /// </summary>
    /// <remarks>
    /// This action is used to debounce changes in a search dynamic event.
    /// </remarks>
    protected Action<ChangeEventArgs>? DebounceSearchDynamic;

    /// <summary>
    /// A list of filtered search objects.
    /// </summary>
    protected List<TSearchObject> FilteredSource = [];

    /// <summary>
    /// Indicates whether the component is currently focused.
    /// </summary>
    protected bool IsFocused;

    /// <summary>
    /// Coordinates of the options box.
    /// </summary>
    /// <value>
    /// A tuple representing the top, right, bottom, and left coordinates of the options box.
    /// </value>
    protected (float Top, float Right, float Bottom, float Left) OptionsBoxCoords = (
        0f,
        0f,
        0f,
        0f
    );

    /// <summary>
    /// The string used for searching.
    /// </summary>
    protected string SearchString = "";

    private readonly int ThrottleMilliseconds = 300;

    /// <summary>
    /// Gets or sets the function used to display an object as a string.
    /// </summary>
    /// <value>
    /// The function that converts an object of type TSearchObject to a string.
    /// </value>
    /// <remarks>
    /// If not explicitly set, the default behavior is to call the ToString method on the object, or
    /// return an empty string if the object is null.
    /// </remarks>
    [Parameter]
    [EditorRequired]
    public Func<TSearchObject, string> DisplayFunction { get; set; } =
        (o) => o?.ToString() ?? string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether dynamic search is enabled.
    /// </summary>
    /// <value>
    /// True if dynamic search is enabled; otherwise, false.
    /// </value>
    [Parameter]
    public bool DynamicSearch { get; set; }

    /// <summary>
    /// Gets or sets the minimum number of characters for dynamic search.
    /// </summary>
    /// <value>
    /// The minimum number of characters for dynamic search.
    /// </value>
    [Parameter]
    public int MinCharacters { get; set; } = 2;

    /// <summary>
    /// Gets or sets the event callback for when an item is selected.
    /// </summary>
    /// <typeparam name="TSearchObject">
    /// The type of the selected item.
    /// </typeparam>
    [Parameter]
    public EventCallback<TSearchObject> OnSelectedItem { get; set; }

    /// <summary>
    /// Gets or sets the position of the search options.
    /// </summary>
    /// <value>
    /// The position of the search options.
    /// </value>
    [Parameter]
    public NjSearchOptionsPosition OptionsPosition { get; set; } = NjSearchOptionsPosition.Bottom;

    /// <summary>
    /// Gets or sets the placeholder text.
    /// </summary>
    /// <value>
    /// The placeholder text.
    /// </value>
    [Parameter]
    public string Placeholder { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the post-adornment string.
    /// </summary>
    /// <value>
    /// The post-adornment string.
    /// </value>
    [Parameter]
    public string? PostAdornment { get; set; }

    /// <summary>
    /// Gets or sets the color of the post adornment.
    /// </summary>
    /// <value>
    /// The color of the post adornment.
    /// </value>
    [Parameter]
    public CssColor? PostAdornmentColor { get; set; }

    /// <summary>
    /// Gets or sets the string to be displayed before the main content.
    /// </summary>
    /// <value>
    /// The string to be displayed before the main content.
    /// </value>
    [Parameter]
    public string? PreAdornment { get; set; }

    /// <summary>
    /// Gets or sets the color of the pre-adornment.
    /// </summary>
    /// <value>
    /// A nullable CssColor representing the color of the pre-adornment.
    /// </value>
    [Parameter]
    public CssColor? PreAdornmentColor { get; set; }

    /// <summary>
    /// Gets or sets the search function that determines if a search object matches a given string.
    /// </summary>
    /// <value>
    /// The search function that takes a search object and a string to perform the search.
    /// </value>
    [Parameter]
    [EditorRequired]
    public Func<TSearchObject, string, bool> SearchFuntion { get; set; } = default!;

    /// <summary>
    /// Gets or sets the sorting function used to sort the search object.
    /// </summary>
    /// <value>
    /// A function that defines the sorting logic for the search object.
    /// </value>
    [Parameter]
    public Func<TSearchObject, object>? SortingFuntion { get; set; }

    /// <summary>
    /// Gets or sets the source data for the search.
    /// </summary>
    /// <value>
    /// An enumerable collection of objects of type TSearchObject.
    /// </value>
    /// <remarks>
    /// This property is marked as a required editor parameter.
    /// </remarks>
    [Parameter]
    [EditorRequired]
    public IEnumerable<TSearchObject> SourceData { get; set; } = default!;

    /// <summary>
    /// Gets or sets the DOM JavaScript interop service for interacting with the Document Object
    /// Model (DOM) in JavaScript.
    /// </summary>
    /// <remarks>
    /// The DOMJsInterop service provides methods for performing JavaScript interop operations on
    /// the DOM.
    /// </remarks>
    [Inject]
    protected IDOMJsInterop DomJs { get; set; } = default!;

    /// <summary>
    /// Determines the CSS class based on the focus state.
    /// </summary>
    /// <value>
    /// The CSS class to apply when the element is focused.
    /// </value>
    protected string FocusClass => IsFocused ? CssClassReferences.Focus : string.Empty;

    /// <summary>
    /// Gets the CSS style for the options box based on its coordinates.
    /// </summary>
    /// <value>
    /// The CSS style for the options box.
    /// </value>
    protected string OptionsBoxStyle
    {
        get
        {
            StringBuilder sb = new();

            if (OptionsBoxCoords.Top > 0f)
                sb.Append($"top: {OptionsBoxCoords.Top.ToString(CultureInfo.InvariantCulture)}px;");
            if (OptionsBoxCoords.Right > 0f)
                sb.Append(
                    $"right: {OptionsBoxCoords.Right.ToString(CultureInfo.InvariantCulture)}px;"
                );
            if (OptionsBoxCoords.Bottom > 0f)
                sb.Append(
                    $"bottom: {OptionsBoxCoords.Bottom.ToString(CultureInfo.InvariantCulture)}px;"
                );
            if (OptionsBoxCoords.Left > 0f)
                sb.Append(
                    $"left: {OptionsBoxCoords.Left.ToString(CultureInfo.InvariantCulture)}px;"
                );

            return sb.ToString();
        }
    }

    /// <summary>
    /// Asynchronously retrieves the coordinates of the options box.
    /// </summary>
    /// <returns>
    /// A tuple containing the coordinates (left, top, right, bottom) of the options box. If the
    /// input reference is null, returns (0f, 0f, 0f, 0f).
    /// </returns>
    protected async Task<(float, float, float, float)> GetOptionsBoxCoordsAsync()
    {
        if (_inputReference == null)
            return (0f, 0f, 0f, 0f);
        return await DomJs.GetCoordsRelativeAsync(
            _inputReference.Value,
            OptionsPosition.ToString().ToLower()
        );
    }

    /// <summary>
    /// Method called after rendering to set the coordinates of the options box asynchronously.
    /// </summary>
    /// <param name="firstRender">
    /// A boolean value indicating if it is the first render.
    /// </param>
    /// <returns>
    /// An asynchronous task.
    /// </returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            OptionsBoxCoords = await GetOptionsBoxCoordsAsync();
    }

    /// <summary>
    /// Updates the focus state to true asynchronously.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected Task OnFocusAsync()
    {
        IsFocused = true;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Clears the search string and sets the focus state to false.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    protected Task OnFocusOutAsync()
    {
        SearchString = string.Empty;
        IsFocused = false;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Initializes the component and sets up a debounced event handler for dynamic search.
    /// </summary>
    /// <remarks>
    /// This method initializes the component by setting up a debounced event handler for dynamic
    /// search. The debounced event handler will wait for a specified time period before triggering
    /// the search operation.
    /// </remarks>
    /// <seealso cref="DebounceEvent{T}(Func{T, Task}, TimeSpan)" />
    /// <seealso cref="SearchDynamicAsync(ChangeEventArgs)" />
    /// <seealso cref="ThrottleMilliseconds" />
    protected override void OnInitialized()
    {
        DebounceSearchDynamic = DebounceEvent<ChangeEventArgs>(
            async e => await SearchDynamicAsync(e),
            TimeSpan.FromMilliseconds(ThrottleMilliseconds)
        );
        base.OnInitialized();
    }

    /// <summary>
    /// Searches the SourceData based on the SearchString and updates the FilteredSource.
    /// </summary>
    /// <remarks>
    /// If the SearchString length is less than MinCharacters, the method returns without performing
    /// the search. The search is performed using the SearchFunction delegate to filter the
    /// SourceData. If a SortingFunction is provided, the filtered results are sorted using it. The
    /// filtered and sorted results are then assigned to the FilteredSource property.
    /// </remarks>
    protected void Search()
    {
        if (SearchString.Length < MinCharacters)
            return;

        IEnumerable<TSearchObject> filter = SourceData.Where(p => SearchFuntion(p, SearchString));
        if (SortingFuntion != null)
            filter = filter.OrderBy(SortingFuntion);
        FilteredSource = filter.ToList();
    }

    /// <summary>
    /// Asynchronously performs a dynamic search based on the provided change event arguments.
    /// </summary>
    /// <param name="changeEventArgs">
    /// The change event arguments containing the value to search for.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected Task SearchDynamicAsync(ChangeEventArgs changeEventArgs)
    {
        if (!DynamicSearch)
            return Task.CompletedTask;
        SearchString = changeEventArgs.Value != null ? (string)changeEventArgs.Value : string.Empty;
        Search();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Asynchronously selects an option and triggers the OnSelectedItem event.
    /// </summary>
    /// <param name="selected">
    /// The option to be selected.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected async Task SelectOptionAsync(TSearchObject selected) =>
        await OnSelectedItem.InvokeAsync(selected);
}