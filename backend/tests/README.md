# Backend tests

`Kairos.UnitTests` verifies isolated domain/application behavior and dependency
direction. `Kairos.IntegrationTests` starts the real API host in memory to verify
HTTP behavior and startup configuration. Future architecture and acceptance
tests should continue to mirror the production boundaries they verify.
