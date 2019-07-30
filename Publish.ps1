# Project Output Paths
$hookOutputPath = "Heroes.Controller.Hook/bin"
$XInputOutputPath = "Heroes.Controller.Hook.XInput/bin"
$PostProcessOutputPath = "Heroes.Controller.Hook.PostProcess/bin"
$publishDirectory = "Publish"

if ([System.IO.Directory]::Exists($publishDirectory)) {
	Get-ChildItem $publishDirectory -Include * -Recurse | Remove-Item -Force -Recurse
}


# Build
dotnet clean Heroes.Controller.Hook.sln
dotnet build -c Release Heroes.Controller.Hook.sln

# Cleanup
Get-ChildItem $hookOutputPath -Include *.pdb -Recurse | Remove-Item -Force -Recurse
Get-ChildItem $hookOutputPath -Include *.xml -Recurse | Remove-Item -Force -Recurse

Get-ChildItem $XInputOutputPath -Include *.pdb -Recurse | Remove-Item -Force -Recurse
Get-ChildItem $XInputOutputPath -Include *.xml -Recurse | Remove-Item -Force -Recurse

Get-ChildItem $PostProcessOutputPath -Include *.pdb -Recurse | Remove-Item -Force -Recurse
Get-ChildItem $PostProcessOutputPath -Include *.xml -Recurse | Remove-Item -Force -Recurse

# Make compressed directory
if (![System.IO.Directory]::Exists($publishDirectory)) {
    New-Item $publishDirectory -ItemType Directory
}

# Compress
Add-Type -A System.IO.Compression.FileSystem
[IO.Compression.ZipFile]::CreateFromDirectory( $hookOutputPath + '/Release', 'Publish/HeroesControllerHook.zip')
[IO.Compression.ZipFile]::CreateFromDirectory( $XInputOutputPath + '/Release', 'Publish/HeroesControllerXInput.zip')
[IO.Compression.ZipFile]::CreateFromDirectory( $PostProcessOutputPath + '/Release', 'Publish/HeroesControllerPostProcess.zip')