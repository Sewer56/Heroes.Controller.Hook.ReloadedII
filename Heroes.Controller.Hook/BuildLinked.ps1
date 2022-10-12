# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/sonicheroes.controller.hook/*" -Force -Recurse
dotnet publish "./Heroes.Controller.Hook.csproj" -c Release -o "$env:RELOADEDIIMODS/sonicheroes.controller.hook" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location