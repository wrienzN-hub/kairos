# Aktivitätsspeicherung

Ticket #14 verbindet den sicheren FIT-Upload mit Parser und Aktivitätsspeicher. Ein Upload bleibt zunächst im Status `pending`. Erst der explizite Import erzeugt eine normalisierte Aktivität und setzt denselben Upload im gleichen Datenbank-Commit auf `imported`.

## API-Ablauf

1. `POST /api/activity-imports/fit` – FIT-Datei hochladen
2. `POST /api/activity-imports/fit/{uploadId}/import` – Datei parsen und atomar speichern
3. `GET /api/activities/{activityId}` – vollständige Aktivität abrufen

Alle drei Endpunkte sind authentifiziert und verwenden die Keycloak-`sub` als Besitzerkennung. Eine fremde Aktivität wird wie eine nicht vorhandene Aktivität mit `404` beantwortet.

## Speicherformat

Die Tabelle `activities` enthält separat indexierbare Metadaten:

- Besitzer und Aktivitäts-ID
- Aktivitätstyp sowie Start- und Endzeit
- Upload-Referenz
- Quellenart, Anbieter, Originaldateiname und SHA-256
- Importzeit

Zusammenfassung, Messpunkte, Segmente, Qualitätshinweise, Einheiten und vollständige Provenienz liegen gemeinsam in einem versionierten JSONB-Dokument. Dadurch benötigt eine Detailabfrage nur einen Datensatz, während spätere Übersichtsabfragen ausschließlich die kleinen indexierten Spalten lesen können.

Die Relation `source_upload_id` ist eindeutig. Aktivität und Statuswechsel werden mit einem einzigen `SaveChanges` geschrieben; EF Core verwendet dafür eine Transaktion. Schlägt ein Teil fehl, gibt es weder eine akzeptierte Teilaktivität noch einen fälschlich als importiert markierten Upload.

## Importantwort

Der Import liefert `201 Created`, die neue Activity-URL und folgende kompakte Angaben:

- Aktivitäts-ID und Typ
- Start- und Endzeit
- Anzahl Samples und Segmente
- Status `imported`

Die Detailantwort enthält zusätzlich Source, TimeRange, Summary, Samples, Segmente, Qualität sowie die Herkunft jedes Messwerts.
