REM A batchscript to deploy the Paratext plugin to the Paratext Plugin Manager.

REM Set function variables
SET SCRIPT_ROOT=%~dp0
SET SOLUTON_ROOT=%SCRIPT_DIR%..

REM Run the publish executable
cd %SOLUTON_ROOT%\TptPublish\
dotnet clean
dotnet run