# Azure DevOps Setup

## Prerequisites

- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
- [Azure DevOps Extension](https://learn.microsoft.com/en-us/azure/devops/cli/)
- Azure DevOps organization and project

## Authentication

### Using Azure Login

Authenticate to Azure:

```shell
az login
```

### Using Personal Access Token

Set your Personal Access Token (PAT):

```shell
export AZURE_DEVOPS_EXT_PAT=<your-pat-token>
```

## Install Azure DevOps Extension

Install the Azure DevOps extension for Azure CLI:

```shell
az extension add --name azure-devops
```

## Configure Defaults

Configure the Azure CLI to use your Azure DevOps organization and project:

```shell
az devops configure --defaults organization=https://dev.azure.com/organizationName project=projectName
```

## Verify Configuration

View current configuration:

```shell
az devops configure --list
```
