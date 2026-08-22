# Backend source

The initial deployable and module boundary projects are:

- `Kairos.Api`: ASP.NET Core HTTP host.
- `Kairos.Application`: use cases and ports.
- `Kairos.Domain`: domain model and invariants.
- `Kairos.Infrastructure`: adapters and persistence implementations.

Product capabilities such as activities/import, athlete profile, goals,
analysis, planning, and coach dialogue will be added inside these boundaries
without prematurely splitting the system into services.
