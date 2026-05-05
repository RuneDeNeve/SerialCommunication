Repository: SerialCommunication (WinForms desktop app)

1) Build, run, test, lint
- Open the solution/project in Visual Studio 2017+ and press F5 to run.
- CLI (MSBuild):
  - Build (Debug): msbuild SerialCommunication\SerialCommunication.csproj /p:Configuration=Debug
  - Build (Release): msbuild SerialCommunication\SerialCommunication.csproj /p:Configuration=Release
- CLI (dotnet SDK may work if installed): dotnet build SerialCommunication\SerialCommunication.csproj
- Run binary: SerialCommunication\bin\Debug\SerialCommunication.exe (or bin\Release)
- Tests: none in this repo. No test runner configured; to add tests create a separate test project.
- Lint/formatters: none configured here.

2) High-level architecture (big picture)
- Single Visual Studio C# WinForms project targeting .NET Framework 4.7.2.
- Entry point: Program.Main -> Application.Run(new Form1()).
- UI/interaction: Form1 (.Designer.cs + .resx) — enumerates serial ports (System.IO.Ports.SerialPort.GetPortNames()), provides port/baud selection and a connect button (buttonConnect_Click is the intended place for connection logic).
- Resources: embedded images and strings live in Properties/ and Form1.resx (images under Resources\).
- Output: standard Visual Studio output paths (bin\Debug, bin\Release).

3) Key conventions and patterns (repo-specific)
- Designer-first workflow: UI layout is managed by Visual Studio designer. Do NOT hand-edit Form1.Designer.cs; change layout/events through the designer to avoid losing metadata.
- Control naming: existing controls use names like comboBoxPoort (port list) and comboBoxBaudrate; event handlers follow the control_event convention (e.g., buttonConnect_Click).
- Serial initialization: Form1_Load enumerates ports and defaults baud to "115200" — reuse or extend this logic when adding connection code.
- Resource usage: images are embedded via resx; modify via the Resources.resx / Project > Properties > Resources.
- Target framework: maintain .NET Framework 4.7.2 compatibility unless intentionally migrating; project file is non-SDK-style.

4) Files of interest for Copilot tasks
- SerialCommunication\Form1.cs (behavior), Form1.Designer.cs (layout), Form1.resx (embedded resources), Program.cs, SerialCommunication.csproj, App.config.

5) Existing AI/assistant config
- No CLAUDE.md, .cursorrules, AGENTS.md, .windsurfrules, CONVENTIONS.md, or other assistant rule files were found in the repository root.

If adding automated checks, tests, or linters, include simple CLI commands here (msbuild/dotnet test/dotnet format) so Copilot can suggest exact commands.

-- end of file --
