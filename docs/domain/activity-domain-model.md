# Aktivitätsdomänenmodell

## Zweck und Geltungsbereich

Das Modell unter `Kairos.Domain.Activities` ist der herstellerunabhängige
fachliche Vertrag für importierte und manuell erfasste Aktivitäten. Es bildet
Quelle, Zeitbezug, Zusammenfassung, Messpunkte, Laps beziehungsweise Segmente
und Datenqualität ab. FIT-Nachrichtennummern, Garmin-Klassen, Concept2-Felder,
RP3-Felder oder Persistenzdetails gehören bewusst nicht in die Domäne.

Das Modell speichert normalisierte Werte. Die ursprüngliche Datei und deren
vollständige technische Provenienz werden in späteren Import- und
Persistenztickets ergänzt; `ActivitySource` und `DataProvenance` schaffen dafür
bereits die notwendigen fachlichen Verweise.

## Struktur

```text
Activity
├── ActivityType                 erweiterbarer Sportcode
├── ActivitySource               Quelle, Provider, Fremd-ID/Datei/Hash, Importzeit
├── ActivityTimeRange
│   ├── Start                    UTC, ursprüngliche Zeitzone, beobachteter Offset
│   └── End                      UTC, ursprüngliche Zeitzone, beobachteter Offset
├── ActivitySummary
│   └── ActivityMetric[]         Wert, Einheit und Herkunft
├── ActivitySample[]             UTC-Zeitpunkt und beliebige Messgrößen
├── ActivitySegment[]            Lap, Intervall, Kraftsatz oder eigener Segmenttyp
│   └── ActivitySummary
└── ActivityQuality
    └── QualityFinding[]         Code, Schweregrad, Erklärung, betroffene Messgrößen
```

## Erweiterbarkeit der Sportarten

`ActivityType`, `SegmentType`, Messgrößencodes und Einheiten sind validierte,
offene Codes. Bekannte Werte erleichtern die einheitliche Nutzung, schließen
aber neue Sportarten oder Geräte nicht aus:

- Radfahren: `cycling`, beispielsweise `distance`, `heart_rate`, `power`;
- Krafttraining: `strength_training`, beispielsweise `load`, `repetitions`,
  Segmenttyp `strength_set`;
- Rudern: `rowing`, beispielsweise `stroke_rate`, `distance`, `pace`;
- spätere Varianten: `ActivityType.FromCode("ski_erg")` oder eigene Messgrößen
  und Einheiten.

Ein fehlender Sensor erzeugt keinen künstlichen Nullwert. Eine Radfahrt ohne
Wattpedale und Trittfrequenzsensor enthält schlicht keine `power`- und
`cadence`-Messgrößen. Falls dies für eine Funktion relevant ist, erklärt ein
`QualityFinding` die Einschränkung. Die Aktivität bleibt grundsätzlich gültig.

## Einheiten und Herkunft

Jeder `ActivityMetric` enthält:

- einen fachlichen Code;
- einen normalisierten Dezimalwert;
- eine explizite `MeasurementUnit` mit stabilem Code und Anzeigesymbol;
- eine `DataProvenance`.

Die Herkunft unterscheidet:

| Herkunft | Bedeutung | Pflichtangaben |
| --- | --- | --- |
| `Measured` | direkt gemessener Originalwert | Quellfeld, optional Quelleinheit |
| `ImportedSummary` | vom Quellsystem gelieferte Zusammenfassung | Quellfeld, optional Quelleinheit |
| `UserEntered` | vom Nutzer eingegeben | keine technische Quelle |
| `Derived` | von Kairos berechnet | Methode, Version und Eingangsgrößen |

Eine Ableitung ohne Methode, Version oder Eingangswert ist ungültig. Damit kann
Kairos später Berechnungen reproduzieren und nach einer Korrektur gezielt neu
bewerten. Normalisierte Einheit und ursprüngliche Quelleinheit bleiben getrennt.

## Zeit und Zeitzone

`ActivityTimestamp` trennt drei Sachverhalte:

1. `InstantUtc` ist der eindeutige, auf UTC normalisierte Zeitpunkt;
2. `TimeZoneId` bewahrt die ursprüngliche Zeitzone, etwa `Europe/Vienna`;
3. `ObservedUtcOffset` bewahrt den beim Ereignis beobachteten Offset.

Diese Trennung verhindert, dass Sommerzeit oder spätere Zeitzonenänderungen die
historische Anzeige verändern. Samples speichern UTC; der Zeitkontext liegt am
Aktivitätszeitraum. Ein Zeitbereich mit Ende vor Start ist ungültig.

## Quelle und Nachvollziehbarkeit

`ActivitySource` verwendet offene Codes für Quellenart und Provider. Ein
nichtmanueller Import benötigt mindestens Fremd-ID, Originaldateiname oder
SHA-256-Hash. Ein vorhandener Hash muss ein gültiger SHA-256-Wert sein. Die
Importzeit ist UTC. Dadurch können dateibasierte Importe und Integrationen
denselben Vertrag nutzen, ohne dass ein Anbieter fachliche Sonderrechte erhält.

Beispiele:

- FIT-Datei: Art `fit_file`, Provider `file_import`, Dateiname und SHA-256;
- Concept2/RP3: Art `integration`, Provider als Integrationscode und Fremd-ID;
- manuelle Krafteinheit: Art `manual`, Provider `kairos`.

## Invarianten

Das Modell erzwingt beim Erstellen:

- nichtleere Aktivitäts-ID und erforderliche Codes;
- UTC für Importzeit, Aktivitätszeit und Sample-Zeit;
- Ende nicht vor Start;
- eindeutige Messgrößencodes in Summary und je Sample;
- mindestens eine Messgröße je Sample;
- Samples innerhalb der Aktivität und streng aufsteigend nach UTC;
- eindeutige, nichtnegative Segmentindizes;
- Segmente vollständig innerhalb der Aktivität;
- gültige und nachvollziehbare Quellen;
- versionierte Ableitungen mit mindestens einer Eingangsgröße;
- explizite Qualitätsbefunde statt stiller Reparatur oder erfundener Werte.

Segmente dürfen sich fachlich überlappen. Das ist absichtlich nicht verboten,
weil beispielsweise ein erkannter Belastungsabschnitt innerhalb eines Geräteleaps
liegen kann. Ob ein konkreter Anwendungsfall Überlappungen zulässt, entscheidet
der jeweilige Use Case.

## Abgrenzung der folgenden Tickets

Dieses Ticket definiert ausschließlich das Domänenmodell. Noch nicht enthalten
sind:

- Datenbankabbildung und Migrationen;
- Upload, Dateigrößen- oder Berechtigungsprüfung;
- FIT-Dekodierung und Normalisierung;
- Duplikaterkennung und Qualitätsregeln;
- API-Endpunkte oder Benutzeroberfläche.

Diese Funktionen bauen in den Tickets B3 bis B8 auf den hier beschriebenen
Typen und Invarianten auf.
