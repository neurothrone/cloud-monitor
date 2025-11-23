# CloudMonitor

## Local Development

### Docker CLI

```shell
docker run \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
  -p 1433:1433 \
  --name sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

### Entity Framework Core CLI

#### Add Migrations

```shell
dotnet ef migrations add InitialCreate \
  --project CloudMonitor.Persistence \
  --startup-project CloudMonitor.Api
```

#### Update Database

```shell
dotnet ef database update \
  --project CloudMonitor.Persistence \
  --startup-project CloudMonitor.Api
```

## Deploy to Azure

### Bicep CLI

```shell
rgName="rg-cloud-monitor"

az deployment group create \
  --resource-group $rgName \
  --template-file infra/main.bicep \
  --parameters infra/main.bicepparam \
  --confirm-with-what-if \
  --debug
```