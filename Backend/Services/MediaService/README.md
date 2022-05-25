## HTTPS setup

```
// create pfx file
dotnet dev-certs https -ep $env:APPDATA\ASP.NET\Https\MediaService.API.pfx -p crypticpassword

// open api folder
cd Services/MediaService/MediaService.API

// generate user secret
dotnet user-secrets -p ./MediaService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
```

## Database migrations

Run this command at the root level inside ./Services/MediaService

```

dotnet ef migrations add Init --project MediaService.Infrastructure -s MediaService.API
dotnet ef database update --project MediaService.Infrastructure -s MediaService.API
```
