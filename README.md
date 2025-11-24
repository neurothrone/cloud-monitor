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

#### Update Database

```shell
ASPNETCORE_ENVIRONMENT="Production"

dotnet ef database update \
  --project CloudMonitor.Persistence \
  --startup-project CloudMonitor.Api
```

### Azure Pipelines

Run the pipeline defined in the file `.azure-pipelines/build-test-deploy-pipeline.yml` to deploy the application to
Azure.

## Quality of Life

### Azure Pipeline Variables

This section describes how to configure Azure Pipeline variables for the application through the Azure CLI, which allows
us to define variables from our local development environment without needing to access the Azure DevOps web interface.

#### Configure Azure CLI

Authenticate to Azure by running the following command:

```shell
az login
```

Or use a Personal Access Token (PAT) to authenticate.

```shell
export AZURE_DEVOPS_EXT_PAT=<your-path-token>
```

Install the Azure DevOps extension for Azure CLI by running the following command:

```shell
az extension add --name azure-devops
```

Configure the Azure CLI to use your Azure DevOps organization and project by running the following command:

```shell
az devops configure --defaults organization=https://dev.azure.com/organizationName project=projectName
```

#### Create Pipeline Variables

The following commands create pipeline variables for the pipeline named `build-test-deploy-pipeline`. Adjust the
variable names and values as needed for your specific configuration.

```shell
pipelineName="build-test-deploy-pipeline"
pipelineId=$(az pipelines show --name $pipelineName --query id -o tsv)

az pipelines variable create --pipeline-id $pipelineId --name aspNetCoreEnvironment --value "Development"
az pipelines variable create --pipeline-id $pipelineId --name DbConnectionString --value "Server=..." --secret true
```