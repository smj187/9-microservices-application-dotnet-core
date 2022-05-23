FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeliveryService.API/DeliveryService.API.csproj", "DeliveryService.API/"]
COPY ["DeliveryService.Application/DeliveryService.Application.csproj", "DeliveryService.Application/"]
COPY ["DeliveryService.Core/DeliveryService.Core.csproj", "DeliveryService.Core/"]
COPY ["DeliveryService.Infrastructure/DeliveryService.Infrastructure.csproj", "DeliveryService.Infrastructure/"]
COPY ["DeliveryService.Contracts/DeliveryService.Contracts.csproj", "DeliveryService.Contracts/"]
RUN dotnet restore "DeliveryService.API/DeliveryService.API.csproj"
COPY . .
WORKDIR "/src/DeliveryService.API"
RUN dotnet build "DeliveryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryService.API.dll"]