## HTTPS setup

```
// create pfx file
dotnet dev-certs https -ep $env:APPDATA\ASP.NET\Https\FileService.API.pfx -p crypticpassword

// open api folder
cd Services/FileService/FileService.API

// generate user secret
dotnet user-secrets -p ./FileService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
```

## Database migrations

Run this command at the root level inside ./Services/FileService

```
dotnet ef migrations add Init --project FileService.Infrastructure -s FileService.API
dotnet ef database update --project FileService.Infrastructure -s FileService.API
```

## Cloud Provider

Run this commands to configure cloudinary account details

```
dotnet user-secrets set "Cloudinary:CloudName" "<your-cloud-name>"
dotnet user-secrets set "Cloudinary:ApiKey" "<your-api-key>"
dotnet user-secrets set "Cloudinary:ApiSecret" "<your-api-secret>"
```

Clean up

```
dotnet user-secrets remove
```
