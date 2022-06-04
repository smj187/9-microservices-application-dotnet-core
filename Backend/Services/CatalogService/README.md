## Initial Setup

### Configure HTTPS

_Reference: [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide#create-a-self-signed-certificate)_

create certification for service

```
dotnet dev-certs https -ep $env:APPDATA\ASP.NET\Https\CatalogService.API.pfx -p crypticpassword
```

generate user secret

```
dotnet user-secrets -p ./CatalogService.API/CatalogService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
```

### Configure Local Nuget

```
dotnet nuget add source $PWD/../../../packages -n LocalPackages
```
