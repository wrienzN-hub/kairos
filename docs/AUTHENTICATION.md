# Benutzerverwaltung, Keycloak und Google-Anmeldung

Kairos verwendet Keycloak als zentrale Benutzerverwaltung. Das Frontend kennt
keine Google-Geheimnisse und spricht ausschließlich OpenID Connect mit Keycloak.
Keycloak verwaltet lokale Konten und vermittelt die Anmeldung zu Google.

## Lokale Anmeldung testen

1. `.env.example` nach `.env` kopieren und insbesondere
   `KAIROS_KEYCLOAK_ADMIN_PASSWORD` ändern.
2. Den Stack mit `docker compose up --build --detach` starten.
3. `http://localhost:5173/today` öffnen.
4. Über **Konto erstellen** ein lokales Konto registrieren oder über
   **Anmelden** ein vorhandenes Konto verwenden.

Die Keycloak-Administration ist unter `http://localhost:8081/admin` erreichbar.
Die lokalen Zugangsdaten stehen nur in der nicht eingecheckten `.env`-Datei.

## Google OAuth einrichten

In der Google Cloud Console:

1. Ein Projekt auswählen oder erstellen.
2. Unter **APIs & Services → OAuth consent screen** einen externen
   Zustimmungsbildschirm konfigurieren.
3. Unter **Credentials → Create credentials → OAuth client ID** eine
   **Web application** erstellen.
4. Folgende autorisierte Weiterleitungs-URI exakt eintragen:

   `http://localhost:8081/realms/kairos/broker/google/endpoint`

5. Client-ID und Client-Secret in der lokalen `.env` eintragen und Google
   aktivieren:

```dotenv
KAIROS_GOOGLE_LOGIN_ENABLED=true
GOOGLE_CLIENT_ID=deine-client-id.apps.googleusercontent.com
GOOGLE_CLIENT_SECRET=dein-lokales-client-secret
```

Beim ersten Keycloak-Start werden diese Werte in das importierte Realm
übernommen. Wurde Keycloak vorher bereits ohne Google-Credentials gestartet,
kannst du entweder die Werte in der Keycloak-Administration unter
**Identity providers → Google** eintragen oder ausschließlich die lokale
Keycloak-Entwicklungsdatenbank neu erzeugen:

```powershell
docker compose stop keycloak frontend
docker compose rm --force keycloak frontend
docker volume rm kairos_kairos-keycloak-data
docker compose up --build --detach keycloak frontend
```

Der `docker volume rm`-Befehl löscht alle lokal angelegten Keycloak-Benutzer und
Sitzungen, aber nicht die Kairos-Trainingsdaten in PostgreSQL.

Ist Google aktiviert, erscheint im grünen Kairos-Header die Schaltfläche
**Mit Google anmelden**. Ohne Aktivierung bleibt sie deaktiviert und erklärt per
Hinweis, dass zuerst die Konfiguration fehlt. Google ist auf der allgemeinen
Keycloak-Anmeldeseite bewusst ausgeblendet; Kairos startet den Google-Login
direkt über diese Schaltfläche.

## Sicherheitsmodell

- Das Frontend nutzt Authorization Code Flow mit PKCE S256.
- Access- und Refresh-Tokens bleiben im Arbeitsspeicher und werden nicht in
  Local Storage oder Session Storage geschrieben.
- Das Backend akzeptiert nur Tokens mit dem Issuer des Kairos-Realm und der
  Audience `kairos-api`.
- `/api/me` antwortet ohne gültigen Bearer-Token mit HTTP 401.
- Google Client Secret und Keycloak-Admin-Passwort dürfen niemals committed
  werden.
- Redirect URIs und Web Origins sind lokal absichtlich auf
  `http://localhost:5173` begrenzt.

## Vor einem Produktivbetrieb

Die Compose-Konfiguration nutzt `start-dev` und Keycloaks lokale Datenhaltung.
Für Produktion sind mindestens erforderlich:

- HTTPS und feste öffentliche Hostnamen;
- Keycloak mit eigener PostgreSQL-Datenbank und Backups;
- Secrets aus einem Secret Store;
- verifizierte E-Mail-Adressen und ein definierter Wiederherstellungsprozess;
- eingeschränkte Redirect URIs und Web Origins ohne Wildcards;
- Monitoring, Audit-Logs, Updateprozess und getestete Wiederherstellung;
- aktualisierte Google-OAuth-Redirect-URI für die Produktionsdomain.
