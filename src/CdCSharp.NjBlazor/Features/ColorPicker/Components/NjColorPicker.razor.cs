using CdCSharp.NjBlazor.Core.Abstractions.Colors;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Collections;
using CdCSharp.NjBlazor.Features.ColorPicker.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CdCSharp.NjBlazor.Features.ColorPicker.Components;

/// <summary>
/// Represents a color picker component that is based on the NjComponentBase class.
/// This class provides functionality for selecting colors.
/// </summary>
public partial class NjColorPicker : NjComponentBase
{
    /// <summary>Gets or sets the ColorPickerJsInterop instance for color picking functionality.</summary>
    /// <value>The ColorPickerJsInterop instance for color picking functionality.</value>
    [Inject]
    IColorPickerJsInterop ColorPickerJs { get; set; } = default!;

    private static readonly Color defaultColor = Color.FromArgb(255, 255, 0, 0);
    private Color _pickedColor = defaultColor;

    /// <summary>Gets or sets the picked color.</summary>
    /// <value>The picked color.</value>
    [Parameter]
    public Color PickedColor
    {
        get => _pickedColor;
        set
        {
            if (_pickedColor == value)
                return;
            _pickedColor = value;
            _hsvColor = HSVColor.FromColor(_pickedColor);

            BaseColorRangeValue = _hsvColor.Hue;

            double coordsX = SelectionWidth - (_hsvColor.Saturation * SelectionWidth);
            double coordsY = SelectionHeight - (_hsvColor.Value * SelectionHeight);

            InvokeAsync(
                () =>
                    ColorPickerJs.RefreshHandlerPositionAsync(
                        selectionReference,
                        handlerSelectionReference,
                        coordsX,
                        coordsY
                    )
            );
            PickedColorChanged.InvokeAsync(_pickedColor);
        }
    }

    /// <summary>Gets or sets the event callback for when the picked color changes.</summary>
    /// <value>The event callback for the picked color change.</value>
    [Parameter]
    public EventCallback<Color> PickedColorChanged { get; set; }

    /// <summary>
    /// Gets or sets the event callback for the save action.
    /// </summary>
    /// <value>
    /// The event callback for the save action.
    /// </value>
    [Parameter]
    public EventCallback OnSave { get; set; }

    /// <summary>Gets or sets the event callback for cancel action.</summary>
    /// <value>The event callback for cancel action.</value>
    [Parameter]
    public EventCallback OnCancel { get; set; }

    /// <summary>Gets or sets the width of the selection.</summary>
    /// <value>The width of the selection. The default value is 200.</value>
    [Parameter]
    public int SelectionWidth { get; set; } = 200;

    /// <summary>Gets or sets the height of the selection.</summary>
    /// <value>The height of the selection.</value>
    [Parameter]
    public int SelectionHeight { get; set; } = 200;

    /// <summary>Gets or sets the throttle time in milliseconds.</summary>
    /// <value>The throttle time in milliseconds.</value>
    [Parameter]
    public int ThrottleMilliseconds { get; set; } = 0;

    /// <summary>Gets or sets the output format for the color picker.</summary>
    /// <value>The output format for the color picker.</value>
    [Parameter]
    public ColorpickerOutputFormat OutputFormat { get; set; } = ColorpickerOutputFormat.RGB;

    private HSVColor _hsvColor = HSVColor.FromColor(defaultColor);

    private HSVColor HsvColor
    {
        get => _hsvColor;
        set
        {
            if (value == _hsvColor)
                return;
            _hsvColor = value;
            _pickedColor = _hsvColor.ToColor(_alphaRangeValue);
            BaseColorRangeValue = _hsvColor.Hue;

            double coordsX = SelectionWidth - (_hsvColor.Saturation * SelectionWidth);
            double coordsY = SelectionHeight - (_hsvColor.Value * SelectionHeight);

            InvokeAsync(
                () =>
                    ColorPickerJs.RefreshHandlerPositionAsync(
                        selectionReference,
                        handlerSelectionReference,
                        coordsX,
                        coordsY
                    )
            );

            PickedColorChanged.InvokeAsync(_pickedColor);
        }
    }
    private HSVColor _hueColor = new(0, 1, 1);
    private int BaseColorRangeValue
    {
        get => _hueColor.Hue;
        set
        {
            if (value == _hueColor.Hue)
                return;
            _hueColor = new HSVColor(value, 1, 1);
            HsvColor = new HSVColor(value, HsvColor.Saturation, HsvColor.Value);
        }
    }

    private int _alphaRangeValue = 255;
    private int AlphaRangeValue
    {
        get => _alphaRangeValue;
        set
        {
            if (value == _alphaRangeValue)
                return;
            _alphaRangeValue = value;
            PickedColor = Color.FromArgb(
                _alphaRangeValue,
                PickedColor.R,
                PickedColor.G,
                PickedColor.B
            );
        }
    }

    ElementReference selectionReference;
    ElementReference handlerSelectionReference;

    private Color _prevPickedColor;
    private HSVColor? _prevHsvColor;

    /// <summary>
    /// This method is called when the component's parameters are set.
    /// It checks if rendering is required based on changes in the picked color or HSV color.
    /// </summary>
    /// <remarks>
    /// The method compares the current picked color and HSV color with the previous values to determine if rendering is necessary.
    /// </remarks>
    protected override void OnParametersSet()
    {
        shouldRender = _prevPickedColor != PickedColor || _prevHsvColor != HsvColor;
        _prevPickedColor = PickedColor;
        _prevHsvColor = HsvColor;
        base.OnParametersSet();
    }

    private bool shouldRender;

    /// <summary>Determines whether rendering should occur.</summary>
    /// <returns>True if rendering should occur, false otherwise.</returns>
    protected override bool ShouldRender() => shouldRender;

    /// <summary>
    /// Called when the element is initialized.
    /// </summary>
    /// <remarks>
    /// Sets up a throttled event handler for mouse drag selection.
    /// </remarks>
    /// <seealso cref="ThrottleEvent{T}(Func{T, Task}, TimeSpan)"/>
    /// <seealso cref="SelectionDragAsync(MouseEventArgs)"/>
    protected override void OnInitialized()
    {
        ThrottleDragSelection = ThrottleEvent<MouseEventArgs>(
            async e => await SelectionDragAsync(e),
            TimeSpan.FromMilliseconds(ThrottleMilliseconds)
        );
        base.OnInitialized();
    }

    Action<MouseEventArgs>? ThrottleDragSelection;

    private async Task SelectionDragAsync(MouseEventArgs args) => await UpdateHsvColorAsync(args);

    private async Task SelectionDragEndAsync(MouseEventArgs args) =>
        await UpdateHsvColorAsync(args);

    private async Task UpdateHsvColorAsync(MouseEventArgs args)
    {
        double[] coords = await ColorPickerJs.RemoveRelativeBoundPositionAsync(
            selectionReference,
            args.ClientX,
            args.ClientY
        );
        HsvColor = new HSVColor(
            HsvColor.Hue,
            (SelectionWidth - coords[0]) / SelectionWidth,
            (SelectionHeight - coords[1]) / SelectionHeight
        );

        await ColorPickerJs.RefreshHandlerPositionAsync(
            selectionReference,
            handlerSelectionReference,
            coords[0],
            coords[1]
        );
    }

    private void SwitchOutputFormat(MouseEventArgs e) => OutputFormat = OutputFormat.Next();

    private static bool ValidateIntervalRGB(int value) => value is >= 0 and <= 255;

    private static bool ValidateIntervalH(int value) => value is >= 0 and <= 360;

    private static bool ValidateIntervalSV(double value) => value is >= 0 and <= 1;

    private bool ValidateHex(string value)
    {
        if (!Regex.IsMatch(value, @"^([0-9a-fA-F]{3}){1,2}$|^([0-9a-fA-F]{4}){1,2}$"))
            return false;

        if (value.Length is not 3 and not 4 and not 6 and not 8)
            return false;

        int colorValue;
        if (int.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out colorValue))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Task SetPreciseRAsync(ChangeEventArgs e)
    {
        if (string.IsNullOrEmpty((string?)e.Value))
            return Task.CompletedTask;

        int value = Math.Clamp(int.Parse((string)e.Value, CultureInfo.InvariantCulture), 0, 255);

        PickedColor = Color.FromArgb(value, PickedColor.G, PickedColor.B);

        return Task.CompletedTask;
    }

    private Task SetPreciseGAsync(ChangeEventArgs e)
    {
        if (string.IsNullOrEmpty((string?)e.Value))
            return Task.CompletedTask;

        int value = Math.Clamp(int.Parse((string)e.Value), 0, 255);

        PickedColor = Color.FromArgb(PickedColor.R, value, PickedColor.B);

        return Task.CompletedTask;
    }

    private Task SetPreciseBAsync(ChangeEventArgs e)
    {
        if (string.IsNullOrEmpty((string?)e.Value))
            return Task.CompletedTask;

        int value = Math.Clamp(int.Parse((string)e.Value), 0, 255);

        PickedColor = Color.FromArgb(PickedColor.R, PickedColor.G, value);

        return Task.CompletedTask;
    }

    private Task SetPreciseHAsync(ChangeEventArgs e)
    {
        if (string.IsNullOrEmpty((string?)e.Value))
            return Task.CompletedTask;

        int value = Math.Clamp(int.Parse((string)e.Value), 0, 360);

        BaseColorRangeValue = value;
        HsvColor = new HSVColor(BaseColorRangeValue, HsvColor.Saturation, HsvColor.Value);

        return Task.CompletedTask;
    }

    private async Task SetPreciseSAsync(ChangeEventArgs e)
    {
        if (string.IsNullOrEmpty((string?)e.Value))
            return;

        double value = Math.Clamp(
            double.Parse(((string)e.Value), NumberStyles.Float, CultureInfo.InvariantCulture),
            0.0d,
            1.0d
        );

        HsvColor = new HSVColor(HsvColor.Hue, value, HsvColor.Value);

        double coordsX = SelectionWidth - (value * SelectionWidth);
        double coordsY = SelectionHeight - (HsvColor.Value * SelectionHeight);

        await ColorPickerJs.RefreshHandlerPositionAsync(
            selectionReference,
            handlerSelectionReference,
            coordsX,
            coordsY
        );
    }

    private async Task SetPreciseVAsync(ChangeEventArgs e)
    {
        if (string.IsNullOrEmpty((string?)e.Value))
            return;

        double value = Math.Clamp(
            double.Parse(((string)e.Value), NumberStyles.Float, CultureInfo.InvariantCulture),
            0.0d,
            1.0d
        );

        HsvColor = new HSVColor(HsvColor.Hue, HsvColor.Saturation, value);

        double coordsX = SelectionWidth - (HsvColor.Saturation * SelectionWidth);
        double coordsY = SelectionHeight - (value * SelectionHeight);

        await ColorPickerJs.RefreshHandlerPositionAsync(
            selectionReference,
            handlerSelectionReference,
            coordsX,
            coordsY
        );
    }

    private void SetPreciseAlpha(ChangeEventArgs e)
    {
        if (string.IsNullOrEmpty((string?)e.Value))
            return;

        int value = Math.Clamp(int.Parse((string)e.Value), 0, 255);

        AlphaRangeValue = value;
    }

    private Task SetPreciseHexAsync(ChangeEventArgs e)
    {
        if (string.IsNullOrEmpty((string?)e.Value))
            return Task.CompletedTask;

        e.Value = ((string)e.Value).TrimStart('#');
        if (!ValidateHex((string)e.Value))
            return Task.CompletedTask;

        Color? color = FromHexA((string)e.Value);

        if (color == null)
            return Task.CompletedTask;

        HsvColor = HSVColor.FromColor((Color)color);

        return Task.CompletedTask;
    }

    private static Color? FromHexA(string hex)
    {
        hex = hex.TrimStart('#');

        if (hex.Length is not 3 and not 4 and not 6 and not 8)
        {
            return null;
        }

        int r,
            g,
            b;
        if (hex.Length <= 4)
        {
            r = int.Parse(hex.Substring(0, 1), System.Globalization.NumberStyles.HexNumber) * 17;
            g = int.Parse(hex.Substring(1, 1), System.Globalization.NumberStyles.HexNumber) * 17;
            b = int.Parse(hex.Substring(2, 1), System.Globalization.NumberStyles.HexNumber) * 17;
        }
        else
        {
            r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }

        byte alpha = 255;
        if (hex.Length is 4 or 8)
        {
            alpha = byte.Parse(
                hex.Substring(hex.Length - 2),
                System.Globalization.NumberStyles.HexNumber
            );
        }

        return Color.FromArgb(alpha, r, g, b);
    }

    private async Task SaveAsync(MouseEventArgs e) => await OnSave.InvokeAsync();

    private async Task CancelAsync(MouseEventArgs e) => await OnCancel.InvokeAsync();
}
