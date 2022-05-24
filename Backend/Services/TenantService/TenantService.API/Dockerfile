#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/TenantService/TenantService.API/TenantService.API.csproj", "Services/TenantService/TenantService.API/"]
COPY ["Services/TenantService/TenantService.Application/TenantService.Application.csproj", "Services/TenantService/TenantService.Application/"]
COPY ["Services/TenantService/TenantService.Core/TenantService.Core.csproj", "Services/TenantService/TenantService.Core/"]
COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
COPY ["Services/TenantService/TenantService.Infrastructure/TenantService.Infrastructure.csproj", "Services/TenantService/TenantService.Infrastructure/"]
COPY ["Services/TenantService/TenantService.Contracts/TenantService.Contracts.csproj", "Services/TenantService/TenantService.Contracts/"]
RUN dotnet restore "Services/TenantService/TenantService.API/TenantService.API.csproj"
COPY . .
WORKDIR "/src/Services/TenantService/TenantService.API"
RUN dotnet build "TenantService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TenantService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TenantService.API.dll"]