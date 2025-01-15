using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using CdCSharp.NjBlazor.Features.Forms.File.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CdCSharp.NjBlazor.Features.Forms.File;

/// <summary>
/// Base class for handling input files in Nj framework.
/// </summary>
/// <typeparam name="IBrowserFile[]">
/// The type representing browser files.
/// </typeparam>
/// <seealso cref="INjInputFileJsCallbacks" />
public abstract class NjInputFileBase : NjInputBase<IBrowserFile[]>, INjInputFileJsCallbacks
{
    /// <summary>
    /// Relays JavaScript callbacks for NjInputFile.
    /// </summary>
    internal NjInputFileJsCallbacksRelay _jsCallbacksRelay;

    private string _text = string.Empty;

    /// <summary>
    /// Gets or sets the background color in CSS format.
    /// </summary>
    /// <value>
    /// A nullable CssColor representing the background color.
    /// </value>
    [Parameter]
    public CssColor? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets an array of file extensions.
    /// </summary>
    /// <value>
    /// The array of file extensions.
    /// </value>
    [Parameter]
    public string[]? Extensions { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of files allowed.
    /// </summary>
    /// <value>
    /// The maximum number of files allowed.
    /// </value>
    [Parameter]
    public int MaxFiles { get; set; } = 5;

    /// <summary>
    /// Gets or sets a value indicating whether multiple items are allowed.
    /// </summary>
    /// <value>
    /// True if multiple items are allowed; otherwise, false.
    /// </value>
    [Parameter]
    public bool Multiple { get; set; } = false;

    /// <summary>
    /// Gets or sets the event callback for when the input file changes.
    /// </summary>
    /// <value>
    /// The event callback for the input file change event.
    /// </value>
    [Parameter]
    public EventCallback<InputFileChangeEventArgs> OnChange { get; set; }

    /// <summary>
    /// Gets or sets the padding value.
    /// </summary>
    /// <value>
    /// The padding value.
    /// </value>
    [Parameter]
    public int Padding { get; set; }

    /// <summary>
    /// Gets or sets the post adornment for the parameter.
    /// </summary>
    /// <value>
    /// The post adornment string.
    /// </value>
    [Parameter]
    public string? PostAdornment { get; set; }

    /// <summary>
    /// Gets or sets the color of the post adornment.
    /// </summary>
    /// <value>
    /// A nullable CssColor representing the color of the post adornment.
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
    /// Gets or sets the color of the pre-adornment for the component.
    /// </summary>
    /// <value>
    /// A nullable CssColor representing the color of the pre-adornment.
    /// </value>
    [Parameter]
    public CssColor? PreAdornmentColor { get; set; }

    /// <summary>
    /// Gets or sets the width of the preview image in pixels.
    /// </summary>
    /// <value>
    /// The width of the preview image in pixels.
    /// </value>
    [Parameter]
    public int PreviewWidthPixels { get; set; } = 150;

    /// <summary>
    /// Gets or sets a value indicating whether to show image previews.
    /// </summary>
    /// <value>
    /// True if image previews should be shown; otherwise, false.
    /// </value>
    [Parameter]
    public bool ShowImagePreviews { get; set; }

    /// <summary>
    /// Gets or sets the text value with a specified transformation.
    /// </summary>
    /// <value>
    /// The text value.
    /// </value>
    /// <remarks>
    /// The Text property allows getting and setting the text value with the specified
    /// transformation provided by the TextTransform function.
    /// </remarks>
    [Parameter]
    public string Text
    {
        get => TextTransform.Invoke(_text);
        set => _text = value;
    }

    /// <summary>
    /// Gets or sets the text transformation function.
    /// </summary>
    /// <value>
    /// The function that transforms input text.
    /// </value>
    /// <remarks>
    /// The default transformation function converts the input text to uppercase.
    /// </remarks>
    [Parameter]
    public Func<string, string> TextTransform { get; set; } = (s) => s.ToUpper();

    /// <summary>
    /// Gets or sets the DOM JavaScript interop service.
    /// </summary>
    /// <remarks>
    /// This property is injected with an instance of the IDOMJsInterop service.
    /// </remarks>
    [Inject]
    protected IDOMJsInterop DomJs { get; set; } = default!;

    /// <summary>
    /// Gets the accepted file extensions as a string.
    /// </summary>
    /// <returns>
    /// A string containing the accepted file extensions separated by commas. If no extensions are
    /// specified, returns "*.*".
    /// </returns>
    protected string ExtensionsAccept => Extensions == null ? "*.*" : string.Join(",", Extensions);

    /// <summary>
    /// Gets or sets the list of image data.
    /// </summary>
    /// <value>
    /// The list of image data.
    /// </value>
    protected List<string> ImagesData { get; set; } = [];

    /// <summary>
    /// Notifies the change in the input file asynchronously.
    /// </summary>
    /// <param name="files">
    /// An array of NjBrowserFile objects representing the files.
    /// </param>
    /// <returns>
    /// A Task representing the asynchronous operation.
    /// </returns>
    async Task INjInputFileJsCallbacks.NotifyChangeAsync(NjBrowserFile[] files)
    {
        for (int i = 0; i < files.Length; i++)
            files[i].Owner = this;

        if (ShowImagePreviews)
        {
            ImagesData.Clear();
            List<NjBrowserFile> imageFiles = files
                .Where(f => f.ContentType.Contains("image"))
                .ToList();
            string t = files[0].ContentType;

            if (InputReference != null)
            {
                foreach (NjBrowserFile imageFile in imageFiles)
                {
                    string imageData = await DomJs.ReadImageDataAsync(
                        InputReference.Value,
                        imageFile.Id
                    );
                    ImagesData.Add(imageData);
                }
            }
        }
        CurrentValue = files;
        await OnChange.InvokeAsync(new InputFileChangeEventArgs(files));
    }

    /// <summary>
    /// Opens a read stream for the specified file.
    /// </summary>
    /// <param name="file">
    /// The file to open a read stream for.
    /// </param>
    /// <param name="maxAllowedSize">
    /// The maximum allowed size for the stream.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A stream for reading the specified file.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when InputReference is not set.
    /// </exception>
    internal Stream OpenReadStream(
        NjBrowserFile file,
        long maxAllowedSize,
        CancellationToken cancellationToken
    )
    {
        if (InputReference == null)
            throw new InvalidOperationException("Invalid operation InputReference not set.");
        return new NjBrowserFileStream(
            DomJs,
            InputReference.Value,
            file,
            maxAllowedSize,
            cancellationToken
        );
    }

    /// <summary>
    /// Method called after rendering the component asynchronously.
    /// </summary>
    /// <param name="firstRender">
    /// A boolean value indicating if this is the first render of the component.
    /// </param>
    /// <returns>
    /// An asynchronous Task.
    /// </returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsCallbacksRelay = new NjInputFileJsCallbacksRelay(this);
            if (InputReference != null)
            {
                await DomJs.InputFileInitializeCallbacksAsync(
                    _jsCallbacksRelay.DotNetReference,
                    InputReference.Value
                );
            }
        }
    }

    /// <summary>
    /// Represents a file preview.
    /// </summary>
    public class FilePreview
    {
        /// <summary>
        /// Gets or sets the extension of a file.
        /// </summary>
        /// <value>
        /// The extension of the file.
        /// </value>
        public string? Extension { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the preview URL.
        /// </summary>
        /// <value>
        /// The preview URL.
        /// </value>
        public string? PreviewUrl { get; set; }

        /// <summary>
        /// Gets or sets the size of an object.
        /// </summary>
        /// <value>
        /// The size of the object.
        /// </value>
        public long Size { get; set; }
    }
}