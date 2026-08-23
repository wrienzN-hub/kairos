import { createContext, useContext } from "react";

export type AthleteIdentity = {
  id?: string;
  name?: string;
  email?: string;
};

export type AuthenticationContextValue = {
  authenticated: boolean;
  identity: AthleteIdentity | null;
  getAccessToken: () => Promise<string | null>;
  login: () => Promise<void>;
  logout: () => Promise<void>;
};

export const AuthenticationContext =
  createContext<AuthenticationContextValue | null>(null);

export function useAuthentication() {
  const context = useContext(AuthenticationContext);
  if (!context) {
    throw new Error(
      "useAuthentication must be used inside AuthenticationProvider.",
    );
  }

  return context;
}
