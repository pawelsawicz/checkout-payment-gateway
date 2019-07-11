FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

COPY src/API/bin/Release/netcoreapp2.2/publish app/

ENTRYPOINT ["dotnet", "app/API.dll"]