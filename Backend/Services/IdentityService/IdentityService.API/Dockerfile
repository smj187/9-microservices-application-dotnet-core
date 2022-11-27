#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY ["backend/Services/IdentityService/IdentityService.API/Certificates/", "/https/"]

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY ["backend/BuildingBlocks/BuildingBlocks.Attributes/BuildingBlocks.Attributes.csproj", "BuildingBlocks/BuildingBlocks.Attributes/"]
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


COPY ["backend/Services/IdentityService/IdentityService.API/IdentityService.API.csproj", "backend/Services/IdentityService/IdentityService.API/"]
COPY ["backend/Services/IdentityService/IdentityService.Application/IdentityService.Application.csproj", "backend/Services/IdentityService/IdentityService.Application/"]
COPY ["backend/Services/IdentityService/IdentityService.Core/IdentityService.Core.csproj", "backend/Services/IdentityService/IdentityService.Core/"]
COPY ["backend/Services/IdentityService/IdentityService.Infrastructure/IdentityService.Infrastructure.csproj", "backend/Services/IdentityService/IdentityService.Infrastructure/"]
COPY ["backend/Services/IdentityService/IdentityService.Contracts/IdentityService.Contracts.csproj", "backend/Services/IdentityService/IdentityService.Contracts/"]

RUN dotnet restore "backend/Services/IdentityService/IdentityService.API/IdentityService.API.csproj"
COPY . .
WORKDIR "/src/backend/Services/IdentityService/IdentityService.API"
RUN dotnet build "IdentityService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.API.dll"]