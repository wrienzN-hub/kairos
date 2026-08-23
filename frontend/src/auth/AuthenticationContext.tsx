import Keycloak, { type KeycloakTokenParsed } from "keycloak-js";
import { type ReactNode, useEffect, useMemo, useState } from "react";

import {
  AuthenticationContext,
  type AuthenticationContextValue,
  type AthleteIdentity,
} from "./authentication-state";

const keycloak = new Keycloak({
  url: import.meta.env.VITE_KEYCLOAK_URL ?? "http://localhost:8081",
  realm: import.meta.env.VITE_KEYCLOAK_REALM ?? "kairos",
  clientId: import.meta.env.VITE_KEYCLOAK_CLIENT_ID ?? "kairos-web",
});

const googleLoginEnabled = import.meta.env.VITE_GOOGLE_LOGIN_ENABLED === "true";
let initialization: Promise<boolean> | undefined;

function initializeKeycloak() {
  initialization ??= keycloak.init({
    onLoad: "check-sso",
    checkLoginIframe: false,
    pkceMethod: "S256",
  });

  return initialization;
}

function readIdentity(token?: KeycloakTokenParsed): AthleteIdentity | null {
  if (!token) {
    return null;
  }

  return {
    id: token.sub,
    name: typeof token.name === "string" ? token.name : undefined,
    email: typeof token.email === "string" ? token.email : undefined,
  };
}

export function AuthenticationProvider({ children }: { children: ReactNode }) {
  const [status, setStatus] = useState<"loading" | "ready" | "error">(
    "loading",
  );
  const [authenticated, setAuthenticated] = useState(false);
  const [identity, setIdentity] = useState<AthleteIdentity | null>(null);

  useEffect(() => {
    let active = true;

    const synchronizeSession = () => {
      if (!active) return;
      setAuthenticated(keycloak.authenticated === true);
      setIdentity(readIdentity(keycloak.tokenParsed));
    };

    keycloak.onAuthSuccess = synchronizeSession;
    keycloak.onAuthRefreshSuccess = synchronizeSession;
    keycloak.onAuthLogout = synchronizeSession;
    keycloak.onTokenExpired = () => {
      void keycloak.updateToken(30).catch(() => keycloak.login());
    };

    void initializeKeycloak()
      .then(() => {
        if (!active) return;
        synchronizeSession();
        setStatus("ready");
      })
      .catch(() => {
        if (active) setStatus("error");
      });

    return () => {
      active = false;
    };
  }, []);

  const value = useMemo<AuthenticationContextValue>(
    () => ({
      authenticated,
      googleLoginEnabled,
      identity,
      getAccessToken: async () => {
        if (!keycloak.authenticated) return null;
        await keycloak.updateToken(30);
        return keycloak.token ?? null;
      },
      login: async () => {
        await keycloak.login({
          redirectUri: `${window.location.origin}/today`,
        });
      },
      loginWithGoogle: async () => {
        if (!googleLoginEnabled) return;
        await keycloak.login({
          idpHint: "google",
          redirectUri: `${window.location.origin}/today`,
        });
      },
      logout: async () => {
        await keycloak.logout({
          redirectUri: `${window.location.origin}/today`,
        });
      },
      register: async () => {
        await keycloak.register({
          redirectUri: `${window.location.origin}/today`,
        });
      },
    }),
    [authenticated, identity],
  );

  if (status === "loading") {
    return (
      <main className="auth-bootstrap" aria-live="polite">
        <span className="auth-spinner" aria-hidden="true" />
        <p>Anmeldung wird vorbereitet …</p>
      </main>
    );
  }

  if (status === "error") {
    return (
      <main className="auth-bootstrap auth-error" role="alert">
        <h1>Anmeldung derzeit nicht erreichbar</h1>
        <p>
          Prüfe, ob Keycloak läuft, und versuche es anschließend erneut. Deine
          Zugangsdaten werden nicht in Kairos gespeichert.
        </p>
        <button type="button" onClick={() => window.location.reload()}>
          Erneut versuchen
        </button>
      </main>
    );
  }

  return (
    <AuthenticationContext.Provider value={value}>
      {children}
    </AuthenticationContext.Provider>
  );
}
