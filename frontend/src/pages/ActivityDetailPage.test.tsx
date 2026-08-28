import { render, screen } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { afterEach, describe, expect, it, vi } from "vitest";

import { AppRoutes } from "../App";

const authentication = vi.hoisted(() => ({
  authenticated: true,
  identity: { name: "Nikolaj" },
  getAccessToken: vi.fn().mockResolvedValue("token"),
  login: vi.fn(),
  logout: vi.fn(),
}));

vi.mock("../auth/authentication-state", () => ({
  useAuthentication: () => authentication,
}));

afterEach(() => vi.unstubAllGlobals());

describe("Activity detail page", () => {
  it("shows source, quality, summary and supported time series", async () => {
    vi.stubGlobal(
      "fetch",
      vi.fn().mockImplementation((input: RequestInfo | URL) =>
        Promise.resolve(
          new Response(
            JSON.stringify(
              input === "/api/me" ? { name: "Nikolaj" } : activity,
            ),
            {
              status: 200,
              headers: { "Content-Type": "application/json" },
            },
          ),
        ),
      ),
    );

    render(
      <MemoryRouter initialEntries={["/activities/activity-1"]}>
        <AppRoutes />
      </MemoryRouter>,
    );

    expect(
      await screen.findByRole("heading", { name: "Radfahrt" }),
    ).toBeInTheDocument();
    expect(screen.getByText("Eingeschränkt")).toBeInTheDocument();
    expect(screen.getByText(/keine Leistungsdaten/)).toBeInTheDocument();
    expect(screen.getByText("morning.fit")).toBeInTheDocument();
    expect(screen.getByText("10.00 km")).toBeInTheDocument();
    expect(
      screen.getByRole("heading", { name: "Aufgezeichnete Zeitreihe" }),
    ).toBeInTheDocument();
    expect(screen.getAllByText("Importiert").length).toBeGreaterThan(0);
  });

  it("shows an actionable error state", async () => {
    vi.stubGlobal(
      "fetch",
      vi.fn().mockImplementation((input: RequestInfo | URL) =>
        Promise.resolve(
          input === "/api/me"
            ? new Response("{}", {
                status: 200,
                headers: { "Content-Type": "application/json" },
              })
            : new Response(null, { status: 404 }),
        ),
      ),
    );

    render(
      <MemoryRouter initialEntries={["/activities/missing"]}>
        <AppRoutes />
      </MemoryRouter>,
    );

    expect(
      await screen.findByRole("heading", { name: "Aktivität nicht verfügbar" }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("button", { name: "Erneut laden" }),
    ).toBeInTheDocument();
  });
});

const measured = (code: string, value: number, symbol: string) => ({
  code,
  value,
  unit: { code: symbol, symbol },
  provenance: {
    origin: "importedSummary",
    sourceField: `session.${code}`,
    sourceUnit: symbol,
    derivation: null,
  },
});

const activity = {
  id: "activity-1",
  type: "cycling",
  source: {
    kind: "fit_file",
    provider: "file_import",
    externalIdentifier: "upload-1",
    originalFileName: "morning.fit",
    contentHashSha256: "a".repeat(64),
    importedAtUtc: "2026-01-15T06:31:00Z",
  },
  timeRange: {
    start: {
      instantUtc: "2026-01-15T06:00:00Z",
      timeZoneId: "Etc/UTC",
      observedUtcOffsetMinutes: 0,
    },
    end: {
      instantUtc: "2026-01-15T06:30:00Z",
      timeZoneId: "Etc/UTC",
      observedUtcOffsetMinutes: 0,
    },
  },
  summary: [measured("distance", 10000, "m"), measured("duration", 1800, "s")],
  samples: [
    {
      timestampUtc: "2026-01-15T06:00:00Z",
      metrics: [measured("speed", 5.5, "m/s")],
    },
  ],
  segments: [],
  quality: {
    analysisStatus: "limited",
    isAnalysisRestricted: true,
    findings: [
      {
        code: "missing_power_stream",
        severity: "warning",
        message: "Die Aktivität enthält keine Leistungsdaten.",
        affectedMetricCodes: ["power"],
      },
    ],
  },
};
