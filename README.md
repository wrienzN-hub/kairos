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

## Quick start with Docker

From the repository root in PowerShell:

```powershell
Copy-Item .env.example .env
docker compose up --build
```

Then open `http://localhost:5173/today`. The API is available at
`http://localhost:8080`, and its health endpoint at
`http://localhost:8080/health`. Keycloak runs at `http://localhost:8081`.

All local setup, Docker, migration, test, build, and troubleshooting commands are
documented in [Development guide](docs/DEVELOPMENT.md).

## Product documentation

- [Lastenheft](docs/lastenheft/README.md)
- [Implementation backlog](docs/backlog/README.md)
- [Project context](docs/PROJECT_CONTEXT.md)
- [Architecture decisions](docs/adr/README.md)
- [Development and command guide](docs/DEVELOPMENT.md)
- [Authentication and Google login](docs/AUTHENTICATION.md)

## Current development focus

The current foundation provides the React application, ASP.NET Core API,
PostgreSQL persistence, Docker environment, and automated tests.
The first vertical product slice imports a cycling FIT file, preserves its
provenance, stores normalized activity data, and displays an accessible activity
detail view. A small, validated training analysis follows on top of that data.
