Set-ExecutionPolicy Unrestricted -Scope Process

$NJ_LOCAL_SOURCE_NAME = "cdcsharp-njblazor-nupkg-local"
$NJ_OUTPUT = "cdcsharp-njblazor-nupkg-pack"
$Dateversion = $(get-date -f yyyyMMddhhmmss)

$tools = @("CdCSharp.NjBlazor.Tools.ThemeGenerator")

function ContainsToolName {
    param (
        [string]$fileName,
        [string[]]$toolNames
    )
    foreach ($tool in $toolNames) {
        if ($fileName -like "*$tool*") {
            Write-Host "Marked to remove $fileName"
            return $true
        }
    }
    return $false
}

if (-not $(Get-PackageSource -Name $NJ_LOCAL_SOURCE_NAME -ProviderName NuGet -ErrorAction Ignore)) {
    dotnet nuget add source "./$NJ_OUTPUT" --name $NJ_LOCAL_SOURCE_NAME
}

if (Test-Path "../$NJ_OUTPUT") {
    Get-ChildItem "../$NJ_OUTPUT" -Filter "*.nupkg" | Where-Object { ContainsToolName $_.Name $tools } | Remove-Item -Force
} else {
    New-Item -Path "../$NJ_OUTPUT" -ItemType "directory"
}

dotnet nuget disable source $NJ_LOCAL_SOURCE_NAME
dotnet restore --interactive

# Pack only the projects corresponding to the tools
Get-ChildItem -Recurse "." -Filter "*.csproj" | ForEach-Object {
    $projectName = [System.IO.Path]::GetFileNameWithoutExtension($_.FullName)
    if (ContainsToolName $projectName $tools) {
        dotnet pack $_ -c "Release" -o "../$NJ_OUTPUT" --version-suffix "pre.$Dateversion"
    }
}

dotnet nuget enable source $NJ_LOCAL_SOURCE_NAME

if (-not (Test-Path "$env:APPDATA/Nuget/$NJ_OUTPUT")) {
    New-Item -Path "$env:APPDATA/Nuget/$NJ_OUTPUT" -ItemType "directory"
}

Get-ChildItem "../$NJ_OUTPUT" -Filter "*.nupkg" | Copy-Item -Destination "$env:APPDATA/Nuget/$NJ_OUTPUT" -Force -PassThru