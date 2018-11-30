@echo off
set IS_ELEVATED=0
setlocal EnableDelayedExpansion

net session >nul 2>&1
if %errorLevel% == 0 (
	rem echo Success: Administrative permissions confirmed.
) else (
	echo Failure: Current permissions inadequate. Run as administrator
	pause
	exit /b 1
)

FOR /F %%I IN ("%0") DO SET BATDIR=%%~dpI
CD /D %BATDIR%
@echo %BATDIR%

Set config=debug
set log=builder.log
set msbuildpath=C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe
@Echo Cleaning references folder
rmdir %BATDIR%\References /S /Q
mkdir References
@Echo Cleaning package folder
rmdir %BATDIR%\Package /S /Q
mkdir package
@Echo Cleaning Release folder
rmdir %BATDIR%\Release /S /Q
mkdir Release

@echo "Starting building in %config% mode"
@echo "Starting building in %config% mode devenv" > %log%

@echo "********** Building Entities **********" >> %log%
@echo ********** Building Entities **********
"%msbuildpath%" "%BATDIR%\Solutions\Entities\Entities.sln" /t:rebuild /p:Configuration=%config%  >> %log%
@echo "********** Building Interface **********" >> %log%
@echo ********** Building Interface **********
"%msbuildpath%" "%BATDIR%\Solutions\Interfaces\Interfaces.sln" /t:rebuild /p:Configuration=%config%  >> %log%
@echo "********** Building Application **********" >> %log%
@echo ********** Building Application **********
"%msbuildpath%" "%BATDIR%\Solutions\Application\Application.sln" /t:rebuild /p:Configuration=%config%  >> %log%
@echo "********** Building DataAccess **********" >> %log%
@echo ********** Building DataAccess **********
"%msbuildpath%" "%BATDIR%\Solutions\DataAccess\DataAccess.sln" /t:rebuild /p:Configuration=%config%  >> %log%
@echo "********** Building Businessprocess **********" >> %log%
@echo ********** Building Businessprocess **********
"%msbuildpath%" "%BATDIR%\Solutions\BusinessProcess\BusinessProcess.sln" /t:rebuild /p:Configuration=%config%  >> %log%
echo "********** Building IQCare.Library **********" >> %log%
@echo ********** Building IQCare.Library  **********
"%msbuildpath%" "%BATDIR%\Solutions\IQCare.Library\IQCare.Library.sln" /t:rebuild /p:Configuration=%config%  >> %log%
echo "********** Building IQCare.Lookup **********" >> %log%
@echo ********** Building IQCare.Lookup **********
"%msbuildpath%" "%BATDIR%\Solutions\IQLookup\IQCare.Lookup.sln" /t:rebuild /p:Configuration=%config%  >> %log%
echo "********** Building Presentation **********" >> %log%
@echo ********** Building Presentation **********
"%msbuildpath%" "%BATDIR%\Solutions\Presentation\Presentation.sln" /t:rebuild /p:Configuration=%config%  >> %log%
echo "********** Building Billing **********" >> %log%
@echo ********** Building Billing **********
"%msbuildpath%" "%BATDIR%\Solutions\Billing\IQCare.Billing.sln" /t:rebuild /p:Configuration=%config%  >> %log%
echo "********** Building IQCare.CCC **********" >> %log%
@echo ********** Building IQCare.CCC **********
"%msbuildpath%" "%BATDIR%\Solutions\IQCare.CCC\IQCare.CCC.sln" /t:rebuild /p:Configuration=%config%  >> %log%
@echo ********** Building IQCare.PSmart **********
"%msbuildpath%" "%BATDIR%\Solutions\IQCare.PSmart\IQCare.PSmart.sln" /t:rebuild /p:Configuration=%config%  >> %log%
@echo ********** Building IQCare.Web.Api **********
"%msbuildpath%" "%BATDIR%\Solutions\IQCare.Web.API\IQCare.Web.API.sln" /t:rebuild /p:Configuration=%config% >> %log%
if /I %config%== release (
echo "********** Building IQCare Service **********" >> %log%
@echo ********** Building IQCare Service **********
"%msbuildpath%" "%BATDIR%\Solutions\IQCareService\IQCareService.sln" /t:rebuild /p:Configuration=%config%  >> %log%
echo "********** Building IQCare Management **********" >> %log%
@echo ********** Building IQCare Management **********
"%msbuildpath%" "%BATDIR%\Solutions\IQCare Management\IQCare Management.sln" /t:rebuild /p:Configuration=%config%  >> %log%
echo "********** Building IQCare.Release **********" >> %log%
@echo ********** Building IQCare.Release **********
"%msbuildpath%" "%BATDIR%\Solutions\IQCare.Release\IQCare.Release.sln" /t:rebuild /p:Configuration=%config%  >> %log%
)
@echo "********** Completed building  devenv**********" >> %log%
@echo ********** Completed building  devenv**********
echo "********** Scripts **********" >> %log%
XCOPY %BATDIR%\Scripts\* %BATDIR%\Release\Scripts /s /i >nul
COPY %BATDIR%\batch.bat %BATDIR%\Release\batch.bat /Y > nul

@echo ********** END OF BUILINDING & PACKAGING **********

echo


xcopy /d %BATDIR%\References\ActiveDatabaseSoftware.ActiveQueryBuilder2.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%References\ActiveDatabaseSoftware.MSSQLMetadataProvider2.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\ActiveDatabaseSoftware.OLEDBMetadataProvider2.dll %BATDIR%package\web\bin
xcopy /d %BATDIR%\References\ActiveDatabaseSoftware.UniversalMetadataProvider2.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\ADODB.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\Ajax.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\AjaxControlToolkit.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\AutoMapper.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\CrystalDecisions.CrystalReports.Engine.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\CrystalDecisions.ReportSource.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\CrystalDecisions.Shared.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\CrystalDecisions.Web.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\itextsharp.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\log4net.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\Microsoft.Office.Interop.Owc11.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\Microsoft.Web.Infrastructure.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\MSDATASRC.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\netchartdir.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\Newtonsoft.Json.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\Sand.Security.Cryptography.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\stdole.dll %BATDIR%package\web\bin
xcopy /d %BATDIR%\Library\System.Net.Http.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Net.Http.Formatting.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Web.Helpers.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Web.Http.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Web.Http.WebHost.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Web.Mvc.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Web.Razor.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Web.WebPages.Deployment.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Web.WebPages.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Web.WebPages.Razor.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\Telerik.Web.UI.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\EntityFramework.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\EntityFramework.SqlServer.dll %BATDIR%\package\web\bin

@echo ********** END OF COPYING DLLS **********

@echo "********** Building IQCare API **********" >> %log%
@echo ********** Building IQCare API **********

dotnet publish Solutions/IQCare.Core/IQCare/IQCare.csproj -o ../../../package/api

@echo ********** END OF BUILINDING IQCare API ********** >> %log%
@echo ********** END OF BUILINDING IQCare API ********** >> %log%


@echo "********** Building IQCare LAB **********" >> %log%
@echo ********** Building IQCare LAB **********

dotnet publish Solutions/IQCare.Core/IQCare.Lab.WebApi/IQCare.Lab.WebApi.csproj -o ../../../package/lab

@echo ********** END OF BUILINDING IQCare LAB ********** >> %log%
@echo ********** END OF BUILINDING IQCare LAB ********** >> %log%



@echo "********** Building IQCare MATERNITY **********" >> %log%
@echo ********** Building IQCare MATERNITY **********

dotnet publish Solutions/IQCare.Core/IQCare.Maternity.WebApi/IQCare.Maternity.WebApi.csproj -o ../../../package/maternity

@echo ********** END OF BUILINDING IQCare MATERNITY ********** >> %log%
@echo ********** END OF BUILINDING IQCare MATERNITY ********** >> %log%

cd Solutions/IQCare.Core/IQCare

ng build --base-href "/frontend/" --prod

pause

