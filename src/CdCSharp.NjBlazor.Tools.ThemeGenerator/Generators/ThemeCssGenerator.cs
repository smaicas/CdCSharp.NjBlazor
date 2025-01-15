using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Css;
using CdCSharp.NjBlazor.Core.Css.Palettes;
using CdCSharp.NjBlazor.Core.Strings;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace CdCSharp.NjBlazor.Tools.ThemeGenerator.Generators;

/// <summary>
/// Variables.css
/// </summary>
/// <summary>
/// Contains methods for generating CSS based on the selected theme.
/// </summary>
public static partial class ThemeCssGenerator
{
    private static readonly Palette[] Palettes = [new PaletteDark(), new PaletteLight()];
    private static Palette RootPalette { get; } = new PaletteDark { Id = "palette" };

    /// <summary>
    /// Builds CSS variables as a string asynchronously.
    /// </summary>
    /// <returns>
    /// A string representing CSS variables.
    /// </returns>
    public static async Task<string> BuildVariablesCss()
    {
        StringBuilder css = new();
        PrintCssVariables(css, GetVariablesByPrefix().SelectMany(kv => kv.Value));
        return await Task.FromResult(css.ToString());
    }

    /// <summary>
    /// Builds a CSS file containing variables.
    /// </summary>
    /// <param name="rootPath">
    /// The root path of the project.
    /// </param>
    /// <param name="outputFolder">
    /// The output folder where the CSS file will be saved.
    /// </param>
    /// <param name="outputFile">
    /// The name of the output CSS file.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    public static async Task BuildVariablesCssFile(string rootPath, string outputFolder, string outputFile)
    {
        string css = await BuildVariablesCss();

        Directory.CreateDirectory(outputFolder);

        bool relativeToRoot = false == outputFolder.Contains(":");

        string outputPath = relativeToRoot ? Path.Combine(rootPath, outputFolder, outputFile) : Path.Combine(outputFolder, outputFile);

        using (FileStream stream = new(outputPath, FileMode.Create, FileAccess.Write, FileShare.Write, 4096, useAsync: true))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(css.ToString());
            await stream.WriteAsync(bytes, 0, bytes.Length);
        }
    }

    /// <summary>
    /// Appends CSS attributes to a StringBuilder.
    /// </summary>
    /// <param name="css">
    /// The StringBuilder to which the CSS attributes will be appended.
    /// </param>
    /// <param name="cssAttributes">
    /// The collection of CSS attributes to be printed.
    /// </param>
    public static void PrintCssAttributes(StringBuilder css, IEnumerable<CssAttribute> cssAttributes)
    {
        foreach (CssAttribute cssAttribute in cssAttributes)
        {
            css.AppendLine($"{cssAttribute.Name}: {cssAttribute.Value};");
        }
    }

    /// <summary>
    /// Prints CSS styles based on the provided CSS styles collection.
    /// </summary>
    /// <param name="css">
    /// The StringBuilder to append the CSS styles to.
    /// </param>
    /// <param name="cssStyles">
    /// The collection of CSS styles to print.
    /// </param>
    public static void PrintCssStyles(StringBuilder css, IEnumerable<CssStyle> cssStyles)
    {
        foreach (CssStyle cssStyle in cssStyles)
        {
            css.AppendLine($"{cssStyle.Selector} {{");
            PrintCssAttributes(css, cssStyle.Attributes);
            css.AppendLine("}");
        }
    }

    /// <summary>
    /// Prints CSS variables to a StringBuilder.
    /// </summary>
    /// <param name="css">
    /// The StringBuilder to which the CSS variables will be printed.
    /// </param>
    /// <param name="cssVariables">
    /// The collection of CSS variables to print.
    /// </param>
    /// <param name="cssVariablesRoot">
    /// The root element for CSS variables (default is ":root {").
    /// </param>
    public static void PrintCssVariables(StringBuilder css, IEnumerable<CssVariable> cssVariables, string? cssVariablesRoot = ":root { /* Nj Theme : Palette */")
    {
        css.AppendLine(cssVariablesRoot);
        foreach (CssVariable cssVariable in cssVariables)
        {
            css.AppendLine($"{cssVariable.FullName}: {cssVariable.Value};");
        }

        css.AppendLine("}");
    }

    /// <summary>
    /// Processes a palette object to generate a list of CSS variables representing color properties.
    /// </summary>
    /// <param name="palette">
    /// The palette object to process.
    /// </param>
    /// <returns>
    /// A list of CSS variables representing color properties from the palette.
    /// </returns>
    public static List<CssVariable> ProcessPaletteToCssVariables(Palette palette)
    {
        List<CssVariable> cssVariables = [];
        foreach (PropertyInfo propertyInfo in palette.GetType().GetProperties())
        {
            Attribute? attribute =
                Attribute.GetCustomAttribute(propertyInfo, typeof(CssVariableAttribute), true);

            if (attribute == null) continue;

            if (!CssVariableType.Color.Equals(((CssVariableAttribute)attribute).Type)) continue;

            string? name = propertyInfo.Name.Transform<StringExtensions.CamelCaseStringTransformer>();
            if (string.IsNullOrEmpty(name)) continue;
            string? value = propertyInfo.GetValue(palette)?.ToString();
            if (string.IsNullOrEmpty(value)) continue;

            cssVariables.Add(new CssVariable(palette.Id, name.ToLower()!, value!, CssVariableType.Color));
        }

        return cssVariables;
    }

    private static Dictionary<string, IEnumerable<CssVariable>> GetVariablesByPrefix()
    {
        Dictionary<string, IEnumerable<CssVariable>> dict = new()
        {
            { RootPalette.Id, ProcessPaletteToCssVariables(RootPalette) }
        };

        foreach (Palette palette in Palettes)
        {
            dict.Add(palette.Id, ProcessPaletteToCssVariables(palette));
        }

        return dict;
    }
}

/// <summary>
/// Contains methods for generating CSS based on the selected theme.
/// </summary>
public static partial class ThemeCssGenerator
{
    /// <summary>
    /// Builds a CSS theme based on generated CSS styles.
    /// </summary>
    /// <returns>
    /// A string representing the CSS theme.
    /// </returns>
    public static async Task<string> BuildThemeCss()
    {
        StringBuilder css = new();
        List<CssStyle> cssStyles = GenerateCssStyles();
        PrintCssStyles(css, cssStyles);
        return await Task.FromResult(css.ToString());
    }

    /// <summary>
    /// Builds a CSS file for the theme based on the provided root path, output folder, and output file.
    /// </summary>
    /// <param name="rootPath">
    /// The root path where the CSS file will be saved.
    /// </param>
    /// <param name="outputFolder">
    /// The folder where the CSS file will be stored.
    /// </param>
    /// <param name="outputFile">
    /// The name of the output CSS file.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    public static async Task BuildThemeCssFile(string rootPath, string outputFolder, string outputFile)
    {
        string css = await BuildThemeCss();

        Directory.CreateDirectory(outputFolder);

        bool relativeToRoot = false == outputFolder.Contains(":");

        string outputPath = relativeToRoot ? Path.Combine(rootPath, outputFolder, outputFile) : Path.Combine(outputFolder, outputFile);

        using (FileStream stream = new(outputPath, FileMode.Create, FileAccess.Write, FileShare.Write, 4096, useAsync: true))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(css.ToString());
            await stream.WriteAsync(bytes, 0, bytes.Length);
        }
    }

    private static void AddAlignStyles(List<CssStyle> styles)
    {
        styles.Add(new CssStyle($".{CssClassReferences.AlignItems.AlignItemsBaseline}",
            new CssAttribute("align-items", "baseline")));
        styles.Add(new CssStyle($".{CssClassReferences.AlignItems.AlignItemsCenter}",
            new CssAttribute("align-items", "center")));
        styles.Add(new CssStyle($".{CssClassReferences.AlignItems.AlignItemsStart}",
            new CssAttribute("align-items", "flex-start")));
        styles.Add(new CssStyle($".{CssClassReferences.AlignItems.AlignItemsEnd}",
            new CssAttribute("align-items", "flex-end")));
        styles.Add(new CssStyle($".{CssClassReferences.AlignItems.AlignItemsStretch}",
            new CssAttribute("align-items", "stretch")));

        styles.Add(new CssStyle($".{CssClassReferences.AlignSelf.AlignSelfAuto}",
            new CssAttribute("align-self", "auto")));
        styles.Add(new CssStyle($".{CssClassReferences.AlignSelf.AlignSelfStart}",
            new CssAttribute("align-self", "flex-start")));
        styles.Add(new CssStyle($".{CssClassReferences.AlignSelf.AlignSelfEnd}",
            new CssAttribute("align-self", "flex-end")));
        styles.Add(new CssStyle($".{CssClassReferences.AlignSelf.AlignSelfCenter}",
            new CssAttribute("align-self", "center")));
        styles.Add(new CssStyle($".{CssClassReferences.AlignSelf.AlignSelfBaseline}",
            new CssAttribute("align-self", "baseline")));
        styles.Add(new CssStyle($".{CssClassReferences.AlignSelf.AlignSelfStretch}",
            new CssAttribute("align-self", "stretch")));
    }

    private static void AddBaseStyles(List<CssStyle> styles)
    {
        styles.Add(new CssStyle($".{CssClassReferences.Disabled}",
            new CssAttribute("opacity", "0.5"),
            new CssAttribute("pointer-events", "none"),
            new CssAttribute("cursor", "crosshair")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Pointer}",
            new CssAttribute("cursor", "pointer")
        ));
    }

    private static void AddColorStyles(List<CssStyle> styles)
    {
        styles.Add(new CssStyle("body",
            new CssAttribute("background-color", "var(--palette-background)"),
            new CssAttribute("color", "var(--palette-backgroundcontrast)")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Prefix.Color}palette",
            new CssAttribute("color", "var(--palette-backgroundcontrast)")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Prefix.Color}primary",
            new CssAttribute("color", "var(--palette-primary)")
        ));

        styles.Add(new CssStyle($"{CssClassReferences.Prefix.Color}secondary",
            new CssAttribute("color", "var(--palette-secondary)")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Prefix.Color}tertiary",
            new CssAttribute("color", "var(--palette-tertiary)")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Prefix.Background}palette",
            new CssAttribute("background-color", "var(--palette-background)")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Prefix.Background}primary",
            new CssAttribute("background-color", "var(--palette-primary)")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Prefix.Background}secondary",
            new CssAttribute("background-color", "var(--palette-secondary)")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Prefix.Background}tertiary",
            new CssAttribute("background-color", "var(--palette-tertiary)")
        ));
    }

    private static void AddFlexStyles(List<CssStyle> styles)
    {
        const double GapMultiplier = 0.8;

        styles.Add(new CssStyle($".{CssClassReferences.FlexBox.Flex}",
            new CssAttribute("display", "-webkit-box"),
            new CssAttribute("display", "-moz-box"),
            new CssAttribute("display", "-ms-flexbox"),
            new CssAttribute("display", "-moz-flex"),
            new CssAttribute("display", "-webkit-flex"),
            new CssAttribute("display", "flex")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.FlexBox.InlineFlex}",
            new CssAttribute("display", "-webkit-inline-box"),
            new CssAttribute("display", "-moz-inline-box"),
            new CssAttribute("display", "-ms-inline-flexbox"),
            new CssAttribute("display", "-moz-inline-flex"),
            new CssAttribute("display", "-webkit-inline-flex"),
            new CssAttribute("display", "inline-flex")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.FlexBox.Flex}.{CssClassReferences.FlexBox.Row}, .{CssClassReferences.FlexBox.InlineFlex}.{CssClassReferences.FlexBox.Row}",
            new CssAttribute("flex-direction", "row")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.FlexBox.Flex}.{CssClassReferences.FlexBox.RowReverse}, .{CssClassReferences.FlexBox.InlineFlex}.{CssClassReferences.FlexBox.RowReverse}",
            new CssAttribute("flex-direction", "row-reverse")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.FlexBox.Flex}.{CssClassReferences.FlexBox.Column}, .{CssClassReferences.FlexBox.InlineFlex}.{CssClassReferences.FlexBox.Column}",
            new CssAttribute("flex-direction", "column")
        ));
        styles.Add(new CssStyle($".{CssClassReferences.FlexBox.Flex}.{CssClassReferences.FlexBox.ColumnReverse}, .{CssClassReferences.FlexBox.InlineFlex}.{CssClassReferences.FlexBox.ColumnReverse}",
            new CssAttribute("flex-direction", "column-reverse")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.FlexBox.FlexWrapNoWrap}",
            new CssAttribute("flex-wrap", "nowrap")));

        styles.Add(new CssStyle($".{CssClassReferences.FlexBox.FlexWrapWrap}",
            new CssAttribute("flex-wrap", "wrap")));

        styles.Add(new CssStyle($".{CssClassReferences.FlexBox.FlexWrapWrapReverse}",
            new CssAttribute("flex-wrap", "wrap-reverse")));

        for (int i = 0; i <= CssClassReferences.MaxGapValue; i++)
        {
            styles.Add(new CssStyle($".{CssClassReferences.Prefix.Gap}{i}",
                new CssAttribute("gap", $"{CssTools.ToCssNumber(GapMultiplier * i)}rem")));
        }
    }

    private static void AddJustifyStyles(List<CssStyle> styles)
    {
        styles.Add(new CssStyle($".{CssClassReferences.JustifyItems.JustifyItemsStart}",
                new CssAttribute("justify-items", "flex-start")));
        styles.Add(new CssStyle($".{CssClassReferences.JustifyItems.JustifyItemsEnd}",
                new CssAttribute("justify-items", "flex-end")));
        styles.Add(new CssStyle($".{CssClassReferences.JustifyItems.JustifyItemsCenter}",
                new CssAttribute("justify-items", "center")));
        styles.Add(new CssStyle($".{CssClassReferences.JustifyItems.JustifyItemsStretch}",
                new CssAttribute("justify-items", "stretch")));
        styles.Add(new CssStyle($".{CssClassReferences.JustifyItems.JustifyItemsNormal}",
                new CssAttribute("justify-items", "normal")));

        styles.Add(new CssStyle($".{CssClassReferences.JustifyContent.JustifyContentStart}",
            new CssAttribute("justify-content", "flex-start")));
        styles.Add(new CssStyle($".{CssClassReferences.JustifyContent.JustifyContentCenter}",
            new CssAttribute("justify-content", "center")));
        styles.Add(new CssStyle($".{CssClassReferences.JustifyContent.JustifyContentEnd}",
            new CssAttribute("justify-content", "flex-end")));
        styles.Add(new CssStyle($".{CssClassReferences.JustifyContent.JustifyContentSpaceBetween}",
            new CssAttribute("justify-content", "space-between")));
        styles.Add(new CssStyle($".{CssClassReferences.JustifyContent.JustifyContentSpaceAround}",
            new CssAttribute("justify-content", "space-around")));
        styles.Add(new CssStyle($".{CssClassReferences.JustifyContent.JustifyContentSpaceEvenly}",
            new CssAttribute("justify-content", "space-evenly")));
    }

    private static void AddLevelStyles(List<CssStyle> styles)
    {
        styles.Add(new CssStyle(".nj-level-hidden",
            new CssAttribute("z-index", "-1")
        ));
        styles.Add(new CssStyle(".nj-level-0",
            new CssAttribute("z-index", "0")
        ));
        styles.Add(new CssStyle(".nj-level-1",
            new CssAttribute("z-index", "100")
        ));
        styles.Add(new CssStyle(".nj-level-2",
            new CssAttribute("z-index", "200")
        ));
        styles.Add(new CssStyle(".nj-level-3",
            new CssAttribute("z-index", "300")
        ));
        styles.Add(new CssStyle(".nj-level-4",
            new CssAttribute("z-index", "300")
        ));
        styles.Add(new CssStyle(".nj-level-5",
            new CssAttribute("z-index", "500")
        ));
        styles.Add(new CssStyle(".nj-level-6",
            new CssAttribute("z-index", "600")
        ));
        styles.Add(new CssStyle(".nj-level-7",
            new CssAttribute("z-index", "700")
        ));
        styles.Add(new CssStyle(".nj-level-8",
            new CssAttribute("z-index", "800")
        ));
        styles.Add(new CssStyle(".nj-level-9",
            new CssAttribute("z-index", "900")
        ));
        styles.Add(new CssStyle(".nj-level-10",
            new CssAttribute("z-index", "1000")
        ));
    }

    private static void AddPaddingMarginStyles(List<CssStyle> styles)
    {
        float multiplier = 0.4f;

        for (int i = 0; i < CssClassReferences.MaxPaddingValue; i++)
        {
            string stringValue = (multiplier * i).ToString(CultureInfo.InvariantCulture);
            styles.Add(new CssStyle($".{CssClassReferences.Prefix.Padding}{i}",
           new CssAttribute("padding", i == 0 ? "0" : $"{stringValue}rem")));
            styles.Add(new CssStyle($".{CssClassReferences.Prefix.PaddingX}{i}",
           new CssAttribute("padding-left", i == 0 ? "0" : $"{stringValue}rem"),
           new CssAttribute("padding-right", i == 0 ? "0" : $"{stringValue}rem")));
            styles.Add(new CssStyle($".{CssClassReferences.Prefix.PaddingY}{i}",
           new CssAttribute("padding-top", i == 0 ? "0" : $"{stringValue}rem"),
           new CssAttribute("padding-bottom", i == 0 ? "0" : $"{stringValue}rem")));

            styles.Add(new CssStyle($".{CssClassReferences.Prefix.Margin}{i}",
           new CssAttribute("margin", i == 0 ? "0" : $"{stringValue}rem")));
            styles.Add(new CssStyle($".{CssClassReferences.Prefix.MarginX}{i}",
           new CssAttribute("margin-left", i == 0 ? "0" : $"{stringValue}rem"),
           new CssAttribute("margin-right", i == 0 ? "0" : $"{stringValue}rem")));
            styles.Add(new CssStyle($".{CssClassReferences.Prefix.MarginY}{i}",
           new CssAttribute("margin-top", i == 0 ? "0" : $"{stringValue}rem"),
           new CssAttribute("margin-bottom", i == 0 ? "0" : $"{stringValue}rem")));
        }
    }

    private static void AddPaletteStyles(List<CssStyle> styles)
    {
        Dictionary<string, IEnumerable<CssVariable>> cssVariables = GetVariablesByPrefix();
        IEnumerable<CssVariable> rootCssVariables = cssVariables[RootPalette.Id];
        IEnumerable<KeyValuePair<string, IEnumerable<CssVariable>>> dynamicCssVariables = cssVariables.Where(kv => kv.Key != RootPalette.Id);

        foreach (KeyValuePair<string, IEnumerable<CssVariable>> dynamicCssVariablePair in dynamicCssVariables)
        {
            List<CssAttribute> attributes = [];
            foreach (CssVariable dynamicCssVariable in dynamicCssVariablePair.Value)
            {
                CssVariable rootVariable = rootCssVariables.First(variable => variable.Name == dynamicCssVariable.Name);
                attributes.Add(new CssAttribute($"{rootVariable.FullName.ToLower()}", $"var({dynamicCssVariable.FullName.ToLower()})"));
            }

            styles.Add(new CssStyle($"html.{dynamicCssVariablePair.Key.ToLower()}", attributes.ToArray()));
        }
    }

    private static void AddPositionStyles(List<CssStyle> styles)
    {
        styles.Add(new CssStyle($".{CssClassReferences.Position.Static}",
            new CssAttribute("position", "static")
        ));
        styles.Add(new CssStyle($".{CssClassReferences.Position.Relative}",
            new CssAttribute("position", "relative")
        ));
        styles.Add(new CssStyle($".{CssClassReferences.Position.Absolute}",
            new CssAttribute("position", "absolute")
        ));
        styles.Add(new CssStyle($".{CssClassReferences.Position.Fixed}",
            new CssAttribute("position", "fixed")
        ));
        styles.Add(new CssStyle($".{CssClassReferences.Position.Sticky}",
            new CssAttribute("position", "sticky")
        ));
    }

    private static void AddTextStyles(List<CssStyle> styles)
    {
        styles.Add(new CssStyle($".{CssClassReferences.Text.AlignCenter}",
            new CssAttribute("text-align", "center")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Text.AlignRight}",
            new CssAttribute("text-align", "right")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Text.Bold}",
            new CssAttribute("font-weight", "bold")
        ));

        styles.Add(new CssStyle($".{CssClassReferences.Text.Italic}",
            new CssAttribute("font-style", "italic")));

        styles.Add(new CssStyle($".{CssClassReferences.Text.Underline}",
            new CssAttribute("text-decoration", "underline")));
    }

    private static void AddWidthStyles(List<CssStyle> styles)
    {
        styles.Add(new CssStyle(".nj-fit-content",
            new CssAttribute("width", "fit-content"),
            new CssAttribute("width", "-moz-fit-content")));

        styles.Add(new CssStyle(".nj-w-100",
            new CssAttribute("width", "100%")));

        styles.Add(new CssStyle(".nj-w-100vh",
            new CssAttribute("width", "100vh")));

        styles.Add(new CssStyle(".nj-h-100vh",
            new CssAttribute("height", "100vh")));
    }

    private static List<CssStyle> GenerateCssStyles()
    {
        List<CssStyle> styles = [];

        AddPaletteStyles(styles);
        AddBaseStyles(styles);
        AddLevelStyles(styles);
        AddColorStyles(styles);
        AddFlexStyles(styles);
        AddAlignStyles(styles);
        AddJustifyStyles(styles);
        AddPositionStyles(styles);
        AddTextStyles(styles);
        AddWidthStyles(styles);
        AddPaddingMarginStyles(styles);

        return styles;
    }
}