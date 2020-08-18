$projectPath = "D:\Core Check\NetCoreCheckDemo"

Set-Location -Path $projectPath

dotnet restore .\NetCoreCheckDemo\NetCoreCheckDemo.csproj

dotnet build --no-restore

#dotnet run  --project .\NetCoreCheckDemo\NetCoreCheckDemo.csproj  --no-restore /MySetting:SomeValue=123

dotnet publish -c Release

dotnet publish -c Release -r linux-x64 --self-contained false

#dotnet publish -c Release -r win-x86 --self-contained true