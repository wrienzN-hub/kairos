# ADR 0006: Keycloak as identity broker

- Status: Accepted
- Date: 2026-08-23

## Context

Kairos needs account registration, secure sessions, protected APIs, and Google
login. Connecting every identity provider directly to the application would
couple product code to vendor-specific authentication behavior and secrets.

## Decision

Keycloak is the OpenID Connect identity broker. The React SPA uses Authorization
Code Flow with PKCE through the official `keycloak-js` adapter. The ASP.NET Core
API validates Keycloak JWT bearer tokens and their `kairos-api` audience. Google
is configured as an external identity provider in the Kairos realm.

## Consequences

Kairos receives one stable OIDC contract and can add or replace identity
providers centrally. Keycloak becomes security-critical infrastructure that must
use HTTPS, durable PostgreSQL storage, secret management, backups, and an upgrade
process before production deployment. The Compose configuration is explicitly a
local development setup and uses Keycloak development mode with local storage.
