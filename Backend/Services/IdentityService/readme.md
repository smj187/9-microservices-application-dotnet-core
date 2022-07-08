## HTTPS setup

```
// create pfx file
dotnet dev-certs https -ep $env:APPDATA\ASP.NET\Https\IdentityService.API.pfx -p crypticpassword

// open api folder
cd Services/IdentityService/IdentityService.API

// generate user secret
dotnet user-secrets -p .\IdentityService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
```

```
rmdir .\IdentityService.Infrastructure\Migrations\ -r ; dotnet ef migrations add Init --project IdentityService.Infrastructure -s IdentityService.API
dotnet ef database update --project IdentityService.Infrastructure -s IdentityService.API
```

how to handle refresh token
https://stackoverflow.com/a/26834685/18175280
