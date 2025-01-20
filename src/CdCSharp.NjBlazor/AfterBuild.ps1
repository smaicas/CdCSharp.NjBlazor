param(
    [switch]$RunNpmInstall = $true,
    [string]$Configuration = "Debug"
)

.\_ThemeGen.ps1
.\_ThemeGen_Icons.ps1
.\_CssBuild.ps1 -RunNpmInstall -Configuration $Configuration
.\_JsBuild.ps1 -RunNpmInstall -Configuration $Configuration

Write-Output "AfterBuild script executed for configuration: $RunNpmInstall $Configuration"
