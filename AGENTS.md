# BookBase — AGENTS.md

.NET 10 Clean Architecture web API for book management with JWT auth.

## Architecture

```
BookBase.Api          — ASP.NET Core Web API entrypoint (FastEndpoints, not MVC)
BookBase.Application  — Services, validation, mapping, DI extensions
BookBase.Domain       — Entities, DTOs, Commands, abstractions, exceptions, config POCOs
BookBase.Infrastructure — EF Core + PostgreSQL (Npgsql), repositories, migrations
BookBase.Helper       — Console seeder app (Bogus faker data)
```

Dependency direction: `Api → Application`, `Api → Infrastructure`, `Application → Domain`, `Infrastructure → Domain`. `Helper → Application + Infrastructure`.

## CodeGraph

This repo is CodeGraph-indexed (`.codegraph/` exists). Before grep/find or reading files to understand code, use:

```
codegraph explore "<symbol names or question>"
```

It returns verbatim source + call paths including dynamic-dispatch hops that grep cannot follow. Much faster and cheaper than scanning files manually.

## Commands

```bash
# Build
dotnet build

# Run API (port 5000 HTTP / 5500 HTTPS)
dotnet run --project BookBase.Api

# Run seeder (requires env var, see below)
dotnet run --project BookBase.Helper

# EF migrations
dotnet ef migrations add <Name> --project BookBase.Infrastructure --startup-project BookBase.Api
dotnet ef database update --project BookBase.Infrastructure --startup-project BookBase.Api
```

## Key conventions & quirks

- **API framework**: FastEndpoints — endpoints are classes under `BookBase.Api/Endpoints/`, not controllers. Routes: `Post("/api/auth/sign-in")` etc.
- **Auth**: JWT Bearer. Config section `JwtConfiguration` in `appsettings.Development.json` (key `JwtConfiguration`). Dev token lifetime: 59 min.
- **DB**: PostgreSQL, schema `bookbase`, snake_case naming enforced by `EFCore.NamingConventions`. Query tracking disabled globally (`NoTracking`).
- **Connection string**: key `BooksBaseConnection` in `ConnectionStrings`. Dev: `Host=localhost; Port=5431; Database=booksbase; Username=postgres; Password=admin`.
- **FluentValidation**: language manager **disabled globally** (`ValidatorOptions.Global.LanguageManager.Enabled = false`). Error responses via `UseProblemDetails()`.
- **CORS**: allows `localhost:3000` (React) and `localhost:4200` (Angular).
- **Serializer**: camelCase + case-insensitive property names.
- **Default admin seed**: `admin` / `password` (via `PasswordHasher`). Roles: `user`, `admin`.
- **Seeder env var**: set `ConnectionStrings__BooksBaseConnection` (double underscore syntax) before running BookBase.Helper.
- **Solution**: `.slnx` format (new `.sln` XML variant). Use `dotnet build` (no sln file needed).
- **No tests** exist in this repo.
- **Stale file**: `BookBase.Api.http` still references a weatherforecast route that does not exist.
