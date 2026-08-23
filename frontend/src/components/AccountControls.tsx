import { useEffect, useState } from "react";

import { useAuthentication } from "../auth/authentication-state";

export function AccountControls() {
  const authentication = useAuthentication();
  const [expanded, setExpanded] = useState(false);
  const [apiIdentity, setApiIdentity] = useState<{
    name?: string;
    email?: string;
  } | null>(null);
  const [profileError, setProfileError] = useState(false);

  useEffect(() => {
    if (!authentication.authenticated) {
      return;
    }

    const controller = new AbortController();
    void authentication
      .getAccessToken()
      .then(async (token) => {
        if (!token) throw new Error("Missing access token.");
        const response = await fetch("/api/me", {
          headers: { Authorization: `Bearer ${token}` },
          signal: controller.signal,
        });
        if (!response.ok) throw new Error("Profile request failed.");
        setApiIdentity(
          (await response.json()) as { name?: string; email?: string },
        );
        setProfileError(false);
      })
      .catch((error: unknown) => {
        if (!(error instanceof DOMException && error.name === "AbortError")) {
          setApiIdentity(null);
          setProfileError(true);
        }
      });

    return () => controller.abort();
  }, [authentication]);

  if (authentication.authenticated) {
    return (
      <div className="account-controls authenticated">
        <button
          type="button"
          className="account-trigger"
          aria-expanded={expanded}
          onClick={() => setExpanded((current) => !current)}
        >
          <span className="avatar" aria-hidden="true">
            {(apiIdentity?.name ?? authentication.identity?.name ?? "A")
              .slice(0, 1)
              .toUpperCase()}
          </span>
          <span>
            {apiIdentity?.name ?? authentication.identity?.name ?? "Athlet:in"}
          </span>
        </button>
        {expanded && (
          <div className="account-popover">
            {(apiIdentity?.email ?? authentication.identity?.email) && (
              <p>{apiIdentity?.email ?? authentication.identity?.email}</p>
            )}
            {profileError && (
              <p role="alert">
                Das geschützte Profil ist gerade nicht erreichbar.
              </p>
            )}
            <button type="button" onClick={() => void authentication.logout()}>
              Abmelden
            </button>
          </div>
        )}
      </div>
    );
  }

  return (
    <div className="account-controls signed-out">
      <button type="button" onClick={() => void authentication.login()}>
        Anmelden
      </button>
      <button
        type="button"
        className="google-login"
        disabled={!authentication.googleLoginEnabled}
        title={
          authentication.googleLoginEnabled
            ? "Mit Google anmelden"
            : "Google-Anmeldung muss zuerst konfiguriert werden"
        }
        onClick={() => void authentication.loginWithGoogle()}
      >
        <span aria-hidden="true">G</span>
        Mit Google anmelden
      </button>
      <button
        type="button"
        className="register-link"
        onClick={() => void authentication.register()}
      >
        Konto erstellen
      </button>
    </div>
  );
}
