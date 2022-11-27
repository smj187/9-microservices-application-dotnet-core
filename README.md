# Work in progress

This repo is work in progress

```
// https setup
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\BasketService.pfx -p crypticpassword
dotnet user-secrets -p ./Services/BasketService/BasketService.API/BasketService.API.csproj set "Kestrel:Certificates:Production:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose-basket.yaml up --build
```

```
dotnet dev-certs https --clean
dotnet dev-certs https -ep backend/Services/CatalogService/CatalogService.API/Certificates/CatalogService.pfx -p crypticpassword
dotnet dev-certs https --trust

dotnet dev-certs https -ep ./backend/Gateways/Public/https/certificate.crt -p crypticpassword --trust --format PEM

dotnet dev-certs https -ep ./https/server.crt -p crypticpassword --trust --format PEM

dotnet dev-certs https -ep certificate.crt -p crypticpassword --trust --format PEM



mkcert -key-file key.pem -cert-file cert.pem localhost

```

https://github.com/envoyproxy/envoy/issues/5892

https://www.envoyproxy.io/docs/envoy/latest/api-v3/extensions/transport_sockets/tls/v3/common.proto#extensions-transport-sockets-tls-v3-tlsparameters

https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-dev-certs
