namespace CdCSharp.NjBlazor.Core.Css.Palettes;

/// <summary>
/// Represents an abstract class for defining color palettes.
/// </summary>
public abstract class Palette
{
    /// <summary>Gets or sets the background color of the app bar.</summary>
    /// <value>The background color of the app bar.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor AppbarBackground { get; set; } = NjColors.Blue.Default;

    /// <summary>Gets or sets the color of the app bar text.</summary>
    /// <value>The color of the app bar text.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor AppbarContrast { get; set; } = NjColors.White.Default;

    /// <summary>Gets or sets the background color CSS variable.</summary>
    /// <value>The background color CSS variable.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor Background { get; set; } = NjColors.White.Default;

    /// <summary>Gets or sets the background contrast color as a CSS variable.</summary>
    /// <value>The background contrast color as a CSS variable.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor BackgroundContrast { get; set; } = NjColors.Black.Default;

    /// <summary>Gets or sets the CSS variable for the color black.</summary>
    /// <value>The CSS color variable for black.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor Black { get; set; } = NjColors.Black.Default;

    /// <summary>Gets or sets the CSS color variable for error messages.</summary>
    /// <value>The CSS color variable for error messages.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor Error { get; set; } = NjColors.Red.Default;

    /// <summary>Gets or sets the CSS color variable for error contrast.</summary>
    /// <value>The CSS color variable for error contrast.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor ErrorContrast { get; set; } = NjColors.White.Default;

    /// <summary>
    /// Gets or sets the identifier of the palette.
    /// </summary>
    /// <value>
    /// The identifier of the palette.
    /// </value>
    public virtual string Id { get; set; } = "palette";

    /// <summary>Gets or sets the CSS color variable for information.</summary>
    /// <value>The CSS color variable for information.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor Info { get; set; } = NjColors.Blue.Default;

    /// <summary>Gets or sets the CSS color variable for information contrast.</summary>
    /// <value>The CSS color variable for information contrast.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor InfoContrast { get; set; } = NjColors.White.Default;

    /// <summary>Gets or sets the background color for input elements.</summary>
    /// <value>The background color for input elements.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor InputBackground { get; set; } = NjColors.Transparent.Default;

    /// <summary>Gets or sets the input border color as a CSS variable of type color.</summary>
    /// <value>The input border color as a CSS variable of type color.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor InputBorderColor { get; set; } = NjColors.Black.Default.SetAlpha(0.8);

    /// <summary>Gets or sets the input color CSS variable.</summary>
    /// <value>The input color CSS variable.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor InputColor { get; set; } = NjColors.Black.Default;

    /// <summary>Gets or sets the color of the input options.</summary>
    /// <value>The color of the input options.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor InputOptionsColor { get; set; } = NjColors.Black.Default;

    /// <summary>Gets or sets the background color for input options.</summary>
    /// <value>The background color for input options.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor InputOptionsBackground { get; set; } = NjColors.White.Default;

    /// <summary>Gets or sets the color of the loading bar.</summary>
    /// <value>The color of the loading bar.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor LoadingBarColor { get; set; } = NjColors.Red.Default;

    /// <summary>Represents a CSS color variable for a dark overlay.</summary>
    /// <value>The CSS color value for the dark overlay.</value>
    [CssVariable(CssVariableType.Color)]
    public CssColor OverlayDark { get; set; } = NjColors.Gray.Default.SetAlpha(0.5);

    /// <summary>
    /// Gets or sets the CSS color variable for the light overlay.
    /// </summary>
    /// <value>
    /// The CSS color variable for the light overlay.
    /// </value>
    [CssVariable(CssVariableType.Color)]
    public CssColor OverlayLight { get; set; } = NjColors.White.Default.SetAlpha(0.5);

    /// <summary>Gets or sets the primary color CSS variable.</summary>
    /// <value>The primary color CSS variable.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor Primary { get; set; } = NjColors.Purple.Default;

    /// <summary>Gets or sets the primary contrast color as a CSS variable.</summary>
    /// <value>The primary contrast color as a CSS variable.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor PrimaryContrast { get; set; } = NjColors.White.Default;

    /// <summary>Gets or sets the secondary color CSS variable.</summary>
    /// <value>The secondary color CSS variable.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor Secondary { get; set; } = NjColors.Pink.Default;

    /// <summary>Gets or sets the secondary contrast color as a CSS variable of type color.</summary>
    /// <value>The secondary contrast color as a CSS variable of type color.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor SecondaryContrast { get; set; } = NjColors.White.Default;

    /// <summary>Gets or sets the CSS color variable for success.</summary>
    /// <value>The CSS color variable for success.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor Success { get; set; } = NjColors.Green.Default;

    /// <summary>Gets or sets the CSS color variable for success contrast.</summary>
    /// <value>The CSS color variable for success contrast.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor SuccessContrast { get; set; } = NjColors.White.Default;

    /// <summary>Gets or sets the tertiary color as a CSS variable of type color.</summary>
    /// <value>The tertiary color as a CSS variable of type color.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor Tertiary { get; set; } = NjColors.Orange.Default;

    /// <summary>Gets or sets the tertiary contrast color as a CSS variable.</summary>
    /// <value>The tertiary contrast color as a CSS variable.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor TertiaryContrast { get; set; } = NjColors.White.Default;

    /// <summary>Gets or sets the disabled text color in CSS format.</summary>
    /// <value>The disabled text color in CSS format.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor TextDisabled { get; set; } = NjColors.Black.Default.SetAlpha(0.38);

    /// <summary>Gets or sets the warning color CSS variable.</summary>
    /// <value>The warning color CSS variable.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor Warning { get; set; } = NjColors.Orange.Default;

    /// <summary>Gets or sets the warning contrast color as a CSS variable.</summary>
    /// <value>The warning contrast color as a CSS variable.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor WarningContrast { get; set; } = NjColors.White.Default;

    /// <summary>Gets or sets the CSS variable for the color white.</summary>
    /// <value>The CSS color variable for white.</value>
    [CssVariable(CssVariableType.Color)]
    public virtual CssColor White { get; set; } = NjColors.White.Default;
}
