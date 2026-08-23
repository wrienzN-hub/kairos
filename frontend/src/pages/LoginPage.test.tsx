import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter } from "react-router-dom";
import { beforeEach, describe, expect, it, vi } from "vitest";

import { LoginPage } from "./LoginPage";

const { authentication } = vi.hoisted(() => ({
  authentication: {
    authenticated: false,
    googleLoginEnabled: true,
    identity: null,
    getAccessToken: vi.fn(async () => null),
    login: vi.fn(async () => undefined),
    loginWithGoogle: vi.fn(async () => undefined),
    logout: vi.fn(async () => undefined),
    register: vi.fn(async () => undefined),
  },
}));

vi.mock("../auth/authentication-state", () => ({
  useAuthentication: () => authentication,
}));

describe("Login page", () => {
  beforeEach(() => {
    authentication.authenticated = false;
    authentication.googleLoginEnabled = true;
    vi.clearAllMocks();
  });

  it("offers Google, local login, and registration", async () => {
    render(
      <MemoryRouter>
        <LoginPage />
      </MemoryRouter>,
    );

    await userEvent.click(
      screen.getByRole("button", { name: /Mit Google fortfahren/ }),
    );
    await userEvent.click(
      screen.getByRole("button", {
        name: /Mit E-Mail und Passwort anmelden/,
      }),
    );
    await userEvent.click(
      screen.getByRole("button", { name: "Konto erstellen" }),
    );

    expect(authentication.loginWithGoogle).toHaveBeenCalledOnce();
    expect(authentication.login).toHaveBeenCalledOnce();
    expect(authentication.register).toHaveBeenCalledOnce();
  });

  it("disables Google when the provider is not configured", () => {
    authentication.googleLoginEnabled = false;

    render(
      <MemoryRouter>
        <LoginPage />
      </MemoryRouter>,
    );

    expect(
      screen.getByRole("button", { name: /Mit Google fortfahren/ }),
    ).toBeDisabled();
  });
});
