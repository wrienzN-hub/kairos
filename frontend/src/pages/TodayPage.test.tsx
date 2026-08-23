import { render, screen } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { describe, expect, it } from "vitest";
import { vi } from "vitest";

import { AppRoutes } from "../App";

vi.mock("../auth/authentication-state", () => ({
  useAuthentication: () => ({
    authenticated: false,
    googleLoginEnabled: false,
    identity: null,
    login: vi.fn(),
    loginWithGoogle: vi.fn(),
    logout: vi.fn(),
    register: vi.fn(),
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
    expect(
      screen.getByRole("button", { name: "Mit Google anmelden" }),
    ).toBeDisabled();
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
});
