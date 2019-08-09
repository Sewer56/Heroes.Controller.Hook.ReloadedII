# Project Output Paths
$hookOutputPath        = "Heroes.Controller.Hook/bin"
$xInputOutputPath      = "Heroes.Controller.Hook.XInput/bin"
$postProcessOutputPath = "Heroes.Controller.Hook.PostProcess/bin"

$hookPublishName       	= "HeroesControllerHook.zip"
$xinputPublishName      = "HeroesControllerXInput.zip"
$postProcessPublishName = "HeroesControllerPostProcess.zip"

$solutionName = "Heroes.Controller.Hook.sln"
$publishDirectory = "Publish"

if ([System.IO.Directory]::Exists($publishDirectory)) {
	Get-ChildItem $publishDirectory -Include * -Recurse | Remove-Item -Force -Recurse
}

# Build
dotnet restore $solutionName
dotnet clean $solutionName
dotnet build -c Release $solutionName

# Cleanup
Get-ChildItem $hookOutputPath -Include *.pdb -Recurse | Remove-Item -Force -Recurse
Get-ChildItem $hookOutputPath -Include *.xml -Recurse | Remove-Item -Force -Recurse

Get-ChildItem $xInputOutputPath -Include *.pdb -Recurse | Remove-Item -Force -Recurse
Get-ChildItem $xInputOutputPath -Include *.xml -Recurse | Remove-Item -Force -Recurse

Get-ChildItem $postProcessOutputPath -Include *.pdb -Recurse | Remove-Item -Force -Recurse
Get-ChildItem $postProcessOutputPath -Include *.xml -Recurse | Remove-Item -Force -Recurse

# Make compressed directory
if (![System.IO.Directory]::Exists($publishDirectory)) {
    New-Item $publishDirectory -ItemType Directory
}

# Compress
Add-Type -A System.IO.Compression.FileSystem
[IO.Compression.ZipFile]::CreateFromDirectory( $hookOutputPath + '/Release', 'Publish/' + $hookPublishName)
[IO.Compression.ZipFile]::CreateFromDirectory( $xInputOutputPath + '/Release', 'Publish/' + $xinputPublishName)
[IO.Compression.ZipFile]::CreateFromDirectory( $postProcessOutputPath + '/Release', 'Publish/' + $postProcessPublishName)