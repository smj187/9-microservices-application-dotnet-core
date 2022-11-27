#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY ["backend/Services/CatalogService/CatalogService.API/Certificates/", "/https/"]

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY ["backend/BuildingBlocks/BuildingBlocks.Attributes/BuildingBlocks.Attributes.csproj", "BuildingBlocks/BuildingBlocks.Attributes/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Authentication/BuildingBlocks.Authentication.csproj", "BuildingBlocks/BuildingBlocks.Authentication/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Cache/BuildingBlocks.Cache.csproj", "BuildingBlocks/BuildingBlocks.Cache/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.EfCore/BuildingBlocks.EfCore.csproj", "BuildingBlocks/BuildingBlocks.EfCore/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Exceptions/BuildingBlocks.Exceptions.csproj", "BuildingBlocks/BuildingBlocks.Exceptions/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Extensions/BuildingBlocks.Extensions.csproj", "BuildingBlocks/BuildingBlocks.Extensions/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Filters/BuildingBlocks.Filters.csproj", "BuildingBlocks/BuildingBlocks.Filters/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Masstransit/BuildingBlocks.Masstransit.csproj", "BuildingBlocks/BuildingBlocks.Masstransit/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Middleware/BuildingBlocks.Middleware.csproj", "BuildingBlocks/BuildingBlocks.Middleware/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Mongo/BuildingBlocks.Mongo.csproj", "BuildingBlocks/BuildingBlocks.Mongo/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Multitenancy/BuildingBlocks.Multitenancy.csproj", "BuildingBlocks/BuildingBlocks.Multitenancy/"]
COPY ["backend/BuildingBlocks/BuildingBlocks.Redis/BuildingBlocks.Redis.csproj", "BuildingBlocks/BuildingBlocks.Redis/"]


COPY ["backend/Services/CatalogService/CatalogService.API/CatalogService.API.csproj", "backend/Services/CatalogService/CatalogService.API/"]
COPY ["backend/Services/CatalogService/CatalogService.Application/CatalogService.Application.csproj", "backend/Services/CatalogService/CatalogService.Application/"]
COPY ["backend/Services/CatalogService/CatalogService.Core/CatalogService.Core.csproj", "backend/Services/CatalogService/CatalogService.Core/"]
COPY ["backend/Services/CatalogService/CatalogService.Infrastructure/CatalogService.Infrastructure.csproj", "backend/Services/CatalogService/CatalogService.Infrastructure/"]
COPY ["backend/Services/CatalogService/CatalogService.Contracts/CatalogService.Contracts.csproj", "backend/Services/CatalogService/CatalogService.Contracts/"]

RUN dotnet restore "backend/Services/CatalogService/CatalogService.API/CatalogService.API.csproj"
COPY . .
WORKDIR "/src/backend/Services/CatalogService/CatalogService.API"
RUN dotnet build "CatalogService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatalogService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogService.API.dll"]