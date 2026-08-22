# Infrastructure

The root `compose.yaml` starts PostgreSQL, the ASP.NET Core API, and the React
web application. Container definitions and the Nginx SPA/proxy configuration
are in `infrastructure/docker`.

No secrets or persistent local data may be committed.

See [the development guide](../docs/DEVELOPMENT.md) for every supported command.
