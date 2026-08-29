# MovieApp

A movie management REST API built with **ASP.NET Core** following **Clean Architecture** principles, with a focus on maintainability, separation of concerns, authentication, authorization, relational data modeling, and automated testing.

## Features

* Create, retrieve, update, and delete movies
* Movie genres and user ratings
* Clean Architecture
* CQRS with MediatR
* Domain-driven design principles
* ASP.NET Core Identity
* Authentication and Authorization
* FluentValidation
* Result Pattern for handling operation outcomes
* Soft Delete
* Entity Framework Core
* SQL Server
* Dependency Injection
* Unit Testing
* Integration Testing
* Architecture Testing

## Technologies

* **C#**
* **ASP.NET Core**
* **Entity Framework Core**
* **SQL Server**
* **ASP.NET Core Identity**
* **MediatR**
* **FluentValidation**
* **xUnit**
* **Moq**
* **FluentAssertions**
* **Git / GitHub**

## Architecture

The project follows **Clean Architecture** principles with a clear separation of concerns between the domain, application, infrastructure, and presentation layers.

```text id="n3r4yc"
MovieApp
│
├── MovieApp.Domain
│   └── Domain entities and business rules
│
├── MovieApp.Application
│   └── Commands, queries, handlers, validation, and application behaviors
│
├── MovieApp.Infrastructure
│   └── Persistence, Identity, authentication, and infrastructure implementations
│
├── MovieApp.SharedKernel
│   └── Shared domain primitives and common abstractions
│
└── MovieApp.API
    └── REST API endpoints and presentation layer
```

## Authentication & Authorization

The application uses **ASP.NET Core Identity** to manage users and implement authentication and authorization.

Protected operations are secured using the application's authorization mechanisms.

## Data Relationships

The project demonstrates relational data modeling and entity relationships using **Entity Framework Core**.

Key relationships include:

* **Movie ↔ Genre** — Many-to-Many relationship
* **Movie ↔ Rating** — One-to-Many relationship
* **User ↔ Rating** — One-to-Many relationship

Entity relationships are configured using **Entity Framework Core** and relational database concepts.

## Domain Model

The core domain includes:

* **Movie** — represents a movie and its related information.
* **Genre** — represents a movie genre.
* **Rating** — represents a user's rating for a movie.

Movies can have multiple genres and ratings.

## Validation

**FluentValidation** is used to validate application commands before they reach their corresponding handlers.

Validation is integrated into the application pipeline using a **MediatR behavior**.

## Soft Delete

Movies support **soft deletion**, allowing records to remain in the database while being excluded from normal queries.

## Testing

The project includes automated tests to verify application behavior and improve reliability.

Testing includes:

* **Unit Tests**
* **Integration Tests**
* **Architecture Tests**

## Purpose

The project was developed as an advanced backend practice project to apply Clean Architecture and modern ASP.NET Core development practices in a structured application.

It builds upon an earlier Movie REST API project and extends it with **Clean Architecture, CQRS, authentication and authorization, validation, relational data modeling, soft delete, and automated testing**.
