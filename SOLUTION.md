
This solution uses a clean controller-based .NET 8 API and emphasizes simplicity and testability.
Architecture:
- ASP.NET Core 8 Web API (Controller-based)
- EF Core with InMemory database for lightweight development storage
- Repository Pattern (GenericRepository + TaskRepository)
- Global Exception Handling middleware
- CORS configured for Angular 18 frontend (http://localhost:4200)
- Swagger for automatic API documentation
- HttpLogging for request/response visibility
- xUnit + FakeItEasy for unit testing and mocking
Key Design Decisions:
1. InMemoryDatabase:
Chosen to eliminate setup friction and allow instant development testing. Ideal for demos, not for
production use.
2. Repository Pattern:
Wraps DbContext to provide clean separation of concerns and enable mock-based unit testing. Keeps
controllers lean.
3. Global Exception Middleware:
Standardizes error responses using ProblemDetails and centralizes all error handling.
4. Controller-Based Approach:
Selected for clarity, organization, and ease of expansion in multi-endpoint APIs.
5. CORS Policy:
Configured specifically to allow communication from Angular’s dev server running on port 4200.
6. Unit Testing Strategy:
FakeItEasy is used to fake repository interfaces, enabling isolated testing of controller logic without
touching EF Core.
7. Swagger:
Enables immediate interactive testing of all CRUD endpoints during development.
Overall:
This solution focuses on maintainability, readability, and reliable development workflows. It suits
CRUD-based applications and educational setups where clean layering and testability are essential.

Angular Frontend Consuming API
Technology Choice

You selected Angular to build the frontend solution because it provides a strong, opinionated structure and works well for enterprise-style applications that consume REST APIs. Using the Angular CLI ensured that the project was created with best-practice scaffolding, clear folder organization, and built-in tooling.

Framework & Tooling

Your environment is built with:

Angular 18 (latest generation standalone architecture)

Angular CLI 18.2.21

TypeScript + Strong Typing

Bootstrap for UI styling

Node 24 with npm 11

Runs on Windows x64

This combination provides fast development cycles, modern frontend patterns, and clean UI styling out of the box.

Application Structure

Crafted the application using Angular CLI generators for components, services, routing, and models. This gives the project a clean modular structure that aligns with Angular 18’s recommended architecture and standalone APIs.


Trade-Off

Using TypeScript enums in Angular introduces a limitation: enums automatically create a two-way mapping, meaning each enum produces both numeric keys and string keys. When retrieving enum values to populate dropdown lists, this results in extra numeric entries that do not belong in the UI. Because of this behavior, enums are not naturally UI-friendly and require additional filtering to remove the numeric members.

Solution, I handle this trade-off by extracting only the string keys from the enum using a filtering approach. This allows you to display clean text values in dropdowns while still retaining the numeric values internally for API communication. The filtered keys provide proper labels for selection, and when selected, the enum automatically translates the string key back to its numeric value, keeping your binding to the .NET API consistent and strongly typed.
