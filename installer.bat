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
REM xcopy /d %BATDIR%\Library\Microsoft.Web.Infrastructure.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\MSDATASRC.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\netchartdir.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\Newtonsoft.Json.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\Sand.Security.Cryptography.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\stdole.dll %BATDIR%package\web\bin
xcopy /d %BATDIR%\Library\System.Net.Http.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Net.Http.Formatting.dll %BATDIR%\package\web\bin
REM xcopy /d %BATDIR%\Library\System.Web.Helpers.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Web.Http.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\Library\System.Web.Http.WebHost.dll %BATDIR%\package\web\bin
REM xcopy /d %BATDIR%\Library\System.Web.Mvc.dll %BATDIR%\package\web\bin
REM xcopy /d %BATDIR%\Library\System.Web.Razor.dll %BATDIR%\package\web\bin
REM xcopy /d %BATDIR%\Library\System.Web.WebPages.Deployment.dll %BATDIR%\package\web\bin
REM xcopy /d %BATDIR%\Library\System.Web.WebPages.dll %BATDIR%\package\web\bin
REM xcopy /d %BATDIR%\Library\System.Web.WebPages.Razor.dll %BATDIR%\package\web\bin
xcopy /d %BATDIR%\References\Telerik.Web.UI.dll %BATDIR%\package\web\bin

pause