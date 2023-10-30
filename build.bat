@echo off
echo Compilation in progress.

echo.
echo Compilling [ Linux x86_64 ] 
dotnet publish -r linux-x64 -c Release --self-contained
echo Compilling [ Windows x86_64 ]
dotnet publish -r win-x64 -c Release --self-contained

echo Finished!
