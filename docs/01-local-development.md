# Local Development

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

Install EF Core tools globally:

```shell
dotnet tool install --global dotnet-ef
```

## SQL Server Setup

### Start SQL Server Container

```shell
docker run \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
  -p 1433:1433 \
  --name sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

### Stop Container

```shell
docker stop sqlserver
```

### Start Existing Container

```shell
docker start sqlserver
```

### Remove Container

```shell
docker rm sqlserver
```

## Database Migrations

### Add Migration

```shell
dotnet ef migrations add <MigrationName> \
  --project CloudMonitor.Persistence \
  --startup-project CloudMonitor.Api
```

### Apply Migrations

```shell
dotnet ef database update \
  --project CloudMonitor.Persistence \
  --startup-project CloudMonitor.Api
```

### Remove Last Migration

```shell
dotnet ef migrations remove \
  --project CloudMonitor.Persistence \
  --startup-project CloudMonitor.Api
```

### List Migrations

```shell
dotnet ef migrations list \
  --project CloudMonitor.Persistence \
  --startup-project CloudMonitor.Api
```
