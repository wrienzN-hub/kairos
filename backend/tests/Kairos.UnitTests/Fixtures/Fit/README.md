# Synthetische FIT-Referenzdateien

Diese Dateien bilden den Abnahmevertrag für den ersten Kairos-FIT-Import. Sie
enthalten ausschließlich deterministisch erzeugte, synthetische Radfahrdaten.
Es wurden weder persönliche Trainingsaufzeichnungen noch Geräteexporte oder
Dateien Dritter verwendet.

Die maschinenlesbaren Sollwerte, Dateigrößen und SHA-256-Prüfsummen stehen in
[`expectations.json`](expectations.json). Zeiten sind immer UTC, Dauer wird in
Sekunden und Distanz in Metern angegeben. `null` bedeutet ausdrücklich, dass
der Wert fehlt und von einem späteren Parser nicht erfunden werden darf.

## Abgedeckte Fälle

| Datei | Zweck | Start UTC | Dauer | Distanz | Messreihen |
| --- | --- | --- | ---: | ---: | --- |
| `valid-cycling.fit` | vollständige Radaktivität | 2026-01-15 06:00 | 1.800 s | 10.000 m | Zeit, Position, Höhe, Distanz, Geschwindigkeit, Herzfrequenz, Trittfrequenz, Leistung, Temperatur |
| `minimal-cycling.fit` | kleinster unterstützter Fall | 2026-01-16 12:00 | 300 s | 1.000 m | Zeit, Distanz |
| `interval-cycling.fit` | zwei prüfbare Laps/Intervalle | 2026-01-17 09:00 | 1.200 s | 8.000 m | Zeit, Distanz, Geschwindigkeit, Herzfrequenz, Trittfrequenz, Leistung |
| `incomplete-cycling.fit` | gültig, aber wesentliche Daten fehlen | 2026-01-18 07:30 | 900 s | fehlt | Zeit, Herzfrequenz |
| `corrupted-crc.fit` | verständlich abzulehnender Fehlerfall | – | – | – | keine, Import muss vor Auswertung scheitern |

Beim Intervallfall dauert jeder Lap 600 Sekunden. Der erste Lap umfasst 3.500 m,
der zweite 4.500 m. Der beschädigte Fall ist von `valid-cycling.fit` abgeleitet,
enthält absichtlich eine unpassende Datei-CRC und erwartet den Fehlercode
`crc_mismatch`.

## Reproduzierbarkeit

Alle Dateien und die Manifest-Prüfsummen werden aus dem Repository-Stamm neu
erzeugt:

```powershell
python tools/fit-fixtures/generate_fit_fixtures.py
```

Die .NET-Unit-Tests prüfen Fallabdeckung, FIT-Signatur, deklarierte Dateigröße,
Header- und Datei-CRC, SHA-256 und die dokumentierte synthetische Herkunft.

Für eine unabhängige Kontrolle kann optional Garmins offizielles Python-SDK
verwendet werden:

```powershell
python -m pip install garmin-fit-sdk
python tools/fit-fixtures/verify_fit_fixtures.py
```

Der Verifizierer dekodiert die vier gültigen Dateien und vergleicht Start- und
Endzeit, Dauer, Distanz, verfügbare Messreihen sowie Laps mit dem Manifest. Beim
beschädigten Fall erwartet er eine fehlgeschlagene Integritätsprüfung. Das SDK
ist bewusst keine Kairos-Laufzeitabhängigkeit; die Parserauswahl erfolgt erst im
späteren Parser-Ticket.

Protokollgrundlage: [Garmin FIT Protocol](https://developer.garmin.com/fit/protocol/)
und [Garmin FIT Activity Files](https://developer.garmin.com/fit/file-types/activity/).
