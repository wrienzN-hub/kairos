import { render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
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

afterEach(() => {
  vi.unstubAllGlobals();
  vi.clearAllMocks();
  authentication.authenticated = true;
});

describe("Activities page", () => {
  it("shows essential metadata and quality for imported activities", async () => {
    const fetch = vi.fn().mockImplementation((input: RequestInfo | URL) =>
      Promise.resolve(
        new Response(
          JSON.stringify(
            input === "/api/me"
              ? { name: "Nikolaj" }
              : [
                  {
                    id: "activity-1",
                    type: "cycling",
                    startUtc: "2026-01-15T06:00:00Z",
                    endUtc: "2026-01-15T06:30:00Z",
                    distanceMeters: 10000,
                    analysisStatus: "limited",
                    originalFileName: "morning.fit",
                    sourceProvider: "file_import",
                  },
                ],
          ),
          { status: 200, headers: { "Content-Type": "application/json" } },
        ),
      ),
    );
    vi.stubGlobal("fetch", fetch);

    render(
      <MemoryRouter initialEntries={["/activities"]}>
        <AppRoutes />
      </MemoryRouter>,
    );

    expect(await screen.findByText("Radfahren")).toBeInTheDocument();
    expect(screen.getByText("10.0 km")).toBeInTheDocument();
    expect(screen.getByText("30 min")).toBeInTheDocument();
    expect(screen.getByText("Eingeschränkt")).toBeInTheDocument();
    expect(screen.getByText("morning.fit")).toBeInTheDocument();
    expect(fetch).toHaveBeenCalledWith("/api/activities", {
      headers: { Authorization: "Bearer token" },
    });
  });

  it("renders an accessible empty state", async () => {
    vi.stubGlobal(
      "fetch",
      vi.fn().mockImplementation((input: RequestInfo | URL) =>
        Promise.resolve(
          new Response(input === "/api/me" ? "{}" : "[]", {
            status: 200,
            headers: { "Content-Type": "application/json" },
          }),
        ),
      ),
    );

    render(
      <MemoryRouter initialEntries={["/activities"]}>
        <AppRoutes />
      </MemoryRouter>,
    );

    expect(
      await screen.findByRole("heading", { name: "Noch keine Aktivität" }),
    ).toBeInTheDocument();
    expect(screen.getByText(/ohne Watt-/)).toBeInTheDocument();
  });

  it("offers central login when signed out", async () => {
    authentication.authenticated = false;
    vi.stubGlobal("fetch", vi.fn());
    render(
      <MemoryRouter initialEntries={["/activities"]}>
        <AppRoutes />
      </MemoryRouter>,
    );

    await userEvent.click(
      screen.getByRole("button", { name: "Jetzt anmelden" }),
    );

    expect(authentication.login).toHaveBeenCalledOnce();
    await waitFor(() => expect(fetch).not.toHaveBeenCalled());
  });
});
