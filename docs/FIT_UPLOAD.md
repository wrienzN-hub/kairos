# Sicherer FIT-Upload

Ticket B3 stellt einen authentifizierten Eingang für FIT-Dateien bereit. Die
Datei wird in diesem Schritt noch nicht fachlich ausgewertet. Das übernimmt der
Parser in B4.

## Speicherung

Nach erfolgreicher Prüfung speichert Kairos die Originaldatei in PostgreSQL in
der Tabelle `fit_uploads`. Der Docker-Dienst `database` hält diese Daten im
Volume `kairos-postgres-data`. Ein Upload enthält:

- die unveränderliche Upload-ID;
- die Keycloak-Subject-ID (`sub`) des Besitzers;
- den bereinigten ursprünglichen Dateinamen und Medientyp;
- Dateigröße und SHA-256-Prüfsumme;
- Uploadzeitpunkt und Verarbeitungsstatus `pending`;
- den binären FIT-Inhalt.

Damit gibt es kein separates Upload-Verzeichnis und kein zusätzliches
Dateisystem-Volume. `docker compose down` behält die Uploads. Der ausdrücklich
destruktive Befehl `docker compose down --volumes` löscht auch diese lokalen
Daten.

## Sicherheitsregeln

`POST /api/activity-imports/fit` erwartet genau ein Formularfeld `file` und ein
gültiges Keycloak-Access-Token. Vor der Speicherung prüft Kairos:

- die konfigurierbare Maximalgröße (standardmäßig 10 MiB);
- Dateiendung und erlaubten Medientyp;
- FIT-Kopfgröße, `.FIT`-Signatur und Protokoll-Hauptversion;
- die im FIT-Header deklarierte Dateigröße;
- Header- und Datei-Prüfsumme.

Nur ein vollständig geprüfter Upload wird in einer Datenbanktransaktion
gespeichert. Fehler liefern ein Problem-Details-JSON mit einem stabilen `code`,
beispielsweise `file_too_large`, `unsupported_file_type` oder
`invalid_fit_crc`. Dateiinhalte werden nicht protokolliert.

Upload-Metadaten können über `GET /api/activity-imports/fit/{id}` abgefragt
werden. Die Abfrage ist auf den Besitzer beschränkt; für andere Benutzer wird
`404 Not Found` zurückgegeben, damit fremde Upload-IDs nicht offengelegt werden.

## Konfiguration

Die Grenze kann über die normale ASP.NET-Core-Konfiguration verändert werden:

```text
FitUpload__MaximumFileSizeBytes=10485760
```

Der Wert muss positiv und höchstens `2147483647` sein. Eine Änderung erfordert
einen Neustart des Backends.

## Direkter API-Test

Bis die Upload-Oberfläche Teil des Produkts ist, kann der Endpunkt mit einem
gültigen Access-Token getestet werden:

```powershell
curl.exe http://localhost:8080/api/activity-imports/fit `
  --header "Authorization: Bearer <ACCESS_TOKEN>" `
  --form "file=@C:\Pfad\zu\aktivitaet.fit;type=application/octet-stream"
```

Eine erfolgreiche Antwort verwendet HTTP `201 Created` und enthält Upload-ID,
Dateiname, Größe, SHA-256, Zeitpunkt und Status. Beschädigte oder nicht
unterstützte Dateien werden ohne Datenbankeintrag abgelehnt.
