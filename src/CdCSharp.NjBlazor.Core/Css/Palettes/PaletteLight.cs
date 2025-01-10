namespace CdCSharp.NjBlazor.Core.Css.Palettes;

public class PaletteLight : Palette
{
    public override CssColor AppbarBackground { get; set; } = NjColors.White.Darken3;
    public override CssColor AppbarContrast { get; set; } = NjColors.Black.Lighten2;
    public override CssColor Background { get; set; } = NjColors.White.Darken3;
    public override CssColor BackgroundContrast { get; set; } = NjColors.Black.Lighten2;
    public override CssColor Black { get; set; } = NjColors.Black.Lighten1;
    public override CssColor Error { get; set; } = NjColors.Red.Default;
    public override string Id { get; set; } = "light";
    public override CssColor Info { get; set; } = NjColors.DodgerBlue.Default;
    public override CssColor InputBackground { get; set; } = NjColors.Transparent.Default;
    public override CssColor InputBorderColor { get; set; } = NjColors.Black.Lighten2.SetAlpha(0.8);
    public override CssColor InputColor { get; set; } = NjColors.Black.Default;
    public override CssColor InputOptionsColor { get; set; } = NjColors.White.Darken3;
    public override CssColor InputOptionsBackground { get; set; } = NjColors.Black.Lighten2;
    public override CssColor Primary { get; set; } = NjColors.Crimson.Darken1;
    public override CssColor PrimaryContrast { get; set; } = NjColors.White.Darken1;
    public override CssColor Secondary { get; set; } = NjColors.Teal.Lighten2;
    public override CssColor SecondaryContrast { get; set; } = NjColors.Black.Default;
    public override CssColor Success { get; set; } = NjColors.ForestGreen.Lighten1;
    public override CssColor Tertiary { get; set; } = NjColors.Goldenrod.Lighten1;
    public override CssColor TertiaryContrast { get; set; } = NjColors.Black.Default;
    public override CssColor TextDisabled { get; set; } = NjColors.Black.Default.SetAlpha(0.38);
    public override CssColor Warning { get; set; } = NjColors.Orange.Default;
    public override CssColor White { get; set; } = NjColors.White.Darken2;
}