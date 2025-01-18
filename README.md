# CdCSharp.NjBlazor 📚

[![NuGet](https://img.shields.io/nuget/v/CdCSharp.NjBlazor.svg)](https://www.nuget.org/packages/CdCSharp.NjBlazor)
[![License](https://img.shields.io/github/license/smaicas/CdCSharp.NjBlazor)](LICENSE)
[![Build Status](https://img.shields.io/github/actions/workflow/status/smaicas/CdCSharp.NjBlazor/build.yml?branch=master)](https://github.com/smaicas/CdCSharp.NjBlazor/actions/workflows/dotnet.yml)

🚀 A modern Blazor component library for .NET that simplifies UI development.

## 🌟 Features

- 🧣 Extends Web and Form native Blazor components.
- 🎨 Modern and customizable.
- ⚡ High performance and optimization.
- 🔧 Easy integration with existing Blazor projects.
- 📱 Responsive design.
- 🎯 Full .NET & Blazor support.

## 📦 Installation

```bash
dotnet add package CdCSharp.NjBlazor
```

## 🚀 Quick Start

1. Configure the services in `Program.cs`:

```csharp
services.AddNjBlazor();
```

2. Add the necessary css to your html root:

```html
<!-- Main library css -->
<link href="_content/CdCSharp.NjBlazor/css/main.css" rel="stylesheet" />

<!-- Scoped files from library -->
<link href="_content/CdCSharp.NjBlazor/CdCSharp.NjBlazor.bundle.scp.css" rel="stylesheet" />
```

3. (Optional) Add the `EmptyLayout.razor` as layout to your project:

```razor
@layout EmptyLayout
```

## 📚 Documentation

For detailed documentation, visit our [site](https://cdcsharp.github.io/).

## 🛣️ Roadmap

Check our [roadmap](ROADMAP.md)

## 🤝 Contributing

Contributions are welcome. Please read our [contribution guide](CONTRIBUTE.md) before submitting a PR.

## 📄 License

This project is licensed under the GPL v3 License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- The .NET community
- The Blazor Community
- All contributors
