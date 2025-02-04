try
{
    dotnet tool run nj-theme-gen -- variables -o CssBundle -f "_variables.css"
    dotnet tool run nj-theme-gen -- theme -o CssBundle -f "_theme.css"
}
catch
{
    Write-Host "Error: $_"
}