FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MediaService.API/MediaService.API.csproj", "MediaService.API/"]
COPY ["MediaService.Application/MediaService.Application.csproj", "MediaService.Application/"]
COPY ["MediaService.Core/MediaService.Core.csproj", "MediaService.Core/"]
COPY ["MediaService.Infrastructure/MediaService.Infrastructure.csproj", "MediaService.Infrastructure/"]
COPY ["MediaService.Contracts/MediaService.Contracts.csproj", "MediaService.Contracts/"]
RUN dotnet restore "MediaService.API/MediaService.API.csproj"
COPY . .
WORKDIR "/src/MediaService.API"
RUN dotnet build "MediaService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaService.API.dll"]