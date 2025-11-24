# Azure Deployment

## Prerequisites

- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
- [Bicep CLI](https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/install)
- Azure subscription with appropriate permissions

## Authentication

Login to Azure:

```shell
az login
```

Set your subscription (if you have multiple):

```shell
az account set --subscription "<subscription-id>"
```

## Resource Deployment

### Deploy Infrastructure

Deploy Azure resources using Bicep templates:

```shell
rgName="rg-cloud-monitor"

az deployment group create \
  --resource-group $rgName \
  --template-file infra/main.bicep \
  --parameters infra/main.bicepparam \
  --confirm-with-what-if \
  --debug
```

### Verify Deployment

List deployed resources:

```shell
az resource list --resource-group $rgName --output table
```

## SQL Server Configuration

### Add Firewall Rule

Allow your local machine to access the SQL Server:

```shell
rgName="rg-cloud-monitor"
serverName="cloud-monitor-sql-server-prod"
ipToAllow=$(curl -s ifconfig.me)

az sql server firewall-rule create \
  --resource-group $rgName \
  --server $serverName \
  --name AllowMyIP \
  --start-ip-address $ipToAllow \
  --end-ip-address $ipToAllow
```

### List Firewall Rules

```shell
az sql server firewall-rule list \
  --resource-group $rgName \
  --server $serverName \
  --output table
```

### Remove Firewall Rule

```shell
az sql server firewall-rule delete \
  --resource-group $rgName \
  --server $serverName \
  --name AllowMyIP
```

## Production Database Management

### Add Migration

```shell
dotnet ef migrations add <MigrationName> \
  --project CloudMonitor.Persistence \
  --startup-project CloudMonitor.Api
```

### Apply Migrations

Set the environment to Production and apply migrations:

```shell
export ASPNETCORE_ENVIRONMENT="Production"

dotnet ef database update \
  --project CloudMonitor.Persistence \
  --startup-project CloudMonitor.Api
```

> **Warning:** Ensure you have a backup before applying migrations to production.

### List Applied Migrations

```shell
export ASPNETCORE_ENVIRONMENT="Production"

dotnet ef migrations list \
  --project CloudMonitor.Persistence \
  --startup-project CloudMonitor.Api
```

## Troubleshooting

### View App Service Logs

```shell
az webapp log tail \
  --resource-group $rgName \
  --name cloud-monitor-app-prod
```
