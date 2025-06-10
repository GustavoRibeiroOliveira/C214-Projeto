//////////////////////////////////////////////////////////////////////
// CONFIGURAÇÕES
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var solutionFile = "./UserValidationProject.sln";
var testProject = "./UserValidationTests/UserValidationTests.csproj";
var testResultsDir = "./TestResults";
var coverageReportDir = $"{testResultsDir}/CoverageReport";

//////////////////////////////////////////////////////////////////////
// TAREFAS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectories("**/bin");
    CleanDirectories("**/obj");
    CleanDirectories(testResultsDir);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetRestore(solutionFile);
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    DotNetBuild(solutionFile, new DotNetBuildSettings {
        Configuration = "Release"
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetTest(testProject, new DotNetTestSettings
    {
        Configuration = "Release",
        NoBuild = true,
        ArgumentCustomization = args => args
            .Append("--results-directory")
            .Append(testResultsDir)
            .Append("--logger:trx;LogFileName=results.trx")
            .Append("--collect")
            .Append("\"XPlat Code Coverage\"")
    });
});

Task("Generate-Coverage-Report")
    .IsDependentOn("Test")
    .Does(() =>
{
    var coberturaReports = GetFiles($"{testResultsDir}/**/coverage.cobertura.xml");
    var trxReportPath = $"{testResultsDir}/results.trx";

    if (!coberturaReports.Any())
    {
        Information("Arquivo de cobertura não encontrado.");
        return;
    }

    if (!FileExists(trxReportPath))
    {
        Information("Arquivo .trx não encontrado.");
        return;
    }

    var reportFiles = coberturaReports.Select(r => r.ToString()).ToList();
    reportFiles.Add(trxReportPath);
    var reportPathsArg = string.Join(";", reportFiles);

    Information("Gerando relatório com cobertura e resumo dos testes...");

    StartProcess("reportgenerator", new ProcessSettings {
        Arguments = new ProcessArgumentBuilder()
            .Append($"-reports:{reportPathsArg}")
            .Append($"-targetdir:{coverageReportDir}")
            .Append("-reporttypes:Html;HtmlSummary")
    });

    Information($"Relatório HTML de cobertura gerado em: {coverageReportDir}");
});

Task("Generate-Test-Report")
    .IsDependentOn("Test")
    .Does(() =>
{
    var trxPath = $"{testResultsDir}/results.trx";
    var outputHtml = $"{testResultsDir}/TestReport.html";
    var converterProject = "./TrxToHtml/TrxToHtml.csproj";
    var dllPath = "./TrxToHtml/bin/Release/net9.0/TrxToHtml.dll";

    // Compilar o projeto se necessário
    if (!FileExists(dllPath))
    {
        Information("Compilando projeto TrxToHtml...");
        StartProcess("dotnet", new ProcessSettings
        {
            Arguments = $"build \"{converterProject}\" -c Release"
        });
    }

    // Rodar conversor
    if (FileExists(trxPath))
    {
        StartProcess("dotnet", new ProcessSettings {
            Arguments = $"\"{dllPath}\" \"{trxPath}\" \"{outputHtml}\""
        });

        Information($"Relatório de testes gerado em: {outputHtml}");
    }
    else
    {
        Warning("Arquivo .trx não encontrado. Relatório de testes não gerado.");
    }
});

Task("Default")
    .IsDependentOn("Generate-Coverage-Report")
    .IsDependentOn("Generate-Test-Report");

//////////////////////////////////////////////////////////////////////
// EXECUTAR
//////////////////////////////////////////////////////////////////////

RunTarget(target);
