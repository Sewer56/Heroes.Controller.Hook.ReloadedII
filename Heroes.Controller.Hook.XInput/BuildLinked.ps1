# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/sonicheroes.controller.hook.xinput/*" -Force -Recurse
dotnet publish "./Heroes.Controller.Hook.XInput.csproj" -c Release -o "$env:RELOADEDIIMODS/sonicheroes.controller.hook.xinput" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location