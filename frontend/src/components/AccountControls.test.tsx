import { render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter } from "react-router-dom";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";

import { AccountControls } from "./AccountControls";

const { authentication } = vi.hoisted(() => ({
  authentication: {
    authenticated: true,
    googleLoginEnabled: false,
    identity: { name: "Token Athlete", email: "token@example.test" },
    getAccessToken: vi.fn(async () => "access-token"),
    login: vi.fn(async () => undefined),
    loginWithGoogle: vi.fn(async () => undefined),
    logout: vi.fn(async () => undefined),
    register: vi.fn(async () => undefined),
  },
}));

vi.mock("../auth/authentication-state", () => ({
  useAuthentication: () => authentication,
}));

describe("Account controls", () => {
  beforeEach(() => {
    authentication.authenticated = true;
    authentication.googleLoginEnabled = false;
  });

  afterEach(() => {
    vi.unstubAllGlobals();
    vi.clearAllMocks();
  });

  it("loads the protected athlete identity with the in-memory access token", async () => {
    vi.stubGlobal(
      "fetch",
      vi.fn(async () =>
        Response.json({
          id: "athlete-123",
          name: "API Athlete",
          email: "api@example.test",
        }),
      ),
    );

    render(
      <MemoryRouter>
        <AccountControls />
      </MemoryRouter>,
    );

    await waitFor(() =>
      expect(fetch).toHaveBeenCalledWith("/api/me", {
        headers: { Authorization: "Bearer access-token" },
        signal: expect.any(AbortSignal),
      }),
    );
    await userEvent.click(
      await screen.findByRole("button", { name: "API Athlete" }),
    );

    expect(screen.getByText("api@example.test")).toBeInTheDocument();
  });

  it("links signed-out users to the Kairos login page", () => {
    authentication.authenticated = false;
    authentication.googleLoginEnabled = true;

    render(
      <MemoryRouter>
        <AccountControls />
      </MemoryRouter>,
    );

    expect(screen.getByRole("link", { name: "Anmelden" })).toHaveAttribute(
      "href",
      "/login",
    );
  });
});
