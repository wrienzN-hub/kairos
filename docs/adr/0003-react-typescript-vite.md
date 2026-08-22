# ADR 0003: React, TypeScript, and Vite

- Status: Accepted
- Date: 2026-08-22

## Context

The athlete-facing web app must be responsive, accessible, and quick to evolve.
It needs client-side routing and a small, fast development toolchain.

## Decision

The frontend uses React with strict TypeScript, React Router, and Vite. Vitest and
Testing Library cover behavior; Oxlint and Prettier enforce code quality.

## Consequences

The UI can grow incrementally from the `/today` route. Browser-only APIs must be
tested in jsdom or behind explicit adapters.
