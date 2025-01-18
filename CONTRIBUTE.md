# 🎉 Thank You for Contributing!

Thank you for your interest in improving **[CdCSharp.NjBlazor]**. Your help is greatly valued. 🙌

---

## 📖 Quick Guide to Contributing

Before you begin, please review the following.

### 🛠️ Steps to Contribute

1. **🔍 Open an Issue**
   - If you have an idea, suggestion, or find a bug, open an [issue](https://github.com/smaicas/CdCSharp.NjBlazor/issues) to discuss it before making significant changes.

2. **🍴 Fork the Repository**
   - Create a fork of this repository to start working on your changes without affecting the main branch.

3. **🌿 Create a New Branch**
   - Create a branch with a descriptive name for your change (for example, `fix/typo-correction` or `feat/new-feature`).

4. **💻 Make Commits**
   - Make frequent and descriptive commits that clearly explain the changes made.

5. **🔀 Submit a Pull Request**
   - When your change is ready, open a [Pull Request](https://github.com/smaicas/CdCSharp.NjBlazor/pulls) for the review. Ensure your PR follows the code guidelines and standards.

---

## 💢 Compile the code

### Repository requirements:
    * Git
    * Powershell 7
    * Node (npm)
    * nj-theme-gen dotnet tool

### Building pipeline
The repository executes a postbuild script. 
This script can be deactivated in csproj file commenting the line:
```
	<!--<Target Name="ExecutePowerShellScript" AfterTargets="Build" Condition="'$(Configuration)' == 'Debug'">
		<Exec Command="pwsh -ExecutionPolicy Bypass -File $(ProjectDir)AfterBuild.ps1 $(Configuration)" />
	</Target>-->
```
The script requires nj-theme-gen tool to be installed

#### Theme Gen Tool
This tool is not published in any public repository.
The code of the tool is in the repository under `CdCSharp.NjBlazor.Tools.ThemeGenerator`.
The best way to go is install from local source .nupkg using the powershell scripts in the _scripts folder.

`Local-Revision-OnlyTools-New.ps1` Will generate the local package and add the source
`Install-Local-Tool-Theme-Gen.ps1` Will install the latest generated package

Execute `dotnet build` for building
Execute `dotnet test` for tests 

---

## 🎨 Best Practices

- Class glossary is automatically generated from XML Doc comments. So ensure to describe the code de forma adecuada.
- Write unit tests for your contributions when applicable.
- Ensure all tests pass before submitting a Pull Request.

---

## ❓ Have Questions?

If you have any doubts, feel free to:
- Join our [Discord](https://discord.gg/nX6dBtEh) for real-time communication.

Here to help! 💬

---

# 🚀 Thank You for Your Contribution!

Your input makes a big difference. 

## Enjoy.