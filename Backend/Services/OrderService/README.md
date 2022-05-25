## HTTPS setup

```
// create pfx file
dotnet dev-certs https -ep $env:APPDATA\ASP.NET\Https\OrderService.API.pfx -p crypticpassword

// open api folder
cd Services/OrderService/OrderService.API

// generate user secret
dotnet user-secrets -p .\OrderService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
```

## Database migrations

Run this command at the root level inside ./Services/OrderService

```

dotnet ef migrations add Init --project OrderService.Infrastructure -s OrderService.API
dotnet ef database update --project OrderService.Infrastructure -s OrderService.API
```
