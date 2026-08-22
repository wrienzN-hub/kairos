# ADR 0004: PostgreSQL and EF Core

- Status: Accepted
- Date: 2026-08-22

## Context

Training data is relational, time-oriented, and requires durable migrations and
reliable querying. Local and hosted environments should use the same database.

## Decision

PostgreSQL is the system of record. EF Core with the Npgsql provider manages the
data model and versioned migrations. Docker Compose applies migrations during
local API startup; other environments can run them explicitly.

## Consequences

Schema changes require committed migrations. PostgreSQL behavior should be used
for database-specific integration tests; the in-memory provider is reserved for
fast isolation and wiring tests.
