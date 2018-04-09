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

Set config=release 
set log=builder.log
set msbuildpath=C:\Windows\Microsoft.NET\Framework\v4.0.30319
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
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /rebuild %config% "%BATDIR%\Solutions\Entities\Entities.sln" >> %log%
@echo "********** Building Interface **********" >> %log%
@echo ********** Building Interface **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /rebuild %config% "%BATDIR%\Solutions\Interfaces\Interfaces.sln" >> %log%
@echo "********** Building Application **********" >> %log%
@echo ********** Building Application **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /rebuild %config% "%BATDIR%\Solutions\Application\Application.sln" >> %log%
@echo "********** Building DataAccess **********" >> %log%
@echo ********** Building DataAccess **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /rebuild %config% "%BATDIR%\Solutions\DataAccess\DataAccess.sln" >> %log%
@echo "********** Building Businessprocess **********" >> %log%
@echo ********** Building Businessprocess **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /rebuild %config% "%BATDIR%\Solutions\BusinessProcess\BusinessProcess.sln" >> %log%
echo "********** Building IQCare.Library **********" >> %log%
@echo ********** Building IQCare.Library  **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /rebuild %config% "%BATDIR%\Solutions\IQCare.Library\IQCare.Library.sln" >> %log%
echo "********** Building IQCare.Lookup **********" >> %log%
@echo ********** Building IQCare.Lookup **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /rebuild %config% "%BATDIR%\Solutions\IQLookup\IQCare.Lookup.sln" >> %log%
echo "********** Building Presentation **********" >> %log%
@echo ********** Building Presentation **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /build %config% "%BATDIR%\Solutions\Presentation\Presentation.sln" >> %log%
echo "********** Building Billing **********" >> %log%
@echo ********** Building Billing **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /build %config% "%BATDIR%\Solutions\Billing\IQCare.Billing.sln" >> %log%
echo "********** Building IQCare.CCC **********" >> %log%
@echo ********** Building IQCare.CCC **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /build %config% "%BATDIR%\Solutions\IQCare.CCC\IQCare.CCC.sln" >> %log%
@echo ********** Building IQCare.Web.Api **********
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe" "%BATDIR%\Solutions\IQCare.Web.API\IQCare.Web.API.sln" >> %log%
if /I %config%== release (
echo "********** Building IQCare Service **********" >> %log%
@echo ********** Building IQCare Service **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /build %config% "%BATDIR%\Solutions\IQCareService\IQCareService.sln" >> %log%
echo "********** Building IQCare Management **********" >> %log%
@echo ********** Building IQCare Management **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /build %config% "%BATDIR%\Solutions\IQCare Management\IQCare Management.sln" >> %log%
echo "********** Building IQCare.Release **********" >> %log%
@echo ********** Building IQCare.Release **********
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" /build %config% "%BATDIR%\Solutions\IQCare.Release\IQCare.Release.sln" >> %log%
)
@echo "********** Completed building  devenv**********" >> %log%
@echo ********** Completed building  devenv**********
echo "********** Scripts **********" >> %log%
XCOPY %BATDIR%\Scripts\* %BATDIR%\Release\Scripts /s /i >nul
COPY %BATDIR%\batch.bat %BATDIR%\Release\batch.bat /Y > nul

@echo ********** END OF BUILINDING & PACKAGING **********

echo


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

pause


