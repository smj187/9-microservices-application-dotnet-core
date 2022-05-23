#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/CatalogService/CatalogService.API/CatalogService.API.csproj", "Services/CatalogService/CatalogService.API/"]
COPY ["Services/CatalogService/CatalogService.Application/CatalogService.Application.csproj", "Services/CatalogService/CatalogService.Application/"]
COPY ["Services/CatalogService/CatalogService.Core/CatalogService.Core.csproj", "Services/CatalogService/CatalogService.Core/"]
COPY ["Services/CatalogService/CatalogService.Infrastructure/CatalogService.Infrastructure.csproj", "Services/CatalogService/CatalogService.Infrastructure/"]
RUN dotnet restore "Services/CatalogService/CatalogService.API/CatalogService.API.csproj"
COPY . .
WORKDIR "/src/Services/CatalogService/CatalogService.API"
RUN dotnet build "CatalogService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatalogService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogService.API.dll"]