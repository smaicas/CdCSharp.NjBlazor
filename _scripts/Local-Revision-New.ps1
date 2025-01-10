# dotnet build . -c "Release";
# ../nuget.exe locals all -clear
Set-ExecutionPolicy Unrestricted -Scope Process

$NJ_LOCAL_SOURCE_NAME = "cdcsharp-njblazor-nupkg-local"
$NJ_OUTPUT = "cdcsharp-njblazor-nupkg-pack"
$Dateversion = $(get-date -f yyyyMMddhhmmss)

if (-not $(Get-PackageSource -Name $NJ_LOCAL_SOURCE_NAME -ProviderName NuGet -ErrorAction Ignore))
{
	dotnet nuget add source "./$NJ_OUTPUT" --name $NJ_LOCAL_SOURCE_NAME
}

if (Test-Path "../$NJ_OUTPUT") { Remove-Item "../$NJ_OUTPUT" -Recurse -Force -Confirm:$false }
New-Item -Path "../$NJ_OUTPUT" -ItemType "directory"
dotnet nuget disable source $NJ_LOCAL_SOURCE_NAME
dotnet restore --interactive
Get-ChildItem -Recurse "." -Filter "*.csproj" | ForEach-Object { dotnet pack $_ -c "Release" -o "../$NJ_OUTPUT" --version-suffix "pre.$Dateversion" }
# dotnet pack . -c "Release" -o "../$NJ_OUTPUT" --version-suffix "pre.$(get-date -f yyyyMMddhhmmss)"
dotnet nuget enable source $NJ_LOCAL_SOURCE_NAME

if (-not (Test-Path "$env:APPDATA/Nuget/$NJ_OUTPUT")) { New-Item -Path "$env:APPDATA/Nuget/$NJ_OUTPUT" -ItemType "directory" }
Get-ChildItem "../$NJ_OUTPUT" -Filter "*.nupkg" | Copy-Item -Destination "$env:APPDATA/Nuget/$NJ_OUTPUT" -Force -PassThru