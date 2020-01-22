FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY src/HospitalApi/HospitalApi.csproj src/HospitalApi/

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