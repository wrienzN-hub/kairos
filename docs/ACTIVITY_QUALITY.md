# Duplikate und Datenqualität

Ticket #15 verhindert doppelte Trainingslast und macht Einschränkungen einer importierten Aktivität sichtbar.

## Duplikaterkennung

Für jeden FIT-Upload wird bereits beim Hochladen ein SHA-256 gebildet. Vor dem Import sucht Kairos innerhalb des angemeldeten Benutzerkontos nach einer Aktivität mit demselben Hash.

- Ohne Treffer wird eine neue Aktivität importiert.
- Bei einem Treffer bleibt die bestehende Aktivität maßgeblich.
- Der neue Upload erhält den Status `duplicate`.
- Die Importantwort enthält die ID der bereits vorhandenen Aktivität und antwortet mit `200 OK`.
- Ein eindeutiger Datenbankindex auf Besitzer und SHA-256 verhindert zusätzlich doppelte Aktivitäten bei konkurrierenden Anfragen.

Gleiche Dateien verschiedener Benutzer gelten nicht als Duplikat voneinander und bleiben strikt getrennt.

## Qualitätsstatus

Jede importierte Aktivität besitzt einen der folgenden Analysezustände:

| Status | Bedeutung |
| --- | --- |
| `eligible` | Keine bekannte Einschränkung für die unterstützten Analysen |
| `limited` | Die Aktivität ist gültig, aber mindestens eine Analyse ist wegen fehlender Daten eingeschränkt |
| `blocked` | Unplausible oder unbrauchbare Daten verhindern verlässliche Analysen |

Ein Qualitätshinweis enthält immer einen stabilen Code, Schweregrad, eine verständliche deutsche Beschreibung und die betroffenen Metrikcodes.

## Fehlende Messreihen

- `missing_power_stream`: keine Leistung; leistungsbasierte Analysen eingeschränkt
- `missing_cadence_stream`: keine Trittfrequenz; Trittfrequenzanalyse nicht verfügbar
- `missing_heart_rate_stream`: keine Herzfrequenz; Belastungsanalyse eingeschränkt
- `missing_position_stream`: keine vollständige Position; Streckenanalyse nicht verfügbar

Fehlende Watt- oder Trittfrequenzdaten verhindern den Import ausdrücklich nicht. Das unterstützt Fahrten ohne Wattpedale.

## Plausibilitätsgrenzen

Kairos meldet aktuell Werte außerhalb dieser technisch großzügigen Bereiche:

- Geschwindigkeit: 0–50 m/s
- Herzfrequenz: 25–240 bpm
- Trittfrequenz: 0–250 rpm
- Leistung: 0–2500 W
- Breitengrad: −90 bis 90 Grad
- Längengrad: −180 bis 180 Grad
- Distanz: nicht negativ
- Aktivitätsdauer: größer als null

Diese Grenzen sind keine medizinische Bewertung. Sie erkennen offensichtlich fehlerhafte Importdaten und können später unabhängig versioniert und sportartspezifisch verfeinert werden.

Die Importantwort nennt `analysisStatus` und `qualityFindingCount`. Der Aktivitätsendpunkt liefert zusätzlich `isAnalysisRestricted` und alle Qualitätshinweise, damit Frontend und Anwender die Einschränkung nachvollziehen können.
