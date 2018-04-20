param($installPath, $toolsPath, $package, $project)
[System.Reflection.Assembly]::LoadFile("$($installPath)\lib\net35\System.Data.CData.Sage50UK.dll")
[System.Data.CData.Sage50UK.Nuget]::CheckNugetLicense("nuget")