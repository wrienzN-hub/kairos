# Aktivitätsexport und Löschung

Ticket #17 gibt angemeldeten Benutzern die Kontrolle über ihre importierten Aktivitätsdaten. Beide Funktionen sind auf den Besitzer der Aktivität begrenzt; eine fremde oder unbekannte ID wird immer mit `404 Not Found` beantwortet.

## JSON-Export

`GET /api/activities/{id}/export` liefert eine herunterladbare Datei mit dem Medientyp `application/json`. Das maschinenlesbare Format besitzt aktuell `schemaVersion: 1` und enthält:

- Exportzeitpunkt und stabile Aktivitäts-ID
- Sportart und vollständigen Zeitraum
- Quellenangaben einschließlich Originaldateiname und SHA-256
- normalisierte Zusammenfassungswerte, Messpunkte und Laps
- Einheit und Provenienz jedes Messwerts
- Datenqualitätsstatus und sämtliche Findings

Die Provenienz unterscheidet importierte, gemessene, eingegebene und berechnete Werte. Bei berechneten Werten werden Methode, Version und Eingabemetriken ausgegeben. Damit kann ein Export später nachvollziehbar weiterverarbeitet werden.

Ein erfolgreicher Export erzeugt einen Audit-Eintrag `exported`. Die exportierte Aktivität selbst wird nicht verändert.

## Löschung

`DELETE /api/activities/{id}` entfernt in einem Datenbankvorgang:

- das normalisierte Aktivitätsdokument
- alle darin enthaltenen Messpunkte und Laps
- sämtliche daraus abgeleiteten Werte und Qualitätsinformationen
- den zugehörigen Upload einschließlich der ursprünglichen FIT-Bytes

Vor dem API-Aufruf zeigt die Detailseite einen Bestätigungsdialog mit dem konkreten Umfang und den vorhandenen Anzahlen. Erst **Endgültig löschen** sendet die Anfrage. Die Löschung kann danach nicht rückgängig gemacht werden.

Ein minimaler Audit-Eintrag `deleted` bleibt ohne Fremdschlüssel zur Aktivität erhalten. Er speichert Besitzer, Aktivitäts-ID, Zeitpunkt, Dateiname, Hash, frühere Anzahlen und die Bestätigung, dass der Roh-Upload gelöscht wurde. Er enthält weder FIT-Bytes noch die vollständigen Messreihen.

## Endpunkte

| Methode  | Pfad                          | Erfolgsantwort        |
| -------- | ----------------------------- | --------------------- |
| `GET`    | `/api/activities/{id}/export` | `200` mit JSON-Anhang |
| `DELETE` | `/api/activities/{id}`        | `204 No Content`      |

Beide Endpunkte erfordern ein gültiges Access Token. Fehlende Besitzerrechte werden nicht von einer unbekannten Aktivität unterschieden.
