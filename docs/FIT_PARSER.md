# FIT-Parser und Normalisierung

Ticket #13 wandelt eine bereits validierte FIT-Aktivitätsdatei in das herstellerneutrale Kairos-Domänenmodell um. Der Parser verwendet das offizielle Garmin FIT SDK. Er speichert die Aktivität noch nicht; diese Orchestrierung folgt im nächsten Import-Ticket.

## Ablauf

1. Die vollständige Datei wird dekodiert, bevor Daten ausgewertet werden.
2. Nur FIT-Dateien vom Typ `activity` werden akzeptiert.
3. Session, Laps und Records werden gesammelt und anschließend zeitlich geordnet.
4. Herstellerfelder und Einheiten werden in stabile Kairos-Metrikcodes und SI-nahe Einheiten überführt.
5. Originaldateiname, Upload-ID, SHA-256 und Importzeit bleiben als `ActivitySource` erhalten.
6. Jedes normalisierte Feld trägt seine Quelle und ursprüngliche Einheit als `DataProvenance`.

Unbekannte Nachrichten und nicht verwendete optionale Felder werden vom SDK dekodiert, aber von Kairos ignoriert. Fehlende optionale Werte sind zulässig. Eine Radfahrt ohne Watt- oder Trittfrequenzdaten bleibt daher importierbar.

## Unterstützte Werte

| FIT-Quelle | Kairos-Code | Einheit |
| --- | --- | --- |
| `session.total_timer_time` | `duration` | Sekunden |
| `session.total_distance` | `distance` | Meter |
| `session.avg/max_speed` | `average_speed` / `maximum_speed` | Meter pro Sekunde |
| `session.avg/max_heart_rate` | `average_heart_rate` / `maximum_heart_rate` | Schläge pro Minute |
| `session.avg/max_cadence` | `average_cadence` / `maximum_cadence` | Umdrehungen pro Minute |
| `session.avg/max_power` | `average_power` / `maximum_power` | Watt |
| `record.position_lat/long` | `latitude` / `longitude` | Grad |
| `record.altitude` | `altitude` | Meter |
| `record.distance` | `distance` | Meter |
| `record.speed` | `speed` | Meter pro Sekunde |
| `record.heart_rate` | `heart_rate` | Schläge pro Minute |
| `record.cadence` | `cadence` | Umdrehungen pro Minute |
| `record.power` | `power` | Watt |
| `record.temperature` | `temperature` | Grad Celsius |

Laps werden als geordnete `lap`-Segmente mit eigener Dauer, Distanz und – sofern vorhanden – Durchschnitts- und Maximalwerten abgebildet. Doppelte Record-Zeitpunkte werden deterministisch zu einem Sample zusammengeführt.

FIT-Zeitstempel stellen UTC-Zeitpunkte bereit, aber keine verlässliche IANA-Zeitzone. Deshalb hält der Import den exakten Zeitpunkt in UTC fest und setzt zunächst `Etc/UTC`; eine spätere Zeitzonenanreicherung kann darauf aufbauen.

## Fehlercodes

- `empty_fit_file`: kein Dateiinhalt
- `invalid_fit_structure`: Datei kann nicht vollständig als FIT dekodiert werden
- `unsupported_fit_file_type`: gültige FIT-Datei, aber keine Aktivitätsdatei
- `missing_activity_time`: keine brauchbare Start- und Endzeit

Die automatisierten Referenztests decken vollständige und minimale Radfahrten, Intervalle, fehlende Watt-/Trittfrequenzdaten, Einheiten, Provenienz und beschädigte Dateien ab.
