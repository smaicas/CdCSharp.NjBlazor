namespace CdCSharp.NjBlazor.Core.Abstractions.Css;
/// <summary>
/// Specifies the mode for justifying content within a container.
/// none	Default value. Specifies no border	
/// hidden	The same as "none", except in border conflict resolution for table elements	
/// dotted	Specifies a dotted border	
/// dashed	Specifies a dashed border	
/// solid	Specifies a solid border	
/// double	Specifies a double border	
/// groove	Specifies a 3D grooved border. The effect depends on the border-color value	
/// ridge	Specifies a 3D ridged border. The effect depends on the border-color value	
/// inset	Specifies a 3D inset border. The effect depends on the border-color value	
/// outset	Specifies a 3D outset border. The effect depends on the border-color value	
/// initial	Sets this property to its default value. Read about initial
/// inherit	Inherits this property from its parent element. Read about inherit
/// </summary>
public enum BorderStyleMode
{
    None,
    Hidden,
    Dotted,
    Dashed,
    Solid,
    Double,
    Groove,
    Ridge,
    Inset,
    Outset,
    Initial,
    Inherit,

}
