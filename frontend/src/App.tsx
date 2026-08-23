import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";

import { AuthenticationProvider } from "./auth/AuthenticationContext";
import { AppErrorBoundary } from "./components/AppErrorBoundary";
import { AppShell } from "./components/AppShell";
import { TodayPage } from "./pages/TodayPage";

export function AppRoutes() {
  return (
    <Routes>
      <Route element={<AppShell />}>
        <Route path="/today" element={<TodayPage />} />
        <Route path="*" element={<Navigate replace to="/today" />} />
      </Route>
    </Routes>
  );
}

export default function App() {
  return (
    <AppErrorBoundary>
      <AuthenticationProvider>
        <BrowserRouter>
          <AppRoutes />
        </BrowserRouter>
      </AuthenticationProvider>
    </AppErrorBoundary>
  );
}
