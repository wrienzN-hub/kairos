# Backend

The backend will contain the ASP.NET Core modular monolith.

Planned structure:

```text
backend/
├── src/       # deployable application and domain modules
└── tests/     # unit, integration, architecture, and acceptance tests
```

Projects are created in the dedicated backend bootstrap ticket. The target is
.NET 10 LTS; the required SDK must be installed before that ticket is executed.

