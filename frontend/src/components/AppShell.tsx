import { NavLink, Outlet } from "react-router-dom";

import { AccountControls } from "./AccountControls";

export function AppShell() {
  return (
    <div className="app-shell">
      <header className="topbar">
        <NavLink className="brand" to="/today" aria-label="Kairos Startseite">
          <span className="brand-mark" aria-hidden="true">
            K
          </span>
          <span>Kairos</span>
        </NavLink>
        <div className="topbar-actions">
          <nav aria-label="Hauptnavigation">
            <NavLink to="/today">Heute</NavLink>
            <NavLink to="/activities">Aktivitäten</NavLink>
          </nav>
          <AccountControls />
        </div>
      </header>
      <Outlet />
    </div>
  );
}
