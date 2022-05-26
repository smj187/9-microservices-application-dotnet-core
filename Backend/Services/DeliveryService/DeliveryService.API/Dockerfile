#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/DeliveryService/DeliveryService.API/DeliveryService.API.csproj", "Services/DeliveryService/DeliveryService.API/"]
COPY ["Services/DeliveryService/DeliveryService.Application/DeliveryService.Application.csproj", "Services/DeliveryService/DeliveryService.Application/"]
COPY ["Services/DeliveryService/DeliveryService.Core/DeliveryService.Core.csproj", "Services/DeliveryService/DeliveryService.Core/"]
COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
COPY ["Services/DeliveryService/DeliveryService.Infrastructure/DeliveryService.Infrastructure.csproj", "Services/DeliveryService/DeliveryService.Infrastructure/"]
COPY ["Services/DeliveryService/DeliveryService.Contracts/DeliveryService.Contracts.csproj", "Services/DeliveryService/DeliveryService.Contracts/"]
RUN dotnet restore "Services/DeliveryService/DeliveryService.API/DeliveryService.API.csproj"
COPY . .
WORKDIR "/src/Services/DeliveryService/DeliveryService.API"
RUN dotnet build "DeliveryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryService.API.dll"]