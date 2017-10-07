Param(
	[switch]$pushLocal,
	[switch]$pushNuget
)

if (Test-Path -Path nuget-powershell) 
{
	rmdir nuget-powershell -Recurse
}
if (Test-Path -Path nuget-cmdline) 
{
	rmdir nuget-cmdline -Recurse
}

rm .\Source\AccidentalFish.ExpressionParser\bin\debug\*.nupkg
rm .\Source\AccidentalFish.ExpressionParser.Linq\bin\debug\*.nupkg

dotnet build

if ($pushLocal)
{
	cp .\Source\AccidentalFish.ExpressionParser\bin\debug\*.nupkg \MicroserviceAnalyticPackageRepository
	cp .\Source\AccidentalFish.ExpressionParser.Linq\bin\debug\*.nupkg \MicroserviceAnalyticPackageRepository
}

if ($pushNuget)
{
	dotnet nuget push .\Source\AccidentalFish.ExpressionParser\bin\debug\*.nupkg --source nuget.org
	dotnet nuget push .\Source\AccidentalFish.ExpressionParser.Linq\bin\debug\*.nupkg --source nuget.org
}
