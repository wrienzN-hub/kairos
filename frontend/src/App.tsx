import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";

import { AuthenticationProvider } from "./auth/AuthenticationContext";
import { AppErrorBoundary } from "./components/AppErrorBoundary";
import { AppShell } from "./components/AppShell";
import { TodayPage } from "./pages/TodayPage";
import { ActivitiesPage } from "./pages/ActivitiesPage";
import { ActivityDetailPage } from "./pages/ActivityDetailPage";

export function AppRoutes() {
  return (
    <Routes>
      <Route element={<AppShell />}>
        <Route path="/today" element={<TodayPage />} />
        <Route path="/activities" element={<ActivitiesPage />} />
        <Route path="/activities/:id" element={<ActivityDetailPage />} />
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
