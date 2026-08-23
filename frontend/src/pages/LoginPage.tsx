import { Navigate } from "react-router-dom";

import { useAuthentication } from "../auth/authentication-state";

export function LoginPage() {
  const authentication = useAuthentication();

  if (authentication.authenticated) {
    return <Navigate replace to="/today" />;
  }

  return (
    <main className="login-page">
      <section className="login-card" aria-labelledby="login-heading">
        <div className="login-intro">
          <p className="eyebrow">Willkommen bei Kairos</p>
          <h1 id="login-heading">Dein Training beginnt mit dir.</h1>
          <p>
            Melde dich sicher an, damit Kairos deinen Trainingsverlauf, dein
            Feedback und deine nächsten Einheiten zuordnen kann.
          </p>
          <ul aria-label="Vorteile deines Kairos-Kontos">
            <li>Radfahren, Krafttraining und Rudern an einem Ort</li>
            <li>Persönliche Empfehlungen auf Basis deines Feedbacks</li>
            <li>Deine Trainingsdaten bleiben deinem Konto zugeordnet</li>
          </ul>
        </div>

        <div className="login-options">
          <div>
            <p className="eyebrow">Anmelden oder registrieren</p>
            <h2>Wie möchtest du fortfahren?</h2>
          </div>

          <button
            type="button"
            className="login-option google-option"
            disabled={!authentication.googleLoginEnabled}
            title={
              authentication.googleLoginEnabled
                ? "Mit Google fortfahren"
                : "Google-Anmeldung muss zuerst konfiguriert werden"
            }
            onClick={() => void authentication.loginWithGoogle()}
          >
            <span className="google-mark" aria-hidden="true">
              G
            </span>
            <span>
              <strong>Mit Google fortfahren</strong>
              <small>Anmelden oder automatisch ein Konto erstellen</small>
            </span>
          </button>

          <div className="login-divider" aria-hidden="true">
            <span>oder</span>
          </div>

          <button
            type="button"
            className="login-option local-option"
            onClick={() => void authentication.login()}
          >
            <span className="keycloak-mark" aria-hidden="true">
              K
            </span>
            <span>
              <strong>Mit E-Mail und Passwort anmelden</strong>
              <small>Sicher über die Kairos-Benutzerverwaltung</small>
            </span>
          </button>

          <p className="register-prompt">
            Noch kein Kairos-Konto?{" "}
            <button
              type="button"
              onClick={() => void authentication.register()}
            >
              Konto erstellen
            </button>
          </p>

          <p className="login-security">
            Deine Anmeldung wird sicher über Keycloak verarbeitet. Kairos
            speichert weder dein Google-Passwort noch dein lokales Passwort.
          </p>
        </div>
      </section>
    </main>
  );
}
