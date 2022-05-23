```
 dotnet ef migrations add Init --project IdentityService.Infrastructure -s IdentityService.API
 dotnet ef database update --project IdentityService.Infrastructure -s IdentityService.API
```

```
docker-compose up --build
docker-compose down
```
