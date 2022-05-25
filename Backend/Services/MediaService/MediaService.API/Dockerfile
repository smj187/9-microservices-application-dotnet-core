#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/MediaService/MediaService.API/MediaService.API.csproj", "Services/MediaService/MediaService.API/"]
COPY ["Services/MediaService/MediaService.Contracts/MediaService.Contracts.csproj", "Services/MediaService/MediaService.Contracts/"]
COPY ["Services/MediaService/MediaService.Infrastructure/MediaService.Infrastructure.csproj", "Services/MediaService/MediaService.Infrastructure/"]
COPY ["Services/MediaService/MediaService.Core/MediaService.Core.csproj", "Services/MediaService/MediaService.Core/"]
COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
COPY ["Services/MediaService/MediaService.Application/MediaService.Application.csproj", "Services/MediaService/MediaService.Application/"]
RUN dotnet restore "Services/MediaService/MediaService.API/MediaService.API.csproj"
COPY . .
WORKDIR "/src/Services/MediaService/MediaService.API"
RUN dotnet build "MediaService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaService.API.dll"]