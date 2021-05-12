# redONE Rewards

An ASP.NET Core Web API project that implements a mock back-end system for using and managing redONE's rewards system.

## Prerequisites

- [.NET 5](https://dotnet.microsoft.com/download)
- [MySQL 8.0+](https://dev.mysql.com/downloads/mysql/)

## Installation and Usage

### Database provisioning

Execute the `db-schema.sql` script on a blank MySQL schema to create the required tables and stored procedures for the application.

Alternatively, a version of the database with populated tables can be imported from the `db-backup.sql` file directly into MySQL.

### First-time application startup

1. Override the default settings provided in `src/RedOne.Rewards.WebApi/appsettings.json` by creating an `appsettings.Development.json` file in the same directory. Typically, the following settings should be overriden:

```json
{
  "AppSettings": {
    "JwtSecret": "some very long secret string for JWT encryption here"
  },
  "ConnectionStrings": {
    "RedOneRewards": "Server=localhost;Database=redone_rewards_1;AllowUserVariables=true;User Id=someuser;Password=123456"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

This is also where the connection string for the database connection should be set.

2. Run the Web API project with the `--seed-data` flag from the command line to seed the database with mock data. This flag can be omitted for subsequent launches of the application.

```bash
$ cd src/RedOne.Rewards.WebApi
$ dotnet run --seed-data
```

Alternatively, you can also launch the Web API project from Visual Studio, supplying the `--seed-data` application argument under the Web API project properties, in the Debug category.

**NOTE**: If the database was provisioned using the `db-backup.sql` file with the pre-populated tables, the application can be run without the `--seed-data` flag.

3. Navigate to https://localhost:5001/swagger/index.html to view the Swagger UI documentation.

## Use Cases

This section describes the use cases of this application. Further information on the endpoints used to execute these use cases can be found in the Swagger UI documentation when running the application.

### Authentication

Separate authentication endpoints are provided for both consumer and admin users. These endpoints return a JWT upon successful authentication, which can then be used in the `Authorization` header of requests to other secured endpoints.

### Administrator endpoints

#### Banner management

Administrators have a set of list, create and delete endpoints to manage banners that are displayed to the consumer user.

#### Reward management

Administrators have a set of list, create and delete endpoints to manage rewards that are available for the consumer user to redeem.

### Consumer user endpoints

#### User information

Consumer users have access to endpoints for retrieving their user information as well as current resource usage information (e.g. for calls, text messages and internet quotas).

#### Banner information

Consumer users can get a list of banners that are able to be viewed.

#### Rewards system

Through the rewards system, consumer users can view a list of rewards that are able to be redeemed, as well as viewing their current reward points status and available member levels. They can also redeem a reward and view their past redemptions through dedicated endpoints. Furthermore, the reward redemption process also involves a task queue that can execute background tasks such as sending emails and/or push notifications without slowing down the API request pipeline.
