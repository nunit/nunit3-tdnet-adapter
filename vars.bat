@echo off
@echo This build environment requires MSBuild 14.0
@echo To build use 'msbuild'

set PATH=%ProgramFiles(x86)%\MSBuild\14.0\bin;%PATH%
set path=%~dp0tools\NuGet;%path%
set path=%~dp0tools\WiX;%path%
