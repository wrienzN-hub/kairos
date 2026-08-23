# Kairos implementation backlog

This backlog is the publish-ready source for GitHub Issues. It is stored in the
repository because GitHub issue creation is currently blocked by the connected
app's missing issue-write permission.

Every implementation issue must satisfy the repository Definition of Done in
`docs/lastenheft/16_Qualitaetsmerkmale_und_Abnahmekriterien.md`.

## Epic A – Lauffähige Produktbasis schaffen

**Goal:** Run and develop Kairos reproducibly with a React frontend, ASP.NET Core
backend, PostgreSQL, Docker, and automated verification.

**Acceptance:** Tickets A1–A7 are complete and a new developer can start and test
the system from the root documentation.

### A1 – Monorepo- und Solution-Struktur anlegen

**Goal:** Establish clear ownership boundaries before application code is added.

**Scope:** Root solution, backend `src`/`tests`, frontend, infrastructure, tools,
ADR, issue templates, ignore rules, and backlog structure.

**Out of scope:** Creating application projects, installing dependencies, Docker
services, or CI.

**Acceptance criteria:**

- [ ] `Kairos.sln` exists at the repository root.
- [ ] All planned top-level areas are documented and tracked.
- [ ] `.gitignore` excludes secrets, build output, dependencies, and local data.
- [ ] Issue templates capture goal, scope, acceptance, verification, and references.
- [ ] Repository structure is documented and contains no generated build output.

**References:** Lastenheft 17.2–17.5 and 19.4.

### A2 – ASP.NET-Core-Backend initialisieren

**Goal:** Provide a runnable .NET 10 LTS backend foundation.

**Scope:** API host, initial modular boundaries, configuration validation, health
endpoint, unit/integration test projects, and solution registration.

**Out of scope:** Database schema, authentication, FIT import, and business logic.

**Acceptance criteria:**

- [ ] The pinned .NET 10 SDK builds the backend.
- [ ] The API starts and exposes a health endpoint.
- [ ] Configuration errors fail clearly.
- [ ] Unit and integration test commands pass.

**Depends on:** A1.

### A3 – React-Web-App initialisieren

**Goal:** Provide a runnable responsive frontend foundation.

**Scope:** React with TypeScript, routing shell, “Today” placeholder, error
boundary, linting, formatting, and component test setup.

**Out of scope:** Final design system and product features.

**Acceptance criteria:**

- [ ] The app starts from a documented command.
- [ ] A responsive “Today” route renders.
- [ ] Lint, type-check, test, and production build pass.
- [ ] No runtime secrets are committed.

**Depends on:** A1.

### A4 – PostgreSQL und Migrationen einrichten

**Goal:** Establish versioned, testable persistence.

**Scope:** Database connection, migration mechanism, empty initial schema,
integration-test database strategy, and configuration documentation.

**Out of scope:** Activity domain tables.

**Acceptance criteria:**

- [ ] A clean database can be created by migration.
- [ ] Migration status is observable.
- [ ] Integration tests use isolated data.
- [ ] Credentials are supplied outside source control.

**Depends on:** A2.

### A5 – Docker-Compose-Entwicklungsumgebung erstellen

**Goal:** Start Kairos dependencies reproducibly from the repository root.

**Scope:** PostgreSQL container, backend and frontend development wiring,
healthchecks, environment example, volumes, and documented commands.

**Acceptance criteria:**

- [ ] A documented command starts the supported local stack.
- [ ] Healthchecks expose readiness.
- [ ] Persistent data and secrets remain untracked.
- [ ] Stopping and restarting does not corrupt the database.

**Depends on:** A2, A3, A4.

### A6 – Build- und Testpipeline einrichten

**Goal:** Verify every proposed change automatically.

**Scope:** GitHub Actions for backend build/tests, frontend lint/type-check/tests,
and repository policy documentation.

**Acceptance criteria:**

- [ ] Pull requests run all required checks.
- [ ] A failing test blocks successful completion.
- [ ] Dependency caches do not hide lockfile changes.
- [ ] Workflow permissions use least privilege.

**Depends on:** A2, A3.

### A7 – Erste ADRs und Entwicklerdokumentation anlegen

**Goal:** Make setup and foundational decisions durable.

**Scope:** Root development README and ADRs for modular monolith, .NET 10,
frontend choice, PostgreSQL, and repository organization.

**Acceptance criteria:**

- [ ] A new developer can set up, run, and test the current system.
- [ ] Each foundational decision has context and consequences.
- [ ] Commands match the implemented environment.

**Depends on:** A2–A6.

## Epic B – FIT-Aktivität End-to-End bereitstellen

**Goal:** Import, normalize, store, display, delete, and export a cycling FIT
activity with traceable source and data quality.

**Acceptance:** Tickets B1–B8 are complete and the reference/error cases in
Lastenheft 16.5 pass.

### B1 – FIT-Referenzdateien und Erwartungswerte definieren

**Goal:** Establish safe, non-personal test fixtures before parser development.

**Acceptance criteria:**

- [ ] Valid, minimal, interval, incomplete, and corrupted cases are covered.
- [ ] Expected timestamps, duration, distance, and available streams are documented.
- [ ] Fixtures contain synthetic or legally reusable data.

### B2 – Aktivitätsdomänenmodell entwerfen

**Goal:** Represent source, activity summary, samples, laps/segments, and quality
without coupling the domain to one vendor.

**Acceptance criteria:**

- [ ] Units, time zones, provenance, and derived values are distinguishable.
- [ ] Cycling, strength, and rowing remain extensible.
- [ ] The model and invariants are documented and tested.

**Depends on:** A4, B1.

### B3 – Sicheren FIT-Upload implementieren

**Goal:** Accept supported FIT files without exposing the service to unsafe or
unbounded input.

**Acceptance criteria:**

- [ ] File type, size, ownership, and malformed input are handled.
- [ ] Upload failures are understandable and do not persist partial activities.
- [ ] Sensitive contents are not written to logs.

**Depends on:** A2, B1.

### B4 – FIT-Datei parsen und normalisieren

**Goal:** Convert supported FIT messages into the domain import representation.

**Acceptance criteria:**

- [ ] Reference fixtures match documented expected values.
- [ ] Unknown optional fields do not break valid imports.
- [ ] Unit and time normalization is reproducible.
- [ ] Original provenance is retained.

**Depends on:** B1, B2, B3.

### B5 – Aktivität und Messreihen speichern

**Goal:** Persist an imported activity atomically and retrieve it efficiently.

**Acceptance criteria:**

- [ ] Summary, source metadata, samples, and segments persist transactionally.
- [ ] Partial failures leave no accepted half-import.
- [ ] Integration tests verify write and read behavior.

**Depends on:** B2, B4.

### B6 – Duplikate und Datenqualität behandeln

**Goal:** Prevent double load and make analysis limitations visible.

**Acceptance criteria:**

- [ ] Reimporting the same source does not duplicate training load.
- [ ] Missing streams and implausible values receive explicit quality findings.
- [ ] A user can understand why an activity is restricted.

**Depends on:** B5.

### B7 – Aktivitätsübersicht und Detailseite erstellen

**Goal:** Let the athlete find and inspect imported cycling activities.

**Acceptance criteria:**

- [ ] The responsive overview shows essential activity metadata.
- [ ] The detail view shows source, quality, summary, and supported time series.
- [ ] Loading, empty, partial, and error states are accessible.
- [ ] Measured and derived values are visually distinguishable.

**Depends on:** A3, B5, B6.

### B8 – Aktivität löschen und exportieren

**Goal:** Provide early data control for the first imported domain object.

**Acceptance criteria:**

- [ ] The user sees deletion scope before confirmation.
- [ ] Activity, samples, and dependent derived data are deleted consistently.
- [ ] A documented machine-readable export includes provenance and normalized data.
- [ ] Authorization and audit behavior are tested.

**Depends on:** B5, B7.

## Epic C – Identität und Athletenkonto

**Goal:** Provide secure, provider-independent user identities before personal
training data is implemented.

### C1 – Benutzerverwaltung mit Keycloak und Google-Anmeldung

**Goal:** Register and authenticate athletes through Keycloak, including Google
as an external identity provider, and protect personal API endpoints.

**Acceptance criteria:**

- [ ] Local registration, login, session restoration, and logout work.
- [ ] Google login is brokered through Keycloak without exposing secrets to the frontend.
- [ ] The SPA uses Authorization Code Flow with PKCE and does not persist tokens.
- [ ] The API validates issuer and audience and rejects anonymous access to `/api/me`.
- [ ] Docker, automated tests, and setup documentation cover the identity flow.

**Depends on:** A2, A3, A5.
