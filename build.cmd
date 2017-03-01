REM build/publish scripts
REM Prerequirement:
REM   nuget setApiKey <APIKEY> -source https://www.nuget.org/api/v2/package
REM Usage: see help label
@echo off
cls
set BASE_DIR=%CD%
set NUGET_SERVER=https://www.nuget.org/api/v2/package
set DIST_DIR=%BASE_DIR%\dist

if "%1" == "build" goto build
if "%1" == "rebuild" goto rebuild
if "%1" == "doc" goto rebuild
if "%1" == "pack" goto pack
if "%1" == "push" goto push
goto help

:build
msbuild /p:Configuration=Release /p:Platform="Any CPU" /t:Build Shuhari.Framework.sln
goto end

:rebuild
msbuild /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild Shuhari.Framework.sln
if "%1" == "doc" (
    goto doc
) else (
    goto end
)

:doc
msbuild /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild Shuhari.Framework.shfbproj
goto end

:pack
if not exist %DIST_DIR% mkdir %DIST_DIR%
del /Q %DIST_DIR%\*.nupkg
set PACK_ARG=-properties Configuration=Release;Platform=AnyCPU -outputdirectory %DIST_DIR%
nuget pack Shuhari.Framework.Common\Shuhari.Framework.Common.csproj %PACK_ARG%
nuget pack Shuhari.Framework.Web.Mvc\Shuhari.Framework.Web.Mvc.csproj %PACK_ARG%
nuget pack Shuhari.Framework.Testing.Common\Shuhari.Framework.Testing.Common.csproj %PACK_ARG%
nuget pack Shuhari.Framework.Testing.Mvc\Shuhari.Framework.Testing.Mvc.csproj %PACK_ARG%
nuget pack Shuhari.Framework.Wpf\Shuhari.Framework.Wpf.csproj %PACK_ARG%
goto end

:push
nuget push %DIST_DIR%\Shuhari.Framework.Common.*.nupkg -source %NUGET_SERVER%
nuget push %DIST_DIR%\Shuhari.Framework.Web.Mvc.*.nupkg -source %NUGET_SERVER%
nuget push %DIST_DIR%\Shuhari.Framework.Testing.Common.*.nupkg -source %NUGET_SERVER%
nuget push %DIST_DIR%\Shuhari.Framework.Testing.Mvc.*.nupkg -source %NUGET_SERVER%
nuget push %DIST_DIR%\Shuhari.Framework.Wpf.*.nupkg -source %NUGET_SERVER%
goto end

:help
echo build.cmd build - Build project
echo build.cmd rebuild - Build project
echo build.cmd doc - Build documentation
echo build.cmd pack - Package .nupkg file
echo build.cmd push - Push .nupkg to nuget server
echo build.cmd help - Show help
goto end

:end
cd %SAVE_DIR%
