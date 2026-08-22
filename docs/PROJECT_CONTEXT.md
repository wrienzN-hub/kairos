# Kairos – Project Context

This document captures the durable product context distilled from the original
[ChatGPT planning conversation](https://chatgpt.com/share/6a70f30d-9140-83eb-b223-da2a0328140a).
It is a working baseline, not a finished requirements specification. Ideas listed
as candidates still require prioritization and validation.

## Product vision

Kairos is an AI-powered endurance coach for ambitious endurance athletes. The
name refers to the “right” or opportune moment: the system should determine which
training is appropriate at which time.

Kairos must not become merely “ChatGPT for sport” or another dashboard that
reports metrics. Its intended differentiator is an autonomous, explainable coach
that can:

- analyse completed training;
- plan future training against long-term goals;
- monitor progress, fatigue, recovery, calendar constraints, and weather;
- adapt plans when circumstances change;
- explain the evidence behind every recommendation;
- learn from the athlete's history and feedback.

## Initial user and problem

The initial user is a committed cyclist/endurance athlete who wants more useful
guidance than Garmin or Strava typically provide. Existing products expose many
numbers, but often do not explain why a session succeeded or failed, what should
change next time, or how today's work contributes to a multi-year goal.

Example questions Kairos should eventually answer:

- Why was my heart rate lower today than during a comparable session?
- Were my intervals executed correctly, and what should I change next time?
- Am I progressing toward a target VO2max, FTP, or race?
- How should today's workout change because of sleep, HRV, fatigue, weather, or
  a calendar conflict?

## Long-term goals

Users should be able to define dated, measurable goals, including examples such
as:

- reach a VO2max of 65 by 2028;
- reach an FTP of 400 W by 2027;
- complete an Ironman in 2029.

The Goal Engine should continuously estimate progress, probability, confidence,
and recommended actions rather than merely storing goal text.

## Core product capabilities

### Training ingestion and analysis

- Import activity and health data from Garmin initially; FIT-file ingestion is a
  viable fallback or complementary route.
- Detect structured intervals and analyse power, heart rate, speed, cadence,
  duration, consistency, heart-rate drift, and aerobic decoupling.
- Calculate or track metrics such as FTP, VO2max, TSS, CTL, ATL, TSB, training
  load, peak power, recovery, sleep, resting heart rate, and HRV where data is
  available.
- Compare similar sessions over time and produce concrete, actionable feedback.

### Intelligent planning

- Generate training plans and individual workouts from athlete goals and current
  state.
- Re-plan automatically after missed sessions, illness, fatigue, travel, weather,
  or calendar changes.
- Support cycling first, with running, swimming, triathlon, strength, nutrition,
  and additional wearables as later expansion candidates.

### Explainable AI and memory

- Every material recommendation should cite the relevant athlete data and explain
  its reasoning in plain language.
- Athlete memory should retain useful longitudinal facts, such as poor recovery
  after a certain interval prescription or recurring scheduling constraints.
- Coach personality may later be configurable (for example scientific,
  motivating, direct, professional, or friendly).
- A later “digital twin” or prediction capability may simulate the likely impact
  of alternative training scenarios.

### Context integrations

- Calendar integration for realistic scheduling and conflict detection.
- Weather integration for heat, rain, wind, storms, and automatic workout changes.
- Route generation that can consider elevation, wind, traffic, junctions, and
  road quality.
- Notifications and background workflows.

### Athlete timeline and dashboard

- Provide a unified timeline across activities, races, illness, holidays,
  equipment changes, tests, training camps, weight, sleep, and other relevant
  events.
- Present goals and progress clearly, while retaining drill-down access to the
  evidence and calculations behind each assessment.

## Technical direction discussed

The intended starting stack is:

- React frontend;
- ASP.NET Core/.NET backend;
- PostgreSQL database;
- Docker-based local/deployment environment;
- OpenAI-powered coaching capabilities;
- n8n for orchestration and integrations;
- GitHub for source control, issues, projects, actions, and collaboration.

The architectural preference is to keep domain logic in the .NET application.
n8n should orchestrate imports, scheduled jobs, external services, and
notifications, but must not become the home of training analytics or core
coaching rules.

A modular monolith is the sensible initial default unless concrete scaling or
organizational requirements justify services later. Candidate bounded modules
include:

- Activity Import;
- Analytics Engine;
- Coach Engine;
- Workout/Planning Engine;
- Recovery Engine;
- Goal and Prediction Engine;
- Routing Engine;
- Calendar and Weather integrations;
- Notifications.

## Garmin and MCP constraints

Community Garmin MCP servers were considered as an integration shortcut. They
should not be treated as an authoritative production dependency without further
investigation: Garmin does not provide an official MCP server, community options
may rely on unofficial Garmin Connect interfaces, and authentication, stability,
terms, privacy, and data ownership must be evaluated.

MCP is useful as a tool interface for AI agents, but it does not replace Kairos's
own durable data model, ingestion pipeline, analytics, authorization, or business
logic.

## Documentation and engineering approach

Kairos should be treated as a real product rather than a disposable prototype.
The repository should evolve to contain:

- a versioned requirements specification;
- architecture decision records (ADRs);
- system, data, sequence, and deployment diagrams;
- an OpenAPI specification;
- database design and migration strategy;
- AI/tool/prompt architecture;
- security and GDPR considerations;
- roadmap, risks, and acceptance criteria;
- coding, testing, branching, and CI/CD conventions.

Requirements should use stable identifiers and testable acceptance criteria.
Architecture and scope decisions should be recorded explicitly instead of being
left implicit in chat history.

## Candidate domain entities

The planning conversation identified the following early candidates:

- User / Athlete Profile;
- Goal and Progress Prediction;
- Activity and Activity Samples;
- Workout, Workout Step, and Training Plan;
- Recovery Status and Sleep;
- Route and Weather Snapshot;
- Calendar Event;
- Equipment / Bike;
- AI Insight and Coach Recommendation;
- Timeline Event.

These are discovery inputs, not a finalized schema.

## Delivery strategy

Build incrementally. A sensible first vertical slice is:

1. import one Garmin/FIT activity;
2. persist it in PostgreSQL;
3. detect and display intervals;
4. calculate a small, verified set of metrics;
5. generate an evidence-backed coaching report;
6. show the result in a minimal React interface.

Only after this slice is trustworthy should the project expand into autonomous
planning, calendar/weather adaptation, routing, broad health integrations, or
long-range prediction.

## Open decisions

- Exact MVP scope and target athlete segment.
- Garmin ingestion method and legal/operational constraints.
- Initial metric definitions and validation datasets.
- Whether AI recommendations are advisory only or may automatically modify plans.
- Data retention, consent, deletion, encryption, and OpenAI data boundaries.
- Hosting model, identity provider, observability, and cost limits.
- Which route, weather, and calendar providers to use.
- Product tiers or monetization; these remain future considerations.

