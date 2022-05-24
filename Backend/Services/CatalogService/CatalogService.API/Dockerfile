FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CatalogService.API/CatalogService.API.csproj", "CatalogService.API/"]
COPY ["CatalogService.Application/CatalogService.Application.csproj", "CatalogService.Application/"]
COPY ["CatalogService.Core/CatalogService.Core.csproj", "CatalogService.Core/"]
COPY ["CatalogService.Infrastructure/CatalogService.Infrastructure.csproj", "CatalogService.Infrastructure/"]
COPY ["CatalogService.Contracts/CatalogService.Contracts.csproj", "CatalogService.Contracts/"]
RUN dotnet restore "CatalogService.API/CatalogService.API.csproj"
COPY . .
WORKDIR "/src/CatalogService.API"
RUN dotnet build "CatalogService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatalogService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogService.API.dll"]