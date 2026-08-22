# ADR 0005: Monorepo organization

- Status: Accepted
- Date: 2026-08-22

## Context

Frontend, backend, infrastructure, requirements, and delivery automation change
together during the initial product phase.

## Decision

Kairos uses one repository with top-level `backend`, `frontend`,
`infrastructure`, `docs`, and `tools` areas. Root-level Compose and CI files
coordinate the complete system.

## Consequences

A single pull request can deliver a vertical slice and verify all affected
parts. CI remains split into backend and frontend jobs for clear feedback.
