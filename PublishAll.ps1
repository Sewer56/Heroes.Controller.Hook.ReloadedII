# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

./Publish.ps1 -ProjectPath "Heroes.Controller.Hook.Custom/Heroes.Controller.Hook.Custom.csproj" `
              -PackageName "Heroes.Controller.Hook.Custom" `
              -PublishOutputDir "Publish/ToUpload/Custom" `
              -MetadataFileName "Heroes.Controller.Hook.Custom.ReleaseMetadata.json" `
			  -RemoveExe $False `
			  -ReadmePath README-CUSTOM.md

./Publish.ps1 -ProjectPath "Heroes.Controller.Hook.PostProcess/Heroes.Controller.Hook.PostProcess.csproj" `
              -PackageName "Heroes.Controller.Hook.PostProcess" `
              -PublishOutputDir "Publish/ToUpload/PostProcess" `
              -MetadataFileName "Heroes.Controller.Hook.PostProcess.ReleaseMetadata.json" `
			  -ReadmePath README-POSTPROCESS.md

./Publish.ps1 -ProjectPath "Heroes.Controller.Hook.XInput/Heroes.Controller.Hook.XInput.csproj" `
              -PackageName "Heroes.Controller.Hook.XInput" `
              -PublishOutputDir "Publish/ToUpload/XInput" `
              -MetadataFileName "Heroes.Controller.Hook.XInput.ReleaseMetadata.json" `
			  -ReadmePath README-XINPUT.md

./Publish.ps1 -ProjectPath "Heroes.Controller.Hook/Heroes.Controller.Hook.csproj" `
              -PackageName "Heroes.Controller.Hook" `
              -PublishOutputDir "Publish/ToUpload/Hook" `
              -MetadataFileName "Heroes.Controller.Hook.ReleaseMetadata.json" `
			  -ReadmePath README-HOOK.md

Pop-Location