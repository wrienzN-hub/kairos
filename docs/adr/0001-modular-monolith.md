# ADR 0001: Modular monolith

- Status: Accepted
- Date: 2026-08-22

## Context

Kairos needs clear domain boundaries but is initially developed and operated by
one person. Distributed services would add deployment and consistency overhead
before independent scaling is necessary.

## Decision

The backend is an ASP.NET Core modular monolith. Domain, Application,
Infrastructure, and API projects express dependency boundaries. Product
capabilities are added as cohesive modules within this process.

## Consequences

Development and deployment remain simple, while module boundaries are testable.
Modules may be extracted later when measured operational needs justify it.
