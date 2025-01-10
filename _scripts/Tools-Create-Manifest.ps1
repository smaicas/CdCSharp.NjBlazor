if (Test-Path ".config/dotnet-tools.json") {throw "dotnet-tools.json already created"}
dotnet new tool-manifest