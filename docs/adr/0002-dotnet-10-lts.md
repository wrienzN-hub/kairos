# ADR 0002: .NET 10 LTS

- Status: Accepted
- Date: 2026-08-22

## Context

The API needs a supported, cross-platform runtime with strong typing, mature web
and testing frameworks, and predictable long-term maintenance.

## Decision

Kairos uses ASP.NET Core on .NET 10 LTS. `global.json` pins the SDK feature band;
container and CI images use .NET 10.

## Consequences

The same runtime line is used locally, in CI, and in containers. SDK upgrades are
explicit changes that must pass the full verification pipeline.
