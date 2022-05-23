FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TenantService.API/TenantService.API.csproj", "TenantService.API/"]
COPY ["TenantService.Application/TenantService.Application.csproj", "TenantService.Application/"]
COPY ["TenantService.Core/TenantService.Core.csproj", "TenantService.Core/"]
COPY ["TenantService.Infrastructure/TenantService.Infrastructure.csproj", "TenantService.Infrastructure/"]
COPY ["TenantService.Contracts/TenantService.Contracts.csproj", "TenantService.Contracts/"]
RUN dotnet restore "TenantService.API/TenantService.API.csproj"
COPY . .
WORKDIR "/src/TenantService.API"
RUN dotnet build "TenantService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TenantService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TenantService.API.dll"]