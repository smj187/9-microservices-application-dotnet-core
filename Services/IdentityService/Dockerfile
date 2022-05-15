FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

COPY ./IdentityService.Core/IdentityService.Core.csproj ./IdentityService.Core/
COPY ./IdentityService.Infrastructure/IdentityService.Infrastructure.csproj ./IdentityService.Infrastructure/
COPY ./IdentityService.Application/IdentityService.Application.csproj ./IdentityService.Application/
COPY ./IdentityService.API/IdentityService.API.csproj ./IdentityService.API/

RUN dotnet restore ./IdentityService.API/IdentityService.API.csproj

COPY . .

WORKDIR /src
RUN dotnet build ./IdentityService.API/IdentityService.API.csproj -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "IdentityService.API/IdentityService.API.csproj" -c Release -o /app/publish \
   -r alpine-x64 \
   --self-contained true \
   -p:PublishTrimmed=true \
   -p:PublishSingleFile=true


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./IdentityService.API"]


