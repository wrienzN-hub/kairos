import { render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";

import { AccountControls } from "./AccountControls";

const { authentication } = vi.hoisted(() => ({
  authentication: {
    authenticated: true,
    identity: { name: "Token Athlete", email: "token@example.test" },
    getAccessToken: vi.fn(async () => "access-token"),
    login: vi.fn(async () => undefined),
    logout: vi.fn(async () => undefined),
  },
}));

vi.mock("../auth/authentication-state", () => ({
  useAuthentication: () => authentication,
}));

describe("Account controls", () => {
  beforeEach(() => {
    authentication.authenticated = true;
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

    render(<AccountControls />);

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

  it("opens the central Keycloak login for signed-out users", async () => {
    authentication.authenticated = false;

    render(<AccountControls />);
    await userEvent.click(screen.getByRole("button", { name: "Anmelden" }));

    expect(authentication.login).toHaveBeenCalledOnce();
  });
});
