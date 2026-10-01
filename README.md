## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# SFA.DAS.Campaign.Api

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

[![Build Status](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/das-digital-engagement-email-integration?branchName=master)](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_build/latest?definitionId=4225&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-campaign-api&metric=alert_status)](https://sonarcloud.io/project/overview?id=SkillsFundingAgency_das-campaign-api)
[![Jira Project](https://img.shields.io/badge/Jira-Project-blue)](https://skillsfundingagency.atlassian.net/jira/software/c/projects/P2/boards/686)
[![Confluence Project](https://img.shields.io/badge/Confluence-Project-blue)](https://skillsfundingagency.atlassian.net/wiki/spaces/NDL/folder/4987027481/E-shot)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

```
The Campaign API receives campaign interest submissions and stores the supplied user data in the campaign database. It is an ASP.NET Core Web API used by the Digital Apprenticeship Service campaign journey.
```

## How It Works

```
The API is hosted by the standard ASP.NET Core Startup pipeline. Requests to POST /api/registercampaigninterest/registerinterest are validated, mapped from UserDataEntity to the persistence model, and passed to IUserDataRepository.

A successful write returns 201 Created. Invalid input returns 400 Bad Request, and failed writes return 500 Internal Server Error.

Swagger is served at the application root. The OpenAPI document is available at /swagger/v1/swagger.json and the health check endpoint is available at /health.
```

## 🚀 Installation

### Pre-Requisites

```
* A clone of this repository
* Visual Studio 2026 (or another IDE supporting .NET 10 development)
* .NET 10 SDK installed
* A SQL server which is either an Azure SQL database or SQL Server 2022 Developer Edition running locally
* Visual Studio with SSDT installed if the database project needs to be built or deployed locally
```

```
Build the API from the repository root using the SFA.DAS.Campaign.Api project. Publish the database using the SFA.DAS.Campaign.Database SQL project and SSDT tooling.
```

Build the API from the repository root:

```bash
dotnet build src/SFA.DAS.Campaign.Api/SFA.DAS.Campaign.Api.csproj
```

Run the unit tests with:

```bash
dotnet test src/SFA.DAS.Campaign.Api.UnitTests/SFA.DAS.Campaign.Api.UnitTests.csproj
```

### Config

Azure Table Storage config

Row Key: SFA.DAS.Campaign.Api_1.0

Partition Key: LOCAL

```
This service uses the standard Apprenticeship Service configuration. The configuration row must provide CampaignConfiguration:SqlConnectionString. Configuration is loaded from Azure Table Storage.
```

The local defaults in `appsettings.json` are:

```json
{
  "ConfigurationStorageConnectionString": "UseDevelopmentStorage=true;",
  "ConfigNames": "SFA.DAS.Campaign.Api",
  "EnvironmentName": "LOCAL",
  "ResourceEnvironmentName": "LOCAL",
  "Version": "1.0",
  "APPLICATIONINSIGHTS_CONNECTION_STRING": ""
}
```

## 🔗 External Dependencies

```
* Azure Table Storage for shared DAS configuration
* Azure SQL / SQL Server for campaign data
* Application Insights and OpenTelemetry for application telemetry
* Azure DevOps pipelines and DAS platform building blocks for build and deployment
```

## Technologies

```
* .NET 10
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server Database Project (SSDT)
* Azure Table Storage configuration
* Swashbuckle / OpenAPI (Swagger)
* OpenTelemetry and Application Insights
* FluentValidation
* Newtonsoft.Json
* NUnit, Moq, FluentAssertions 
```