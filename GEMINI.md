# Project Overview

This is a .NET 8 Web API for an online store. It follows a clean architecture pattern, with separate projects for Core, Infrastructure, and Web.

*   **`OnlineStore.Core`**: Contains the core business logic, entities, and interfaces.
*   **`OnlineStore.Infrastructure`**: Implements data access and other external services.
*   **`OnlineStore.Web`**: Exposes the application's functionality through a RESTful API.

## Key Technologies

*   **.NET 8**: The underlying framework for the application.
*   **ASP.NET Core**: Used to build the web API.
*   **Entity Framework Core & Dapper**: Used for data access.
*   **JWT (JSON Web Tokens)**: Used for authentication.
*   **Serilog**: Used for logging.
*   **Swagger (Swashbuckle)**: Used for API documentation.
*   **PayTabs**: Used for payment processing.

## Building and Running

To build and run the project, you can use the following commands from the root directory:

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the web API (from the src/OnlineStore.Web directory)
dotnet run --project src/OnlineStore.Web/OnlineStore.Web.csproj
```

The API documentation will be available at the root URL of the application (e.g., `https://localhost:5001`).

## Development Conventions

*   **Clean Architecture**: The code is organized into three main layers: Core, Infrastructure, and Web.
*   **Dependency Injection**: The application uses dependency injection to decouple components.
*   **Repository Pattern**: The application uses the repository pattern to abstract data access logic.
*   **User Secrets**: The application uses user secrets to store sensitive information like connection strings and JWT keys. To configure the application, you will need to set up the following user secrets:
    *   `ConnectionStrings:DefaultConnection`: The connection string to the database.
    *   `JwtSettings:Issuer`: The issuer for JWT tokens.
    *   `JwtSettings:audience`: The audience for JWT tokens.
    *   `JwtSettings:SecurityKey`: The security key for signing JWT tokens.

## Interaction Preferences

*   **Guidance over Solutions**: When I ask for help, guide me toward the solution but do not provide it directly. My preference is to think through the problem and come up with the solution on my own.
*   **Gemini CLI Instructions**:
    * You are an expert web developer, help answering my questions and teaching me web development concepts. Focus on teaching me rather than just giving 
    * Search the internet whenever you think that you are not sure about your answer


## Development Workflow

*   **Database Scaffolding**: I use scaffolding after every update to my database.
