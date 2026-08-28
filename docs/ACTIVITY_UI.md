# Aktivitätsübersicht und Detailseite

Ticket #16 macht importierte Aktivitäten im Web-Frontend auffindbar und prüfbar. Die Seiten verwenden den bestehenden grünen Kairos-Stil, funktionieren responsiv und sind über den Navigationspunkt **Aktivitäten** erreichbar.

## Übersicht

`GET /api/activities` liefert höchstens 100 Aktivitäten des angemeldeten Benutzers, absteigend nach Startzeit. Die Abfrage liest nur indexierte Metadaten und nicht die vollständigen Messreihen.

Die Seite `/activities` zeigt:

- Datum, Uhrzeit und Sportart
- Dauer und Distanz
- Qualitätsstatus `Vollständig`, `Eingeschränkt` oder `Prüfung nötig`
- ursprünglichen Dateinamen
- Lade-, Fehler- und Leerzustand

Über **FIT importieren** kann eine Datei direkt hochgeladen und importiert werden. Nach dem Import öffnet Kairos die neue Aktivität. Ein erkannter Duplikatimport öffnet die bereits bestehende Aktivität.

## Detailseite

Die Seite `/activities/{id}` enthält:

- Zusammenfassungswerte mit Einheit und Herkunft
- verständliche Qualitäts- und Einschränkungshinweise
- Originaldatei, Anbieter, Importzeit und SHA-256
- Laps mit eigener Zusammenfassung
- alle unterstützten Messreihen in einer horizontal scrollbareren Tabelle

Berechnete Werte werden als **Berechnet**, gemessene und importierte Werte als **Importiert** gekennzeichnet. Fehlende optionale Messwerte erscheinen als Gedankenstrich oder werden durch den Qualitätsbereich erklärt.

Nicht angemeldete Benutzer erhalten eine eindeutige Anmeldeaufforderung. Fremde Aktivitäten werden von der API nicht offengelegt und erscheinen wie nicht vorhandene Daten.

## Barrierearme Zustände

- Ladezustände verwenden `aria-live`.
- Fehlerzustände verwenden `role="alert"` und bieten eine erneute Abfrage an.
- Formulareingaben besitzen sichtbare Beschriftungen.
- Fokusrahmen bleiben im gesamten Frontend sichtbar.
- Tabellenüberschriften und Seitenbereiche besitzen semantische Bezeichnungen.
- Bei reduziert gewünschter Bewegung werden Übergänge deaktiviert.
