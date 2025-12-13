# Branch review vs 14-day plan (status after Day 4)

## Overview
- Solution layout follows the Hosts/Modules/BuildingBlocks split and already wires the Users module into the Web API host with JWT auth, Swagger, CORS, and a health endpoint.
- Building blocks for domain modeling and CQRS exist, and the Users domain/application/infrastructure stack delivers registration/login plus a basic `/api/users/me` endpoint.
- Contracts and non-Users modules (Subscriptions, Exercises, etc.) remain skeletal, and automated tests are mostly placeholders.

## Day-by-day notes

### Day 1 – Solution skeleton & building blocks
- **Done:** Domain base types (`Entity`, `AggregateRoot`, `ValueObject`) and core exceptions are present.【F:src/BuildingBlocks/LingEdu.BuildingBlocks.Domain/Models/Entity.cs†L1-L55】【F:src/BuildingBlocks/LingEdu.BuildingBlocks.Domain/Exceptions/DomainException.cs†L1-L14】
- **Done:** CQRS abstractions and the generic `Result` helper exist in BuildingBlocks.Application.【F:src/BuildingBlocks/LingEdu.BuildingBlocks.Application/Cqrs/Interfaces.cs†L1-L32】【F:src/BuildingBlocks/LingEdu.BuildingBlocks.Application/Common/Result.cs†L1-L58】
- **Partial:** Infrastructure base DbContext is present but only provides tracking toggles—no auditable interface or auditing hooks yet.【F:src/BuildingBlocks/LingEdu.BuildingBlocks.Infrastructure/EF/LingEduDbContextBase.cs†L1-L24】

### Day 2 – WebApi host, JWT pipeline, Contracts, smoke tests
- **Done:** Web API host registers Swagger, CORS, JWT authentication, global exception handling, and a `/api/health` probe.【F:src/Hosts/LingEdu.WebApi/Program.cs†L7-L93】【F:src/Hosts/LingEdu.WebApi/Middlewares/ExceptionHandlingMiddleware.cs†L1-L40】
- **Done:** An integration test hits `/api/health` to ensure the host starts.【F:tests/LingEdu.Users.IntegrationTests/UnitTest1.cs†L1-L22】
- **Missing:** No API versioning is configured yet, and the Contracts project contains only the csproj (no DTOs).【F:src/Contracts/LingEdu.Contracts/LingEdu.Contracts.csproj†L1-L9】

### Day 3 – Users.Domain + Infrastructure
- **Done:** User aggregate with premium/active flags and role management, plus repository abstraction, are implemented.【F:src/Modules/Users/LingEdu.Users.Domain/Users/User.cs†L8-L119】【F:src/Modules/Users/LingEdu.Users.Domain/Users/IUserRepository.cs†L1-L22】
- **Done:** EF Core DbContext and repository implementation are available for Users, with configuration registration in DI.【F:src/Modules/Users/LingEdu.Users.Infrastructure/Persistence/UsersDbContext.cs†L1-L22】【F:src/Modules/Users/LingEdu.Users.Infrastructure/Persistence/Repositories/UserRepository.cs†L1-L40】【F:src/Modules/Users/LingEdu.Users.Infrastructure/DependencyInjection.cs†L1-L34】
- **Missing:** Database migrations/seeding are not present; Role configuration exists but roles are not seeded.

### Day 4 – Users.Application + Auth API
- **Done:** User-facing DTOs and mappings exist alongside password hashing and JWT generation services.【F:src/Modules/Users/LingEdu.Users.Application/Users/UserDto.cs†L1-L14】【F:src/Modules/Users/LingEdu.Users.Application/Users/UserMappingExtensions.cs†L1-L32】【F:src/Modules/Users/LingEdu.Users.Infrastructure/Services/PasswordHasher.cs†L1-L39】【F:src/Modules/Users/LingEdu.Users.Application/Auth/AuthService.cs†L1-L41】
- **Done:** CQRS handlers for registration/login plus `/api/auth` and `/api/users/me` endpoints are implemented.【F:src/Modules/Users/LingEdu.Users.Application/Users/RegisterUser/RegisterUserCommandHandler.cs†L1-L44】【F:src/Modules/Users/LingEdu.Users.Application/Users/LoginUser/LoginUserCommandHandler.cs†L1-L52】【F:src/Hosts/LingEdu.WebApi/Controllers/AuthController.cs†L9-L36】【F:src/Hosts/LingEdu.WebApi/Controllers/UsersController.cs†L20-L34】
- **Missing:** Unit tests for password hashing/registration are stubs, and there is no JWT validation test or authorization guard coverage.【F:tests/LingEdu.Users.UnitTests/UnitTest1.cs†L1-L10】

## Other observations
- Subscriptions, Exercises, Ranking, Contests, and Reports modules are still placeholders with only default `Class1` files—no domain/application code yet.【F:src/Modules/Subscriptions/LingEdu.Subscriptions.Domain/Class1.cs†L1-L6】
- No Contracts DTOs or Web API controllers exist for non-Users areas, so later-day vertical slices have not started.
- `dotnet test` cannot currently run in this environment because the .NET SDK is missing; CI will need the SDK installed to execute tests.
