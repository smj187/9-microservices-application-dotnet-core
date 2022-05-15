FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

COPY ./CatalogService.Core/CatalogService.Core.csproj ./CatalogService.Core/
COPY ./CatalogService.Infrastructure/CatalogService.Infrastructure.csproj ./CatalogService.Infrastructure/
COPY ./CatalogService.Application/CatalogService.Application.csproj ./CatalogService.Application/
COPY ./CatalogService.API/CatalogService.API.csproj ./CatalogService.API/

RUN dotnet restore ./CatalogService.API/CatalogService.API.csproj

COPY . .

WORKDIR /src
RUN dotnet build ./CatalogService.API/CatalogService.API.csproj -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "CatalogService.API/CatalogService.API.csproj" -c Release -o /app/publish \
   -r alpine-x64 \
   --self-contained true \
   -p:PublishTrimmed=true \
   -p:PublishSingleFile=true


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./CatalogService.API"]


