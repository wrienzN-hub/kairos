# Backend

The backend is an ASP.NET Core modular monolith targeting .NET 10 LTS.

## Structure

```text
backend/
├── src/
│   ├── Kairos.Api/             # HTTP host, startup, and configuration
│   ├── Kairos.Application/     # use cases and application contracts
│   ├── Kairos.Domain/          # domain model and invariants
│   └── Kairos.Infrastructure/  # technical adapters and persistence
└── tests/
    ├── Kairos.UnitTests/
    └── Kairos.IntegrationTests/
```

Dependencies point inward: API and Infrastructure depend on Application, while
Application depends on Domain. Domain has no project dependency.

The vendor-neutral activity aggregate and its invariants are documented in
[`docs/domain/activity-domain-model.md`](../docs/domain/activity-domain-model.md).

## Commands

Run these commands from the repository root with the SDK pinned in
`global.json`:

```powershell
dotnet restore Kairos.sln
dotnet build Kairos.sln --configuration Release --no-restore
dotnet test Kairos.sln --configuration Release --no-build
dotnet run --project backend/src/Kairos.Api
```

After starting the API, request `GET /health` to verify readiness. The default
development address is printed by ASP.NET Core at startup.

## Configuration

The API reads product settings from the `Kairos` configuration section. Required
values are validated on startup, so an invalid deployment fails immediately with
an actionable validation message. Environment variables use the normal ASP.NET
Core double-underscore notation, for example `Kairos__ProductName`.
