```
docker-compose -f docker-compose-identity.yaml up --build
docker-compose -f docker-compose-identity.yaml down

docker-compose -f docker-compose-tenant.yaml up --build
docker-compose -f docker-compose-tenant.yaml down



```

### Basket Service

```
// https setup
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\BasketService.pfx -p crypticpassword
dotnet user-secrets -p ./Services/BasketService/BasketService.API/BasketService.API.csproj set "Kestrel:Certificates:Production:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose-basket.yaml up --build
```

### Catalog Service

```
// https setup
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\CatalogService.pfx -p crypticpassword
dotnet user-secrets -p ./Services/CatalogService/CatalogService.API/CatalogService.API.csproj set "Kestrel:Certificates:Production:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose-catalog.yaml up --build
```

### Delivery Service

```
// https setup
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\DeliveryService.pfx -p crypticpassword
dotnet user-secrets -p ./Services/DeliveryService/DeliveryService.API/DeliveryService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose-delivery.yaml up --build
```

### File Service

```
// https setup
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\FileService.pfx -p crypticpassword
dotnet user-secrets -p ./Services/FileService/FileService.API/FileService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose-file.yaml up --build
```

### Identity Service

```
// https setup
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\IdentityService.pfx -p crypticpassword
dotnet user-secrets -p ./Services/IdentityService/IdentityService.API/IdentityService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose-identity.yaml up --build
```

### Payment Service

```
// https setup
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\PaymentService.pfx -p crypticpassword
dotnet user-secrets -p ./Services/PaymentService/PaymentService.API/PaymentService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose-payment.yaml up --build
```

### Tenant Service

```
// https setup
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\TenantService.pfx -p crypticpassword
dotnet user-secrets -p ./Services/TenantService/TenantService.API/TenantService.API.csproj set "Kestrel:Certificates:Development:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose-tenant.yaml up --build
```

### Translation Service

```
// https setup
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\TranslationService.pfx -p crypticpassword
dotnet user-secrets -p ./Services/TranslationService/TranslationService.API/TranslationService.API.csproj set "Kestrel:Certificates:Production:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose-translation.yaml up --build
```
