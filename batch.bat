@echo off
setlocal EnableDelayedExpansion
rem this is a build scripts
FOR /F %%I IN ("%0") DO SET BATDIR=%%~dpI
CD /D %BATDIR%
echo %BATDIR%
Set /p Server= Enter Server name e.g \sqlexpress : || Set Server=.\SQLExpress 
Set /p Username= Enter username with permission to create/drop db || Set Username=sa
Set /p Password= Enter the password  || Set Password=12345
if %Password%==NothingPut goto sub_pass
Set /p Database= Enter the database Name  || Set Database=IQCare_LVCT_356
if %Database%==NoDB goto sub_db

set log=Installer.log

Set f731="%BATDIR%\MoH731_v0.xsl"
Set f711="%BATDIR%\MoH711_v0.xsl"

echo "Starting executing the script" > %log%
For /R Scripts\ %%G IN (*.sql) do (
	@echo "********** Start Executing file =====> %%G **********" >> %log%
	Sqlcmd -S %Server% -d %Database%  -U %Username% -P %Password%  -i "%%G" >> %log%
	@echo "******* Finished Executing file %%G *******"
 )
@echo "****** updating iqtools 731, 711 IQcare xml templates*****
Sqlcmd -S %Server% -d %Database%  -U %Username% -P %Password% -v var731=%f731% var711=%f711% -i "Scripts\Iqtool.sql.txt" >> %log%
echo "Finished executing the script" >> %log%
  
 
goto eof

:CANCELED
 echo The process has been cancelled
goto eof

:sub_pass
echo No password to connect to SQL Server was provided
goto eof

:sub_db
echo Database was not provided
goto eof

:eof
pause
cd
