import { NavLink, Outlet } from "react-router-dom";

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
        <nav aria-label="Hauptnavigation">
          <NavLink to="/today">Heute</NavLink>
        </nav>
      </header>
      <Outlet />
    </div>
  );
}
