rm -rf ./publish
dotnet pack ../Argument.Net/Argument.Net.csproj --output ../nuget/publish --include-symbols --include-source --configuration Release 