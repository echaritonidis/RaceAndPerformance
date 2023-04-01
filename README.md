# RaceAndPerformance Web API

This is an ASP.NET Core 3.1 Web API project called RaceAndPerformance. The project uses the following technologies and features:

- MSSQL 2019 server
- Docker
- Healthcheck
- Swagger
- Mediator pattern with validations
- Rate limiting

## Prerequisites

To build and run the RaceAndPerformance Web API project, you'll need the following:

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- The `dotnet ef` tool, which can be installed using the following command: `dotnet tool install --global dotnet-ef` 

Make sure to restart your command prompt or terminal after installing the `dotnet ef` tool.

## Getting started

1. Clone the repository: `git clone https://github.com/echaritonidis/RaceAndPerformance.git`
2. Navigate to the project directory: `cd RaceAndPerformance`
3. Run the docker-compose.yml: `docker-compose -f docker-compose.yml -f docker-compose.override.yml up`

The API should now be accessible at `https://localhost:5001`.

## Database Migration

To setup the database and run migrations, follow these steps:

1. Navigate to the `Core` directory: `cd ../Core`
2. Run the migration command: `dotnet ef migrations add InitialDbCreation --startup-project ../Api`
3. The application will apply the migration on cold start

This will create the initial database and apply any pending migrations.

## Swagger

Swagger has been integrated with the API for easy testing and documentation. You can access Swagger UI at `https://localhost:5001/swagger/index.html`.

## Healthchecks

The API includes a healthcheck endpoint at `https://localhost:5001/hc`. This can be used to monitor the health of the app.

## Rate Limiting

The API includes rate limiting. The default configuration limits each IP to 100 requests per minute. The rate limiting rules can be configured in the appsettings.Development.json file under the IpRateLimiting section.

## Mediator Pattern and Validation

The API uses the Mediator pattern along with FluentValidation for command and query validation. Commands and queries are validated automatically before being processed by their respective handlers.

## Tests

Integration tests have been written for the API controllers using the xUnit. The tests can be found in the `Tests` directory and are run using the `dotnet test` command.

## Postman

The collection contains a set of pre-configured requests that can be used to interact with the API:

1. Import the postman_collection.json file into Postman.
2. Expand the collection to see the available endpoints.
3. Click on the Send button to send the request to the API.

## License

This project is licensed under the [MIT License](LICENSE).