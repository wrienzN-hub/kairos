import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter } from "react-router-dom";
import { describe, expect, it } from "vitest";
import { vi } from "vitest";

import { AppRoutes } from "../App";

const { login } = vi.hoisted(() => ({ login: vi.fn() }));

vi.mock("../auth/authentication-state", () => ({
  useAuthentication: () => ({
    authenticated: false,
    identity: null,
    login,
    logout: vi.fn(),
  }),
}));

describe("Today page", () => {
  it("renders the responsive coaching starting point", () => {
    render(
      <MemoryRouter initialEntries={["/today"]}>
        <AppRoutes />
      </MemoryRouter>,
    );

    expect(
      screen.getByRole("heading", {
        name: "Bereit für den nächsten guten Reiz.",
      }),
    ).toBeInTheDocument();
    expect(screen.getByText("Produktbasis bereit")).toBeInTheDocument();
    expect(
      screen.getByRole("navigation", { name: "Hauptnavigation" }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("button", { name: "Anmelden" }),
    ).toBeInTheDocument();
  });

  it("redirects unknown routes to today", async () => {
    render(
      <MemoryRouter initialEntries={["/unbekannt"]}>
        <AppRoutes />
      </MemoryRouter>,
    );

    expect(
      await screen.findByRole("heading", {
        name: "Bereit für den nächsten guten Reiz.",
      }),
    ).toBeInTheDocument();
  });

  it("starts the central Keycloak login from the header", async () => {
    render(
      <MemoryRouter initialEntries={["/today"]}>
        <AppRoutes />
      </MemoryRouter>,
    );

    await userEvent.click(screen.getByRole("button", { name: "Anmelden" }));

    expect(login).toHaveBeenCalledOnce();
  });
});
