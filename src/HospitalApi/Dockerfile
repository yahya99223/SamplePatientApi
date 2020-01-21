FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY src/HospitalApi/HospitalApi.csproj src/HospitalApi/
COPY src/API.Models/API.Models.csproj src/API.Models/
COPY src/Shared.IOC/Shared.IOC.csproj src/Shared.IOC/
COPY src/Service.Implementation/Service.Implementation.csproj src/Service.Implementation/
COPY src/Service.Contracts/Service.Contracts.csproj src/Service.Contracts/
COPY src/Shared.Core/Shared.Core.csproj src/Shared.Core/

RUN dotnet restore src/HospitalApi/HospitalApi.csproj
COPY . .
WORKDIR /src
RUN dotnet build src/HospitalApi/HospitalApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish src/HospitalApi/HospitalApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HospitalApi.dll"]