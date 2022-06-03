#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/FileService/FileService.API/FileService.API.csproj", "Services/FileService/FileService.API/"]
COPY ["Services/FileService/FileService.Application/FileService.Application.csproj", "Services/FileService/FileService.Application/"]
COPY ["Services/FileService/FileService.Core/FileService.Core.csproj", "Services/FileService/FileService.Core/"]
COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
COPY ["Services/FileService/FileService.Infrastructure/FileService.Infrastructure.csproj", "Services/FileService/FileService.Infrastructure/"]
COPY ["Services/FileService/FileService.Contracts/FileService.Contracts.csproj", "Services/FileService/FileService.Contracts/"]
RUN dotnet restore "Services/FileService/FileService.API/FileService.API.csproj"
COPY . .
WORKDIR "/src/Services/FileService/FileService.API"
RUN dotnet build "FileService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileService.API.dll"]