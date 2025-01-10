param (
    [Parameter(Mandatory = $true)]
    [string]$name
)

try {
    dotnet tool uninstall $name --local
} catch {
    Write-Output "$name not installed."
}

$pattern = "$name.1.0.0-pre.*"
$directory = "../cdcsharp-njblazor-nupkg-pack"  # Specify the directory path here

# Get the first file that matches the pattern
$file = Get-ChildItem -Path $directory -Filter $pattern | Select-Object -First 1

if ($file) {
    $fileName = $file.BaseName
    # Split the filename into name and version parts based on the known pattern
    $regex = "^(?<name>.+)\.(?<version>\d+\.\d+\.\d+-pre\.\d+)$"
    $matches = [regex]::Match($fileName, $regex)

    if ($matches.Success) {
        $name = $matches.Groups["name"].Value
        $version = $matches.Groups["version"].Value
        Write-Output "Name: $name"
        Write-Output "Version: $version"
        dotnet tool install $name --version $version --local --add-source $directory
    } else {
        Write-Output "The filename does not match the expected pattern."
    }
} else {
    Write-Output "No matching files found."
}