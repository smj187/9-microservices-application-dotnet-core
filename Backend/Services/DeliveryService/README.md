## Initial Setup

### Configure HTTPS

_Reference: [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide#create-a-self-signed-certificate)_

create certification for service

```
dotnet dev-certs https -ep $env:APPDATA\ASP.NET\Https\DeliveryService.API.pfx -p crypticpassword
```

generate user secret

```
dotnet user-secrets -p ./DeliveryService.API/DeliveryService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
```
