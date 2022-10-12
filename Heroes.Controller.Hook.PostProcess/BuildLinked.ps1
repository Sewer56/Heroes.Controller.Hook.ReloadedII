# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/sonicheroes.controller.hook.postprocess/*" -Force -Recurse
dotnet publish "./Heroes.Controller.Hook.PostProcess.csproj" -c Release -o "$env:RELOADEDIIMODS/sonicheroes.controller.hook.postprocess" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location