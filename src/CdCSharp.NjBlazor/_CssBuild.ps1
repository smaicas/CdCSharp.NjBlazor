param(
    [switch]$RunNpmInstall = $true,
    [string]$Configuration = "Debug"
)

# Function to rename the CSS file
function Rename-CssFile {
  param (
    [string] $SourcePath = "./wwwroot/css",  
    [string] $TargetPath = "./wwwroot/css",
    [string] $TargetName = "main.css"
  )

  # Specify the directory where the files are located

# Get the file with the changing pattern using wildcard match
$fileToRename = Get-ChildItem -Path $SourcePath -Filter "main-*.css" | Select-Object -First 1

# Check if a file is found
if ($fileToRename) {
    $newFileName = "main.css"

    $newFilePath = Join-Path -Path $TargetPath -ChildPath $TargetName

    Rename-Item -Path $fileToRename.FullName -NewName $newFileName -Force

    Write-Output "File renamed successfully."
} else {
    Write-Output "No file found matching the pattern."
}
}

try
{
    Write-Host "Running _JsBuild. RunNpmInstall: $RunNpmInstall. Configuration: $Configuration"
    if ($RunNpmInstall)
    {
        Write-Host "Running npm install..."
        npm install
    }

    npx vite build --config vite.config.css.js

    Rename-CssFile
}
catch
{
    Write-Host "Error: $_"
}
