import { render, screen } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { describe, expect, it } from "vitest";

import { AppRoutes } from "../App";

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
