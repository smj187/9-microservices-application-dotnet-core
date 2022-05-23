## HTTPS setup

```
// create pfx file
dotnet dev-certs https -ep $env:APPDATA\ASP.NET\Https\DeliveryService.API.pfx -p crypticpassword

// open api folder
cd Services/DeliveryService/DeliveryService.API

// generate user secret
dotnet user-secrets -p ./DeliveryService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
```

## Docker build

Run the service container

```
docker-compose -f docker-compose.yaml up --build
docker-compose -f docker-compose.yaml down
```
