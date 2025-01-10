try
{
    dotnet tool run nj-theme-gen -- --icons
}
catch
{
    Write-Host "Error: $_"
}

