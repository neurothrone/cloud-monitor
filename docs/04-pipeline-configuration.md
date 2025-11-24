# Pipeline Configuration

## Prerequisites

- Azure DevOps CLI configured (see [Azure DevOps Setup](03-azure-devops-setup.md))
- Azure DevOps organization and project access

## Create Pipeline

Create a pipeline using the YAML file in your repository:

```shell
repoName="cloud-monitor"
branchName="main"

az pipelines create \
  --repository-type tfsgit \
  --name "build-test-deploy-pipeline" \
  --repository $repoName \
  --branch $branchName \
  --yml-path .azure-pipelines/build-test-deploy-pipeline.yml \
  --skip-first-run
```

### Valid Repository Types

- `tfsgit` - Azure Repos Git
- `github` - GitHub
- `githubenterprise` - GitHub Enterprise
- `bitbucket` - Bitbucket Cloud

## Pipeline Variables

### Get Pipeline ID

```shell
pipelineName="build-test-deploy-pipeline"
pipelineId=$(az pipelines show --name $pipelineName --query id -o tsv)
```

### Create Variables

Create non-secret variables:

```shell
az pipelines variable create \
  --pipeline-id $pipelineId \
  --name aspNetCoreEnvironment \
  --value "Development"
```

Create secret variables:

```shell
az pipelines variable create \
  --pipeline-id $pipelineId \
  --name DbConnectionString \
  --value "Server=..." \
  --secret true
```

### List Variables

```shell
az pipelines variable list --pipeline-id $pipelineId --output table
```

### Update Variable

```shell
az pipelines variable update \
  --pipeline-id $pipelineId \
  --name aspNetCoreEnvironment \
  --value "Production"
```

### Delete Variable

```shell
az pipelines variable delete \
  --pipeline-id $pipelineId \
  --name aspNetCoreEnvironment
```

## Run Pipeline

```
az pipelines runs list --pipeline-ids $pipelineId --output table
```shell

### View Pipeline Runs

```

az pipelines run --name $pipelineName

```shell

### Trigger Pipeline Run


