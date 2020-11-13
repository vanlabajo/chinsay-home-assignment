cd GoGreen.Api
dotnet build
Start-Process -FilePath 'dotnet' -ArgumentList 'run'
cd..
cd GoGreen.Client
Start-Process -FilePath 'npm' -ArgumentList 'start'