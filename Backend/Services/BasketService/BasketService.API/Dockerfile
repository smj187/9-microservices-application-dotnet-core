#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/BasketService/BasketService.API/BasketService.API.csproj", "Services/BasketService/BasketService.API/"]
COPY ["Services/BasketService/BasketService.Application/BasketService.Application.csproj", "Services/BasketService/BasketService.Application/"]
COPY ["Services/BasketService/BasketService.Core/BasketService.Core.csproj", "Services/BasketService/BasketService.Core/"]
COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
COPY ["Services/BasketService/BasketService.Infrastructure/BasketService.Infrastructure.csproj", "Services/BasketService/BasketService.Infrastructure/"]
COPY ["Services/BasketService/BasketService.Contracts/BasketService.Contracts.csproj", "Services/BasketService/BasketService.Contracts/"]
RUN dotnet restore "Services/BasketService/BasketService.API/BasketService.API.csproj"
COPY . .
WORKDIR "/src/Services/BasketService/BasketService.API"
RUN dotnet build "BasketService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BasketService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasketService.API.dll"]