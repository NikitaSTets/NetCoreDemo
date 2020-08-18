

dotnet restore .\NetCoreCheckDemo\NetCoreCheckDemo\NetCoreCheckDemo.csproj

dotnet build --no-restore

dotnet run  --project .\NetCoreCheckDemo\NetCoreCheckDemo\NetCoreCheckDemo.csproj  --no-restore

dotnet publish -c Release

dotnet publish -c Release -r linux-x64 --self-contained false

#dotnet publish -c Release -r win-x86 --self-contained true