@echo off
setlocal

pushd "%~dp0"
dotnet test --logger:trx /p:CollectCoverage=true /p:CoverletOutputFormat="opencover" /p:CoverletOutput=TestResults/coverage
dotnet reportgenerator "-reports:TestResults\coverage.opencover.xml" "-targetdir:TestResults\Coverage" -reporttypes:HTML;HTMLSummary
popd