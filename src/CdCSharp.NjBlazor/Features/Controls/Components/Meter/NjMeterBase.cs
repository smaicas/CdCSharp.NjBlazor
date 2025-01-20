using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Meter;

/// <summary>
/// Base class for NjMeter components.
/// </summary>
public abstract class NjMeterBase : NjControlComponentBase
{
    /// <summary>
    /// Gets or sets the high value.
    /// </summary>
    /// <value>
    /// The high value.
    /// </value>
    [Parameter]
    public double High { get; set; } = 50;

    /// <summary>
    /// Gets or sets the high color for the element.
    /// </summary>
    /// <value>
    /// The high color represented as a CssColor. Default value is NjColors.Orange.Default.
    /// </value>
    [Parameter]
    public CssColor HighColor { get; set; } = NjColors.Orange.Default;

    /// <summary>
    /// Gets or sets the label to display.
    /// </summary>
    /// <value>
    /// The label to display.
    /// </value>
    [Parameter]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the low value.
    /// </summary>
    /// <value>
    /// The low value.
    /// </value>
    [Parameter]
    public double Low { get; set; } = 25;

    /// <summary>
    /// Gets or sets the low color for the element.
    /// </summary>
    /// <value>
    /// The low color represented as a CssColor. Default value is NjColors.Red.Default.
    /// </value>
    [Parameter]
    public CssColor LowColor { get; set; } = NjColors.Red.Default;

    /// <summary>
    /// Gets or sets the maximum width in pixels.
    /// </summary>
    /// <value>
    /// The maximum width in pixels.
    /// </value>
    [Parameter]
    public int MaxWidthPx { get; set; } = 200;

    /// <summary>
    /// Gets or sets the optimum value.
    /// </summary>
    /// <value>
    /// The optimum value.
    /// </value>
    [Parameter]
    public double Optimum { get; set; } = 75;

    /// <summary>
    /// Gets or sets the optimum color for the element.
    /// </summary>
    /// <value>
    /// The optimum color represented as a CssColor.
    /// </value>
    [Parameter]
    public CssColor OptimumColor { get; set; } = NjColors.Green.Default;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The current value.
    /// </value>
    [Parameter]
    public double Value { get; set; } = 100;

    /// <summary>
    /// Gets the fill color based on the current value.
    /// </summary>
    /// <returns>
    /// The fill color based on the current value.
    /// </returns>
    protected CssColor FillColor
    {
        get
        {
            if (Value <= Low)
                return LowColor;
            if (Value > Low && Value < Optimum)
                return HighColor;
            if (Value >= Optimum)
                return OptimumColor;
            return LowColor;
        }
    }

    /// <summary>
    /// Calculates the percentage width based on the current value and maximum width in pixels.
    /// </summary>
    /// <value>
    /// The percentage width as an integer.
    /// </value>
    protected int FillWidthPercent => (int)Value * 100 / MaxWidthPx;

    /// <summary>
    /// Validates and sets the parameters for the component.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown when the parameters are not valid.
    /// </exception>
    protected override void OnParametersSet()
    {
        bool valid = Low < High && High < Optimum;
        if (!valid)
            throw new ArgumentException(
                "Parameter Min must be lower than High. Parameter High must be lower than Optimum"
            );
    }
}