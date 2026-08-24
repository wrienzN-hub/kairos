# Kairos lokal starten und entwickeln

Diese Datei ist die zentrale Befehlsreferenz für das gesamte System. Alle
Befehle werden im Repository-Stamm ausgeführt, sofern nicht anders angegeben.

## Voraussetzungen

Für den einfachsten Start wird nur Docker Desktop mit Docker Compose benötigt.
Für Entwicklung ohne Container zusätzlich:

- .NET SDK 10 (das Repository erwartet `10.0.400` laut `global.json`)
- Node.js 24 und npm
- optional PostgreSQL 17, falls die Datenbank nicht über Docker läuft

## Komplettes System mit Docker starten

Einmalig die lokale Konfiguration anlegen und das Passwort in `.env` ändern:

```powershell
Copy-Item .env.example .env
notepad .env
```

Danach alle Images bauen und Dienste starten:

```powershell
docker compose up --build
```

Im Hintergrund starten:

```powershell
docker compose up --build --detach
```

Adressen nach dem Start:

- Web-App: `http://localhost:5173/today`
- API: `http://localhost:8080`
- API-Healthcheck: `http://localhost:8080/health`
- PostgreSQL: `localhost:5432`
- Keycloak: `http://localhost:8081`
- Keycloak-Administration: `http://localhost:8081/admin`

Beim Containerstart wartet das Backend auf PostgreSQL und wendet ausstehende
EF-Core-Migrationen automatisch an. Frontend und Backend warten zusätzlich auf
Keycloak. Die vollständige Konto- und Google-Einrichtung steht in
[AUTHENTICATION.md](AUTHENTICATION.md).

Der sichere, authentifizierte FIT-Endpunkt und die Speicherung der noch nicht
geparsten Originaldateien sind in [FIT_UPLOAD.md](FIT_UPLOAD.md) beschrieben.

## Wichtige Docker-Befehle

```powershell
# Status und Healthchecks anzeigen
docker compose ps

# Logs aller Dienste verfolgen
docker compose logs --follow

# Nur Backend-Logs verfolgen
docker compose logs --follow backend

# Keycloak-Logs verfolgen
docker compose logs --follow keycloak

# Dienste stoppen, Container und Netzwerk behalten
docker compose stop

# Gestoppte Dienste wieder starten
docker compose start

# Einzelnen Dienst neu starten
docker compose restart backend

# Container entfernen; Datenbank-Volume bleibt erhalten
docker compose down

# Images neu bauen und starten
docker compose up --build --detach

# Nur einen Dienst neu bauen
docker compose build backend
docker compose up --detach backend

# Konfiguration prüfen
docker compose config

# Shell im Backend öffnen
docker compose exec backend /bin/sh

# PostgreSQL-Konsole öffnen
docker compose exec database psql -U kairos -d kairos

# Öffentliche Keycloak-/OIDC-Konfiguration prüfen
Invoke-WebRequest -UseBasicParsing http://localhost:8081/realms/kairos/.well-known/openid-configuration
```

Achtung: Der folgende Befehl löscht auch alle lokalen PostgreSQL-Daten und ist
nur für einen vollständigen Neuanfang gedacht:

```powershell
docker compose down --volumes
```

## Lokal entwickeln, nur PostgreSQL in Docker

Zuerst `.env` wie oben anlegen und nur die Datenbank starten:

```powershell
docker compose up --detach database
```

Backend in einem Terminal starten:

```powershell
dotnet restore Kairos.sln
dotnet run --project backend/src/Kairos.Api
```

Frontend in einem zweiten Terminal starten:

```powershell
Set-Location frontend
npm ci
npm run dev
```

Die lokale API verwendet standardmäßig die Development-Verbindung aus
`appsettings.Development.json`. Passwörter für andere Umgebungen werden über
Umgebungsvariablen oder Secret Stores gesetzt, niemals eingecheckt.

## Build, Tests und Qualität

Backend vollständig prüfen:

```powershell
dotnet restore Kairos.sln
dotnet format Kairos.sln --verify-no-changes --no-restore
dotnet build Kairos.sln --configuration Release --no-restore
dotnet test Kairos.sln --configuration Release --no-build
```

Frontend vollständig prüfen:

```powershell
Set-Location frontend
npm ci
npm run format:check
npm run lint
npm run typecheck
npm test
npm run build
```

Code automatisch formatieren:

```powershell
dotnet format Kairos.sln
Set-Location frontend
npm run format
```

## EF-Core-Migrationen

Lokale .NET-Tools einmalig wiederherstellen:

```powershell
dotnet tool restore
```

Neue Migration erstellen:

```powershell
dotnet tool run dotnet-ef migrations add NameDerMigration `
  --project backend/src/Kairos.Infrastructure `
  --startup-project backend/src/Kairos.Api `
  --output-dir Persistence/Migrations
```

Migrationen anzeigen oder auf die konfigurierte Datenbank anwenden:

```powershell
dotnet tool run dotnet-ef migrations list `
  --project backend/src/Kairos.Infrastructure `
  --startup-project backend/src/Kairos.Api

dotnet tool run dotnet-ef database update `
  --project backend/src/Kairos.Infrastructure `
  --startup-project backend/src/Kairos.Api
```

Letzte noch nicht veröffentlichte Migration entfernen:

```powershell
dotnet tool run dotnet-ef migrations remove `
  --project backend/src/Kairos.Infrastructure `
  --startup-project backend/src/Kairos.Api
```

Für eine abweichende Datenbank vor dem EF-Befehl setzen:

```powershell
$env:KAIROS_CONNECTION_STRING = "Host=localhost;Port=5432;Database=kairos;Username=kairos;Password=..."
```

## Häufige Probleme

- Port belegt: `KAIROS_WEB_PORT`, `KAIROS_API_PORT` oder `KAIROS_DB_PORT` in
  `.env` ändern und `docker compose up --detach` erneut ausführen.
- Backend startet nicht: `docker compose logs backend database` prüfen.
- Anmeldung startet nicht: `docker compose logs keycloak frontend` prüfen und
  `http://localhost:8081/realms/kairos` im Browser aufrufen.
- Google meldet `401: invalid_client`: Im Realm `kairos` unter
  **Identity providers → Google** prüfen, dass nicht der Platzhalter
  `not-configured`, sondern die Client-ID und das Client-Secret des
  Google-OAuth-Clients vom Typ **Web application** gespeichert sind.
- Migration schlägt fehl: zuerst mit `docker compose ps` kontrollieren, ob die
  Datenbank `healthy` ist.
- Frontend-Abhängigkeiten inkonsistent: im Ordner `frontend` erneut `npm ci`
  ausführen; dieser Befehl verwendet exakt `package-lock.json`.
- Containerzustand neu aufbauen, Daten aber behalten: `docker compose down`,
  anschließend `docker compose up --build --detach`.
