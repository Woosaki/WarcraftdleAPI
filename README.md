# WarcraftdleAPI
WarcraftdleAPI is a C# based API designed for the game "Warcraftdle".

It provides various endpoints to support the game's functionalities.

This project began as an academic project and continues to be developed and refined.

## Architectural Overview

This project implements the CQRS pattern using MediatR.

It adheres to SOLID principles and is designed following Clean Architecture to ensure maintainability, scalability, and testability.

## Infrastructure

- **Docker Compose**: The project uses Docker Compose to run the API and database in their respective containers.
- **Unit Tests**: Comprehensive unit tests are included to ensure code quality.
- **CI Pipeline**: A continuous integration pipeline checks that the project builds successfully, all tests pass, and then publishes the Docker image to DockerHub.

## Used Packages

- **Microsoft.EntityFrameworkCore**
- **Npgsql.EntityFrameworkCore.PostgreSQL**
- **Serilog.AspNetCore**
- **Swashbuckle.AspNetCore**
- **AutoMapper**
- **FluentValidation.AspNetCore**
- **MediatR**
- **xunit**
- **Moq**
- **FluentAssertions**
