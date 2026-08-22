# Kairos

Kairos is an explainable AI endurance coach for cycling, cycling-supportive
strength training, and rowing. It connects training analysis, adaptive planning,
goals, recovery, calendar, weather, and athlete feedback.

## Repository structure

```text
kairos/
├── backend/          # ASP.NET Core modular monolith and backend tests
├── frontend/         # responsive React web application
├── infrastructure/   # Docker and deployment infrastructure
├── tools/            # repository-local development and verification tools
├── docs/
│   ├── adr/          # architecture decision records
│   ├── backlog/      # publish-ready implementation backlog
│   └── lastenheft/   # approved product requirements
└── Kairos.sln        # root .NET solution
```

Application projects and runtime dependencies are introduced by their dedicated
backlog tickets. This keeps the repository foundation reviewable and avoids
mixing project organization with framework bootstrap decisions.

## Backend quick start

The repository pins .NET SDK 10.0.400 through `global.json`. From the repository
root:

```powershell
dotnet restore Kairos.sln
dotnet build Kairos.sln --configuration Release --no-restore
dotnet test Kairos.sln --configuration Release --no-build
dotnet run --project backend/src/Kairos.Api
```

The API readiness endpoint is available at `GET /health`. Runtime configuration
uses the `Kairos` section; required values are validated during application
startup.

## Product documentation

- [Lastenheft](docs/lastenheft/README.md)
- [Implementation backlog](docs/backlog/README.md)
- [Project context](docs/PROJECT_CONTEXT.md)
- [Architecture decisions](docs/adr/README.md)

## Current development focus

The current foundation provides the ASP.NET Core API and its automated tests.
The first vertical product slice imports a cycling FIT file, preserves its
provenance, stores normalized activity data, and displays an accessible activity
detail view. A small, validated training analysis follows on top of that data.
