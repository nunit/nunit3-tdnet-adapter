@echo off
@echo This build environment requires .NET Framework v4.0
@echo To build use 'msbuild'

set path=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319;%path%
set path=%~dp0tools\NuGet;%path%
set path=%~dp0tools\WiX;%path%
