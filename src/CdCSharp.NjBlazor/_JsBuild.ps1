param(
    [switch]$RunNpmInstall = $true,
    [string]$Configuration = "Debug"
)

try
{
    Write-Host "Running _JsBuild. RunNpmInstall: $RunNpmInstall. Configuration: $Configuration"
    if ($RunNpmInstall)
    {
        Write-Host "Running npm install..."
        npm install
    }

    npx vite build --config vite.config.js

}
catch
{
    Write-Host "Error: $_"
}